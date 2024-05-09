using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : CharacterAnimationController
{
    #region Animator Parameter Name Constants

    private const string VELOCITY_ANIMATOR_FLOAT = "velocity";

    #endregion

    #region Inspector

    [Header("Player Animation Values")]

    [SerializeField] private float _velocityForMaxAnimation = 10.0f;

    [Space]

    [SerializeField] private float _velocityFloatSlideSpeed = 10.0f;

    [Space]

    [SerializeField] private float _headLookSpeed = 5.0f;

    [SerializeField] private float _headLookMinDistanceSpeedMultiplier = 0.2f;

    #endregion

    #region Private

    private Transform _lookTarget;

    private Vector3 _smoothLookPosition;

    private bool _hasFreeLookOn = false;

    private float _targetVelocityFloat;
    private float _currentVelocityFloat;

    private int _velocityHash;

    #endregion

    protected override void Awake()
    {
        base.Awake();

        _velocityHash = Animator.StringToHash(VELOCITY_ANIMATOR_FLOAT);
    }

    public void SetFreeLookBool(bool state)
    {
        if (_hasFreeLookOn == state) return;

        _hasFreeLookOn = state;

        if (_hasFreeLookOn)
        {
            SnapHeadLookToCurrentTarget();
        }
    }

    public void SetTargetVelocityFloat(float value)
    {
        _targetVelocityFloat = value;
    }

    /// <summary>
    /// Controls the head look float of the animators. This should be a float between -1 and 1 of how much the player head is looking up or down.
    /// </summary>
    /// <param name="value">value between -1 and 1 of how much the head is looking from downwards position to upwards position.</param>
    public void SetHeadLookTarget(Transform target, bool instantRotate = false)
    {
        _lookTarget = target;

        if (instantRotate)
        {
            SnapHeadLookToCurrentTarget();
        }
    }

    private void SnapHeadLookToCurrentTarget()
    {
        if (_lookTarget == null) return;
        
        _smoothLookPosition = _lookTarget.position;
    }

    private Vector3 GetLookTargetPosition()
    {
        if (_lookTarget == null)
            return Vector3.zero;

        return _lookTarget.position;
    }

    private void SmoothLookUpdate()
    {
        // Move look target with a speed multiplied by distance or the minimum distance multiplier
        float maxDelta = _headLookSpeed * Time.deltaTime * Mathf.Max(_headLookMinDistanceSpeedMultiplier, Vector3.Distance(_smoothLookPosition, GetLookTargetPosition()));
        _smoothLookPosition = Vector3.MoveTowards(_smoothLookPosition, GetLookTargetPosition(), maxDelta);
    }

    private Vector3 GetIKLookPosition()
    {
        if (_hasFreeLookOn)
        {
            return _smoothLookPosition;
        }

        return GetLookTargetPosition();
    }

    protected void OnAnimatorIK()
    {
        if (_humanoidAnimator)
        {
            // Update free look whenever on
            if (_hasFreeLookOn)
                SmoothLookUpdate();

            _humanoidAnimator.SetLookAtWeight(1);
            _humanoidAnimator.SetLookAtPosition(GetIKLookPosition());
        }
    }

    protected void Update()
    {
        _currentVelocityFloat = Mathf.MoveTowards(_currentVelocityFloat, _targetVelocityFloat, _velocityFloatSlideSpeed * Time.deltaTime);

        SetFloatInAllAnimators(_velocityHash, Mathf.Clamp(_currentVelocityFloat / _velocityForMaxAnimation, 0f, 1f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(GetIKLookPosition(), 0.1f);
    }
}
