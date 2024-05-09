using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Inspector

    [SerializeField] protected TextMeshProUGUI velocityText;

    [Header("Animation Controller")]

    [SerializeField] protected PlayerAnimationController playerAnimationController;

    [Header("Player Look")]

    [Tooltip("Transform that will rotate with player's look input.")]
    [SerializeField] protected Transform playerLookTransform;

    [Space]

    [Tooltip("Generic sensitivity multiplier of the player's look.")]
    [SerializeField] protected float genericSensitivity = 1.0f;

    [Tooltip("Horizontal sensitivity multiplier of the player's look.")]
    [SerializeField] protected float horizontalSensitivity = 1.0f;
    [Tooltip("Vertical sensitivity multiplier of the player's look.")]
    [SerializeField] protected float verticalSensitivity = 1.0f;

    [Space]

    [Tooltip("Maximum angle that the player can look vertically.")]
    [SerializeField] protected float clampedYLookMax = 90.0f;
    [Tooltip("Minimum angle that the player can look vertically.")]
    [SerializeField] protected float clampedYLookMin = -90.0f;

    [Header("Player Movement")]

    [Tooltip("Transform that rotates with the player's look and determines the player's movement direction.")]
    [SerializeField] protected Transform orientationTransform;

    [Space]

    [Tooltip("Speed at which the player accelerates when moving on ground")]
    [SerializeField] protected float crouchAccelerationSpeed = 50.0f;

    [Tooltip("Speed at which the player accelerates when moving on ground")]
    [SerializeField] protected float walkAccelerationSpeed = 100.0f;

    [Tooltip("Speed at which the player accelerates when moving on ground")]
    [SerializeField] protected float airAccelerationSpeed = 50.0f;

    [Space]

    [Tooltip("Max speed when the player is crouching")]
    [SerializeField] protected float crouchSpeed = 5.0f;
    [Tooltip("Max speed when the player is walking")]
    [SerializeField] protected float walkSpeed = 10.0f;
    [Tooltip("Max speed when the player is sprinting")]
    [SerializeField] protected float sprintSpeed = 15.0f;

    [Space]

    [Tooltip("Drag on the player while they are grounded.")]
    [SerializeField] protected float groundDrag = 5.0f;
    [Tooltip("Drag on the player while not grounded.")]
    [SerializeField] protected float airDrag = 0f;

    [Header("Player Jumping")]

    [Tooltip("Velocity that the player will jump with.")]
    [SerializeField] protected float jumpHeight = 2;
    [Tooltip("Layers that the player ground detection can detect as being ground.")]
    [SerializeField] protected LayerMask whatIsGround;

    [SerializeField] protected float fallForce = 30;

    [Space]

    [Tooltip("Transform to reference when checking the jump detection area.")]
    [SerializeField] protected Transform jumpCheckTransform;

    [Tooltip("Size of the jump detection area.")]
    [SerializeField] protected Vector3 jumpCheckBoxSize;
    [Tooltip("Offset of the jump detection area of the jump check transform.")]
    [SerializeField] protected Vector3 jumpCheckBoxOffset;

    [Space]

    [Tooltip("Max angle of a slope that the player can walk on.")]
    [SerializeField] protected float maxSlopeAngle = 40f;

    [Tooltip("Dstance that the ray of checking for the floor's normal will go from the jump check transform.")]
    [SerializeField] protected float groundNormalDetectionRange = 0.25f;

    #endregion

    #region Protected

    protected float currentMovementMaxSpeed;

    /// <summary>
    /// Bool stating if there is ground in the player's ground detection area.
    /// </summary>
    protected bool isGrounded = true;

    protected RaycastHit lastGroundHit;
    protected bool isOnSlope = false;

    /// <summary>
    /// Bool stating if the player is giving crouching input currently.
    /// </summary>
    protected bool isGivingCrouchingInput = false;

    /// <summary>
    /// Bool stating if the player is giving sprinting input currently.
    /// </summary>
    protected bool isGivingSprintingInput = false;

    protected bool isGivingJumpInput = false;

    /// <summary>
    /// Current movement state of the player.
    /// </summary>
    protected MovementState movementState = MovementState.Walking;

    /// <summary>
    /// Rigidbody of this player.
    /// </summary>
    protected new Rigidbody rigidbody;

    #endregion

    #region Enums

    protected enum MovementState
    {
        Crouching,
        Walking,
        Sprinting,
        air
    }

    #endregion

    protected void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;

        // Not grounded by default
        SetGrounded(false);
    }

    protected void Update()
    {
        // Look rotation
        UpdatePlayerLookRotation();

        // Inputs
        UpdatePlayerInputs();

        // Movement state and jumping
        UpdatePlayerMovementState();
        CheckForJump();

        // Movement Limiting
        LimitVelocity();

        // Debug
        UpdateVelocityDebug();
    }

    protected void UpdateVelocityDebug()
    {
        if (velocityText != null)
            velocityText.text = rigidbody.velocity.magnitude.ToString();
    }

    protected void FixedUpdate()
    {
        UpdatePlayerMovement();
    }

    private float movementInputX;
    private float movementInputY;
    protected void UpdatePlayerInputs()
    {
        movementInputX = Input.GetAxisRaw("Horizontal");
        movementInputY = Input.GetAxisRaw("Vertical");

        isGivingCrouchingInput = Input.GetButton("Crouch");

        isGivingSprintingInput = Input.GetButton("Sprint");

        isGivingJumpInput = Input.GetButton("Jump");
    }

    #region Player Jumping

    /// <summary>
    /// Updates the <c>isGrounded</c> bool with a physics cast underneath the player to check for ground.
    /// </summary>
    protected void UpdateIsGroundedState()
    {
        // Check for colliders inside the jumping area
        SetGrounded(Physics.OverlapBox(jumpCheckTransform.position + jumpCheckBoxOffset, jumpCheckBoxSize, Quaternion.identity, whatIsGround).Length > 0);
    }

    /// <summary>
    /// Sets the <c>isGrounded</c> bool to the given state. If the given state differs from the previous state, player drag gets updated.
    /// </summary>
    /// <param name="state">New state to set <c>isGrounded</c> to.</param>
    protected void SetGrounded(bool state)
    {
        // Skip if matched
        if (isGrounded == state) return;

        // Update state and set drag accordingly
        isGrounded = state;
        rigidbody.drag = isGrounded ? groundDrag : airDrag;
    }

    /// <summary>
    /// Checks for the player input for jump. If input is gotten the player will try to jump.
    /// </summary>
    protected void CheckForJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
    }

    /// <summary>
    /// Tries to make the player jump. Will fail if the player is not grounded or crouching.
    /// </summary>
    /// <returns>Result of the jump. True if successful, false if the player was crouching or not grounded.</returns>
    protected bool TryJump()
    {
        if (!IsGrounded() || IsCrouching()) return false;

        Jump();
        return true;
    }

    // TODO: Make jumping as a charge up. Jumps on release and charges up when player is holding it
    protected void Jump()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, GetJumpVelocityFromHeight(), rigidbody.velocity.z);
    }

    protected float GetJumpVelocityFromHeight()
    {
        return Mathf.Sqrt(jumpHeight * 2 * -Physics.gravity.y);
    }

    #endregion

    #region Player Look

    private float lookRotationX;
    private float lookRotationY;

    protected void UpdatePlayerLookRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * genericSensitivity * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * genericSensitivity * verticalSensitivity;

        // Vertical clamped
        lookRotationX = Mathf.Clamp(lookRotationX - mouseY, clampedYLookMin, clampedYLookMax);

        // Horizontal
        lookRotationY += mouseX;

        // Rotate player look
        playerLookTransform.localRotation = Quaternion.Euler(lookRotationX, lookRotationY, 0);

        // Rotate player orientation
        orientationTransform.localRotation = Quaternion.Euler(0, lookRotationY, 0);
    }

    #endregion

    #region Player Movement

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public bool IsCrouching()
    {
        return movementState == MovementState.Crouching;
    }

    public bool IsWalking()
    {
        return movementState == MovementState.Walking;
    }

    public bool IsSprinting()
    {
        return movementState == MovementState.Sprinting;
    }

    protected void UpdatePlayerMovementState()
    {
        // Set the evaluated movement state as the current state
        movementState = CheckCurrentMovementState();

        // Set max movement speed by current state
        switch (movementState)
        {
            case MovementState.Crouching:
                currentMovementMaxSpeed = crouchSpeed;
                break;
            case MovementState.Walking:
                currentMovementMaxSpeed = walkSpeed;
                break;
            case MovementState.Sprinting:
                currentMovementMaxSpeed = sprintSpeed;
                break;
            case MovementState.air:
                // Sprint speed while in air
                currentMovementMaxSpeed = isGivingSprintingInput ? sprintSpeed : walkSpeed;
                break;
            default:
                break;
        }

        // Animation controller updates. Skip if no animator controller
        if (playerAnimationController == null)
            return;

        playerAnimationController.SetIsCrouchedState(IsCrouching());
        playerAnimationController.SetIsSprintingState(isGivingSprintingInput && !IsCrouching());
    }

    /// <summary>
    /// Checks player's current state and inputs for movement to determine which movement state the player is in.
    /// </summary>
    /// <param name="updateIsGrounded">Should the <c>UpdateIsGroundedState()</c> be called before the evaluation.</param>
    /// <returns>The evaluated movement state the player is in currently.</returns>
    protected MovementState CheckCurrentMovementState(bool updateIsGrounded = true)
    {
        if (updateIsGrounded)
            UpdateIsGroundedState();

        // Always in air if not grounded
        if (!isGrounded)
            return MovementState.air;

        // Priotitize crouching more than sprinting
        if (isGivingCrouchingInput)
            return MovementState.Crouching;

        // Sprinting
        if (isGivingSprintingInput)
            return MovementState.Sprinting;

        // Default to walking
        return MovementState.Walking;
    }

    private Vector3 moveDirection;

    /// <summary>
    /// Fixed update call for applying movement forces to the player according to the movement input axes.
    /// </summary>
    protected void UpdatePlayerMovement()
    {
        // Use ground normal only when grounded and ground angle underneath is found with a slope that isn't too high
        bool useGroundNormal = IsGrounded() && UpdateGroundHit() && GetGroundAngle() <= maxSlopeAngle;

        // Get movement direction with possible slope projections
        moveDirection = GetForwardMovementDirection(useGroundNormal) * movementInputY + GetRightMovementDirection(useGroundNormal) * movementInputX;

        rigidbody.AddForce(moveDirection.normalized * GetAccelerationSpeed(), ForceMode.Acceleration);

        // Add falling force when the player is not giving jump input or when
        if (!IsGrounded() && (!isGivingJumpInput || rigidbody.velocity.y < 0))
        {
            rigidbody.AddForce(Vector3.down * fallForce, ForceMode.Acceleration);
        }
    }

    protected float GetAccelerationSpeed()
    {
        if (IsGrounded())
        {
            return IsCrouching() ? crouchAccelerationSpeed : walkAccelerationSpeed;
        }

        return airAccelerationSpeed;
    }

    /// <summary>
    /// Returns the forward vector direction of the player's current movement orientation.
    /// </summary>
    protected Vector3 GetForwardMovementDirection(bool useLastGroundHit)
    {
        return useLastGroundHit ? Vector3.ProjectOnPlane(orientationTransform.forward, lastGroundHit.normal) : orientationTransform.forward;
    }

    /// <summary>
    /// Returns the right vector direction of the player's current movement orientation.
    /// </summary>
    protected Vector3 GetRightMovementDirection(bool useLastGroundHit)
    {
        return useLastGroundHit ? Vector3.ProjectOnPlane(orientationTransform.right, lastGroundHit.normal) : orientationTransform.right;
    }

    /// <summary>
    /// Limits the player velocity by the current movement state.
    /// </summary>
    protected void LimitVelocity()
    {
        Vector3 xzVelocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if (xzVelocity.magnitude > currentMovementMaxSpeed)
        {
            Vector3 limitedVelocity = xzVelocity.normalized * currentMovementMaxSpeed;
            rigidbody.velocity = new Vector3(limitedVelocity.x, rigidbody.velocity.y, limitedVelocity.z);
        }
    }

    #region Ground point Detection

    /// <summary>
    /// Casts the ray for checking the point on the ground right underneath the player. Saved to <c>lastFloorHit</c>
    /// </summary>
    /// <returns>True if ground was found in the range of the ray.</returns>
    protected bool UpdateGroundHit()
    {
        return Physics.Raycast(jumpCheckTransform.position, -jumpCheckTransform.up, out lastGroundHit, groundNormalDetectionRange, whatIsGround);
    }

    protected float GetGroundAngle()
    {
        return Vector3.Angle(Vector3.up, lastGroundHit.normal);
    }

    #endregion

    #endregion


    #region Gizmos

    private void OnDrawGizmosSelected()
    {
        // Jump area gizmo
        if (jumpCheckTransform != null)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube(jumpCheckTransform.position + jumpCheckBoxOffset, jumpCheckBoxSize * 2);

            Gizmos.DrawLine(jumpCheckTransform.position, jumpCheckTransform.position - jumpCheckTransform.up * groundNormalDetectionRange);
        }

        Gizmos.color= Color.cyan;

        Gizmos.DrawLine(transform.position, transform.position + moveDirection);
    }

    #endregion
}
