using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    #region Inspector

    [Header("Player Input Controller")]

    [SerializeField] private PlayerInputScriptableObject _playerInputController;

    [Header("Player Camera")]

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private Transform _playerLookRotationTransform;

    [SerializeField] private LayerMask _whatBlocksCamera;

    [Space]

    [SerializeField] private float _cameraNearClippingThirdPerson = 0.05f;

    [Header("First Person Mode Parameters")]

    [SerializeField] private Transform _firstPersonRotationPoint;
    [SerializeField] private Transform _firstPersonPositionTarget;

    [Header("Third Person Mode Parameters")]

    [SerializeField] private Transform _thirdPersonRotationPoint;
    [SerializeField] private Transform _thirdPersonPositionTarget;

    [Space]

    [SerializeField] private float _thirdPersonSphereCastRadius = 0.15f;

    [SerializeField] private float _thirdPersonMinDistanceBeforeFirstPerson = 0.2f;

    #endregion

    #region Private

    private VoidDelegate _cameraTrackUpdate;


    private float _cameraNearClippingFirstPerson;

    private CameraMode _cameraMode = CameraMode.FirstPerson;

    #endregion

    #region Delegates

    private delegate void VoidDelegate();

    #endregion

    #region Enums

    private enum CameraMode
    {
        FirstPerson,
        ThirdPerson
    }

    #endregion

    private void Awake()
    {
        // Starts in first person, set near clipping value based on the one in start
        _cameraNearClippingFirstPerson = _playerCamera.nearClipPlane;

        // First person by default
        SwitchToFirstPerson();
    }

    private void Start()
    {
        SetupPlayerInputs();
    }

    private void SetupPlayerInputs()
    {
        _playerInputController.AddOnCameraModeSwitch(OnCameraModeSwtichInput);
    }

    private void OnCameraModeSwtichInput(bool state)
    {
        if (state)
            CycleCameraMode();
    }

    private void CycleCameraMode()
    {
        switch (_cameraMode)
        {
            // First to Third
            case CameraMode.FirstPerson:
                SwitchToThirdPerson();
                break;

            // Third to First
            case CameraMode.ThirdPerson:
                SwitchToFirstPerson();
                break;

            default:
                Debug.LogError("ERROR: CameraMode value is not listen in a switch statement!", this);
                break;
        }
    }

    #region First Person

    private void SwitchToFirstPerson()
    {
        _cameraMode = CameraMode.FirstPerson;
        _cameraTrackUpdate = FirstPersonCameraTrackUpdate;
    }

    private void FirstPersonCameraTrackUpdate()
    {
        _playerLookRotationTransform.position = _firstPersonRotationPoint.position;

        _playerCamera.nearClipPlane = _cameraNearClippingFirstPerson;
        _playerCamera.transform.position = _firstPersonPositionTarget.position;
    }

    #endregion

    #region Third Person

    private void SwitchToThirdPerson()
    {
        _cameraMode = CameraMode.ThirdPerson;

        _cameraTrackUpdate = ThirdPersonCameraTrackUpdate;
    }

    private void ThirdPersonCameraTrackUpdate()
    {
        // Look rotation point positioning
        _playerLookRotationTransform.position = _thirdPersonRotationPoint.position;

        // Near clipping
        _playerCamera.nearClipPlane = _cameraNearClippingThirdPerson;

        RaycastHit hit;
        float castDistance = Vector3.Distance(_firstPersonPositionTarget.position, _thirdPersonPositionTarget.position);
        Vector3 castDirection = Vector3.Normalize(_thirdPersonPositionTarget.position - _firstPersonPositionTarget.position);

        // Cast sphere to see for camera blockages
        if (Physics.SphereCast(_firstPersonPositionTarget.position, _thirdPersonSphereCastRadius, castDirection, out hit, castDistance, _whatBlocksCamera, QueryTriggerInteraction.Ignore))
        {
            // Blocked Camera

            // Blocked from too close so fall back to first person and stop execution
            if (hit.distance <= _thirdPersonMinDistanceBeforeFirstPerson)
            {
                FirstPersonCameraTrackUpdate();
                return;
            }
            // Put camera the hit's distance away towards the third person target's direction
            _playerCamera.transform.position = _firstPersonPositionTarget.position + castDirection * hit.distance;
        }
        else
        {
            // Camera not blocked
            _playerCamera.transform.position = _thirdPersonPositionTarget.position;
        }
    }

    #endregion

    #region Updates

    private void Update()
    {
        // Call any possible camera tracking method
        _cameraTrackUpdate?.Invoke();
    }

    #endregion
}
