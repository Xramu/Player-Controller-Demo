using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RamuInput;
using UnityEngine.InputSystem;

public class PlayerCharacterController : MonoBehaviour
{
    #region Constants

    protected const float GAMEPAD_JOYSTICK_LOOK_INPUT_MULTIPLIER = 150f;

    #endregion

    #region Inspector

    [SerializeField] private TextMeshProUGUI _velocityDebug;

    [Header("Player Input Controller")]

    [SerializeField] private PlayerInputScriptableObject _playerInputController;

    [Header("Player Character Controller")]

    [SerializeField] private CharacterController _characterController;

    [Header("Player Animation Controller")]

    [SerializeField] private PlayerAnimationController _playerAnimationController;

    [Header("Player Look")]

    [Tooltip("Default transform that the player humanoid IK will look at.")]
    [SerializeField] private Transform _defaultLookAtPoint;

    [Tooltip("Transform that will rotate with player's look input.")]
    [SerializeField] private Transform _playerLookTransform;

    [Tooltip("Transform that rotates with the player's look and determines the player's movement direction.")]
    [SerializeField] private Transform _orientationTransform;

    [Space]

    [Tooltip("Generic sensitivity multiplier of the player's look.")]
    [SerializeField] private float _genericSensitivity = 1.0f;

    [Tooltip("Horizontal sensitivity multiplier of the player's look.")]
    [SerializeField] private float _horizontalSensitivity = 1.0f;
    [Tooltip("Vertical sensitivity multiplier of the player's look.")]
    [SerializeField] private float _verticalSensitivity = 1.0f;

    [Space]

    [Tooltip("Maximum angle that the player can look vertically.")]
    [SerializeField] private float _clampedYLookMax = 90.0f;
    [Tooltip("Minimum angle that the player can look vertically.")]
    [SerializeField] private float _clampedYLookMin = -90.0f;

    [Header("Player Movement")]

    [Tooltip("Speed at which the grounded movement will accelerate.")]
    [SerializeField] private float _movementAcceleration = 20.0f;

    [Tooltip("Speed at which the grounded movement will deaccelerate.")]
    [SerializeField] private float _movementDeceleration = 60.0f;

    [Space]

    [SerializeField] private float _crouchSpeed = 3.0f;
    [SerializeField] private float _walkSpeed = 7.0f;
    [SerializeField] private float _sprintSpeed = 12.0f;

    [Header("Player Crouching")]

    [Tooltip("Point to check from anything blocking the player from standing up.")]
    [SerializeField] private Transform _crouchStandUpCheckTransform;

    [SerializeField] private Vector3 _crouchStandUpCheckBoxHalves = new Vector3(0.3f, 0.5f, 0.3f);
    [SerializeField] private Vector3 crouchStandUpCheckBoxOffset = new Vector3(0, 1.5f, 0);

    [Header("Player Ground Detection")]

    [SerializeField] private LayerMask _whatIsGround;

    [Space]

    [SerializeField] private Transform _groundCheckTransform;

    [SerializeField] private Vector3 _groundCheckBoxSizeHalves = new Vector3(0.2f, 0.06f, 0.2f);
    [SerializeField] private Vector3 _groundCheckBoxOffset = new Vector3(0, -0.06f, 0);

    [Space]

    [SerializeField] private float _snapToGroundDistance = 0.15f;

    [Header("Player Jumping")]

    [Tooltip("Velocity that the player will jump with.")]
    [SerializeField] private float _jumpHeight = 3;

    [Space]

    [SerializeField] private float _airControlSpeed = 20.0f;

    [Space]

    [SerializeField] private float _jumpFallMultiplier = 2.0f;

    [Header("Player Physics")]

    [SerializeField] private float _gravityMultiplier = 2.0f;

    #endregion

    #region Private

    //protected PlayerInputActions playerInputActions;

    private MovementState _movementState = MovementState.Walking;

    private bool _isGrounded = false;
    private bool _isGroundedForJump = false;

    private float _currentMovementSpeed;
    private float _currentJumpFallMultiplier = 1f;

    private Vector3 _smoothMoveVector;

    private Vector3 _velocity;
    private Vector3 _nextMoveVector;

    private bool _isCrouching;

    private bool _isGivingSprintInput = false;
    private bool _isGivingCrouchInput = false;
    private bool _isGivingJumpInput = false;
    private bool _isGivingFreeLookInput = false;

    #endregion

    #region Enums

    private enum MovementState
    {
        Crouching,
        Walking,
        Sprinting,
        AirCrouching,
        AirWalking,
        AirSprinting
    }

    #endregion

    #region Initialization

    private void Awake()
    {
        ResetIKLookTarget(true);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Set up controls
        SetupPlayerInputs();

        // Just in case
        UpdatePlayerMovementState();
    }

    #endregion

    #region Player Inputs

    private void SetupPlayerInputs()
    {
        _playerInputController.AddOnMoveInput(OnMoveInput);
        _playerInputController.AddOnLookInputMouse(OnLookInputMouse);
        _playerInputController.AddOnLookInputGamepad(OnLookInputGamepad);
        _playerInputController.AddOnCrouchInput(OnCrouchInput);
        _playerInputController.AddOnSprintInput(OnSprintInput);
        _playerInputController.AddOnJumpInput(OnJumpInput);
        _playerInputController.AddFreeLookInput(OnFreeLookInput);
    }

    #region Player Input Vectors

    private Vector2 _moveInput;
    private void OnMoveInput(Vector2 vector2)
    {
        // Normalize input if needed
        _moveInput = vector2.magnitude > 1 ? vector2.normalized : vector2;
    }

    private Vector2 _mouseLookInput;
    private void OnLookInputMouse(Vector2 vector2)
    {
        _mouseLookInput = vector2;
    }

    private Vector2 _gamepadLookInput;
    private void OnLookInputGamepad(Vector2 vector2)
    {
        _gamepadLookInput = vector2.magnitude > 1 ? vector2.normalized : vector2;
    }

    #endregion

    private void OnCrouchInput(bool state)
    {
        _isGivingCrouchInput = state;

        // Allow crouching instantly for now
        if (state)
            _isCrouching = state;

        UpdatePlayerMovementState();
    }

    private void OnSprintInput(bool state)
    {
        _isGivingSprintInput = state;
        UpdatePlayerMovementState();
    }

    private void OnJumpInput(bool state)
    {
        _isGivingJumpInput = state;
        
        // Try to jump
        if (state)
            TryJump();

        UpdatePlayerMovementState();
    }

    private void OnFreeLookInput(bool state)
    {
        if (!state)
            ReleaseFreeLook();

        _isGivingFreeLookInput = state;

        // Animator update
        _playerAnimationController.SetFreeLookBool(_isGivingFreeLookInput);
    }

    #endregion

    #region Player Look

    private void ResetIKLookTarget(bool instantRotate = false)
    {
        _playerAnimationController.SetHeadLookTarget(_defaultLookAtPoint, instantRotate);
    }

    private float lookRotationY;
    private float lookRotationX;
    private void PlayerLookUpdate()
    {
        // Add inputs together. Handle joystick with delta time
        Vector2 finalLookInput = _mouseLookInput + _gamepadLookInput * Time.deltaTime / Time.timeScale * GAMEPAD_JOYSTICK_LOOK_INPUT_MULTIPLIER;

        // Vertical clamped
        lookRotationY = Mathf.Clamp(lookRotationY - finalLookInput.y, _clampedYLookMin, _clampedYLookMax);

        // Horizontal
        lookRotationX += finalLookInput.x;

        // Rotate player look
        _playerLookTransform.localRotation = Quaternion.Euler(lookRotationY, lookRotationX, 0);

        // Rotate player orientation
        if (!_isGivingFreeLookInput)
            _orientationTransform.localRotation = Quaternion.Euler(0, lookRotationX, 0);
    }

    private void ReleaseFreeLook()
    {
        // Rseet look rotation
        lookRotationX = _orientationTransform.rotation.eulerAngles.y;
    }

    #endregion

    #region Ground Check

    /// <summary>
    /// Updates the <c>isGrounded</c> boolean by checking for colliders inside the ground detection area that are in the layermask <c>whatIsGround</c>
    /// </summary>
    private void UpdateIsGrounded()
    {
        // Specifically allow jumping only when the ovelap box hits something
        _isGroundedForJump = Physics.OverlapBox(_groundCheckTransform.position + _groundCheckBoxOffset, _groundCheckBoxSizeHalves, Quaternion.identity, _whatIsGround).Length > 0;

        bool newState = _characterController.isGrounded || _isGroundedForJump;

        if (_isGrounded == newState)
            return;

        // State actually changed
        _isGrounded = newState;

        if (!_isGrounded)
        {
            // Add movement as velocity when player has either jumped or walked off the ledge
            AddCurrentMovementAsVelocity();
        }

        // Update movement state
        UpdatePlayerMovementState();
    }

    #endregion

    #region Player Movement State

    private void UpdatePlayerMovementState()
    {
        MovementState newState = EvaluateCurrentMovementState();

        if (_movementState == newState)
            return;

        // State has actually changed
        _movementState = newState;

        switch (_movementState)
        {
            case MovementState.Crouching:
                _currentMovementSpeed = _crouchSpeed;
                break;
            case MovementState.AirSprinting:
            case MovementState.Sprinting:
                _currentMovementSpeed = _sprintSpeed;
                break;
            case MovementState.AirCrouching:
            case MovementState.AirWalking:
            case MovementState.Walking:
                _currentMovementSpeed = _walkSpeed;
                break;
        }

        // Animator updates
        _playerAnimationController.SetIsGroundedState(_isGrounded);
        _playerAnimationController.SetIsCrouchedState(_movementState == MovementState.Crouching);
        _playerAnimationController.SetIsSprintingState(_movementState == MovementState.Sprinting || _movementState == MovementState.AirSprinting);
    }

    private MovementState EvaluateCurrentMovementState()
    {
        // Sprint if input is given and player is not crouching or they can stand up
        if (_isGivingSprintInput && (!_isCrouching || CanStandUp()))
        {
            return _isGrounded ? MovementState.Sprinting : MovementState.AirSprinting;
        }

        if (_isCrouching)
        {
            return _isGrounded ? MovementState.Crouching : MovementState.AirCrouching;
        }

        return _isGrounded ? MovementState.Walking : MovementState.AirWalking;
    }

    #endregion

    #region Jumping

    private void JumpFallUpdate()
    {
        if (_isGivingJumpInput && _velocity.y >= 0)
        {
            // No multiplier while holding jump down
            _currentJumpFallMultiplier = 1;
        }
        else
        {
            // Multiply fall as soon as jump is not pressed or jump peak has been reached
            _currentJumpFallMultiplier = _jumpFallMultiplier;
        }
    }

    private void TryJump()
    {
        // Specifically check that the player is gorunded for jump
        if (_isGroundedForJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Update Jump fall multiplier
        JumpFallUpdate();
        SetVelocityY(GetNeededVelocityYForHeight(_jumpHeight));
    }

    /// <summary>
    /// Calculates the needed velocity to reacch
    /// </summary>
    /// <returns></returns>
    private float GetNeededVelocityYForHeight(float height)
    {
        return Mathf.Sqrt(height * -2f * GetTotalGravity().y);
    }

    #endregion

    #region Velocity

    private void AddCurrentMovementAsVelocity()
    {
        AddVelocity(GetCurrentInputMovementVector() * _currentMovementSpeed);
    }

    private void AddVelocity(Vector3 velocityVector)
    {
        _velocity += velocityVector;
    }

    private void AddVelocity(float velocityX, float velocityY, float velocityZ)
    {
        AddVelocity(new Vector3(velocityX, velocityY, velocityZ));
    }

    private void SetVelocity(Vector3 velocityVector)
    {
        _velocity = velocityVector;
    }

    private void SetVelocity(float velocityX, float velocityY, float velocityZ)
    {
        SetVelocity(new Vector3(velocityX, velocityY, velocityZ));
    }

    private void SetVelocityY(float velocityY)
    {
        _velocity.y = velocityY;
    }

    #endregion

    #region Gravity

    /// <summary>
    /// Returns the total amount of gravity that affects this player. Result is all the multipliers added to the base gravity vector.
    /// </summary>
    private Vector3 GetTotalGravity()
    {
        return Physics.gravity * _gravityMultiplier * _currentJumpFallMultiplier;
    }

    #endregion

    #region Character Controller Moving

    /// <summary>
    /// Moves this player by the given amount at the end of the update cycle.
    /// </summary>
    /// <param name="movementVector">Vector in worldspace to move the player in.</param>
    private void Move(Vector3 movementVector)
    {
        _nextMoveVector += movementVector;
    }

    /// <summary>
    /// Final calculation of the total amount to move the player character controller this frame. Adding in velocities and movements together.
    /// </summary>
    private void MovementUpdate()
    {
        // Gravity's and Ground's effect on velocity
        if (_isGrounded)
        {
            if (_velocity.y < 0)
            {
                // Velocity reset when grounded fully
                SetVelocity(Vector3.zero);
            }
        }
        else
        {
            // Add gravity acceleration when not grounded
            AddVelocity(GetTotalGravity() * Time.deltaTime);
        }

        // Accelerate/Decelerate movement
        AccelerateMoveVector();

        // Apply player's movement when grounded
        if (_isGrounded)
        {
            Move(GetDeltaMovementFromInput());
        }
        else // Apply velocity to player according to input
        {
            // Air control
            Vector3 clampedVelocity = _velocity + GetCurrentInputMovementVector() * _airControlSpeed * Time.deltaTime;
            clampedVelocity.y = 0;

            // Clamp velocity if player is about to gain speed faster than allowed
            if (clampedVelocity.magnitude > _currentMovementSpeed)
            {
                clampedVelocity = clampedVelocity.normalized * _currentMovementSpeed;
            }

            // Set x and z only to be safe
            SetVelocity(clampedVelocity.x, _velocity.y, clampedVelocity.z);
        }

        // Checking for standing up
        if (_isCrouching && !_isGivingCrouchInput)
        {
            TryStandUp();
        }

        // Apply velocity delta
        Move(_velocity * Time.deltaTime);

        // Snap to ground if possible
        SnapToGround();

        // Move X and Z in animations
        Vector3 velocityFromOrientation = _orientationTransform.InverseTransformVector(_characterController.velocity);
        _playerAnimationController.SetMoveXZFloats(velocityFromOrientation.x, velocityFromOrientation.z);

        // Apply the final accumulated move vector
        _characterController.Move(_nextMoveVector);

        // Reset move vector
        _nextMoveVector = Vector3.zero;
    }

    /// <summary>
    /// Checks if there is anything blocking the stand up position. If nothing blocks it, <c>isCrouching</c> gets set to false and movement state is updated.
    /// </summary>
    private void TryStandUp()
    {
        // Only allow to disable crouching when not being blocked from the check position
        if (CanStandUp())
        {
            _isCrouching = false;
            UpdatePlayerMovementState();
        }
    }

    private bool CanStandUp()
    {
        return Physics.OverlapBox(_crouchStandUpCheckTransform.position + crouchStandUpCheckBoxOffset, _crouchStandUpCheckBoxHalves, Quaternion.identity, _whatIsGround).Length < 1;
    }

    private void SnapToGround()
    {
        if (!_isGrounded || _velocity.y > 0)
            return;

        Move(Vector3.down * _snapToGroundDistance);
    }

    /// <summary>
    /// Accelerates/Decelerates the <c>smoothMoveVector</c> according to the <c>GetCurrentInputMovementVector()</c> value. Multiplied with TimeManager.deltaTime!
    /// </summary>
    private void AccelerateMoveVector()
    {
        if (!_isGrounded)
        {
            // If player is in air, velocity already smooths the player's movement
            _smoothMoveVector.x = _characterController.velocity.x;
            _smoothMoveVector.z = _characterController.velocity.z;
            return;
        }

        Vector3 readMoveVector = GetCurrentInputMovementVector();

        float vectorDot = Vector3.Dot(readMoveVector.normalized, _smoothMoveVector.normalized);

        // If positive dot, use it. If negative dot, Use complement fraction by adding it to 1.
        float accelerationMultiplier = vectorDot >= 0 ? vectorDot : 1 + vectorDot;

        // Get the complement fraction of acceleration and use it for deceleration
        float decelerationMultiplier = 1f - accelerationMultiplier;

        // Total movement smoothing speed multiplier
        float totalAccDecMultiplier = accelerationMultiplier * _movementAcceleration + decelerationMultiplier * _movementDeceleration;

        // Move smooth move vector towards given input in worldspace multiplied with movement speed
        _smoothMoveVector = Vector3.MoveTowards(_smoothMoveVector, readMoveVector * _currentMovementSpeed, Time.deltaTime * totalAccDecMultiplier);
    }

    /// <summary>
    /// Returns this frame specific movement from player input while walking. Multiplies the normalized input movement vector with walk speed and delta time.
    /// </summary>
    private Vector3 GetDeltaMovementFromInput()
    {
        return _smoothMoveVector * Time.deltaTime;
    }

    /// <summary>
    /// Returns the normalized movement input vector in worldspace according to the orientation of the player.
    /// </summary>
    private Vector3 GetCurrentInputMovementVector()
    {
        return _orientationTransform.forward * _moveInput.y + _orientationTransform.right * _moveInput.x;
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        // Jump detection area
        if (_groundCheckTransform != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(_groundCheckTransform.position + _groundCheckBoxOffset, _groundCheckBoxSizeHalves * 2);
        }

        // Stand up check
        if (_crouchStandUpCheckTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_crouchStandUpCheckTransform.position + crouchStandUpCheckBoxOffset, _crouchStandUpCheckBoxHalves * 2);
        }
    }

    #endregion

    #region Updates

    private void Update()
    {
        // Camera movement and orientation
        PlayerLookUpdate();

        // Update grounded state
        UpdateIsGrounded();

        // Possible Jump addition
        JumpFallUpdate();

        MovementUpdate();

        float xzVelocityMagnitude = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z).magnitude;

        // Animator update with player velocity
        _playerAnimationController.SetTargetVelocityFloat(xzVelocityMagnitude);

        // Debug text
        _velocityDebug.text = string.Format("XYZ: {0:0.00}\nXZ: {1:0.00}", _characterController.velocity.magnitude, xzVelocityMagnitude);
    }

    #endregion
}
