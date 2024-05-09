using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    #region Animator Parameter Name Constants

    private const string IS_GROUNDED_ANIMATOR_BOOL = "isGrounded";
    private const string IS_CROUCHING_ANIMATOR_BOOL = "isCrouching";
    private const string IS_SPRINTING_ANIMATOR_BOOL = "isSprinting";

    private const string MOVE_X_ANIMATOR_FLOAT = "moveX";
    private const string MOVE_Z_ANIMATOR_FLOAT = "moveZ";

    #endregion

    #region Inspector

    [Tooltip("Animator that this character animation controller is connected to.")]
    [SerializeField] private Animator _parentAnimator;

    [Tooltip("Anmator of the possible humanoid in this character")]
    [SerializeField] protected Animator _humanoidAnimator;

    [Header("Character Animation Values")]
    [SerializeField] private float _moveXZFloatMultiplier = 0.5f;

    #endregion

    #region Private

    private int _isGroundedHash;
    private int _isCrouchingHash;
    private int _isSprintingHash;
    private int _moveXHash;
    private int _moveZHash;

    #endregion

    protected virtual void Awake()
    {
        // Hash parameter strings
        _isGroundedHash = Animator.StringToHash(IS_GROUNDED_ANIMATOR_BOOL);
        _isCrouchingHash = Animator.StringToHash(IS_CROUCHING_ANIMATOR_BOOL);
        _isSprintingHash = Animator.StringToHash(IS_SPRINTING_ANIMATOR_BOOL);

        _moveXHash = Animator.StringToHash(MOVE_X_ANIMATOR_FLOAT);
        _moveZHash = Animator.StringToHash(MOVE_Z_ANIMATOR_FLOAT);
    }

    #region Parameter Changes

    /// <summary>
    /// Handles animations for the grounded state change.
    /// </summary>
    /// <param name="state">New state of being grounded.</param>
    public void SetIsGroundedState(bool state)
    {
        SetBoolInAllAnimators(_isGroundedHash, state);
    }

    /// <summary>
    /// Handles animations for the crouch state change.
    /// </summary>
    /// <param name="state">New state of crouching.</param>
    public void SetIsCrouchedState(bool state)
    {
        SetBoolInAllAnimators(_isCrouchingHash, state);
    }

    /// <summary>
    /// Handles animations for the sprint state change.
    /// </summary>
    /// <param name="state">New state of sprinting.</param>
    public void SetIsSprintingState(bool state)
    {
        SetBoolInAllAnimators(_isSprintingHash, state);
    }

    /// <summary>
    /// Sets the movement X and Y floats in the animators.
    /// </summary>
    /// <param name="valueX">Value of Movement X to pass to animator.</param>
    /// <param name="valueZ">Value of Movement Z to pass to animator.</param>
    public void SetMoveXZFloats(float valueX, float valueZ)
    {
        SetFloatInAllAnimators(_moveXHash, valueX * _moveXZFloatMultiplier);
        SetFloatInAllAnimators(_moveZHash, valueZ * _moveXZFloatMultiplier);
    }

    #endregion

    #region Mass Animator Calls

    /// <summary>
    /// Sets a bool parameter with the matching hash to the given state in all the animators this ccontroller handles.
    /// </summary>
    /// <param name="boolNameHash">Hash of the bool parameter's name.</param>
    /// <param name="state">State to set the bool as.</param>
    protected void SetBoolInAllAnimators(int boolNameHash, bool state)
    {
        _parentAnimator?.SetBool(boolNameHash, state);
        _humanoidAnimator?.SetBool(boolNameHash, state);
    }

    /// <summary>
    /// Sets a float parameter with the matching hash to the given value in all the animators this ccontroller handles.
    /// </summary>
    /// <param name="floatNameHash">Hash of the float parameter's name</param>
    /// <param name="value">Value to set the float as.</param>
    protected void SetFloatInAllAnimators(int floatNameHash, float value)
    {
        _parentAnimator?.SetFloat(floatNameHash, value);
        _humanoidAnimator?.SetFloat(floatNameHash, value);
    }

    #endregion
}
