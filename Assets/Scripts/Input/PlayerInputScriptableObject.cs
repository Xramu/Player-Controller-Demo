using RamuInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputController", menuName = "ScriptableObjects/PlayerInputController")]
public class PlayerInputScriptableObject : ScriptableObject
{
    private PlayerInputActions _playerInputActions;

    #region Character Controls Actions

    private UnityAction<Vector2> _onMoveInputAction;
    private UnityAction<Vector2> _onLookInputMouseAction;
    private UnityAction<Vector2> _onLookInputGamepadAction;

    private UnityAction<bool> _onCrouchInputAction;
    private UnityAction<bool> _onSprintInputAction;
    private UnityAction<bool> _onJumpInputAction;

    private UnityAction<bool> _onFreeLookInputAction;

    #endregion

    #region Camera Controls Actions

    private UnityAction<bool> _onCameraModeSwitchInput;

    #endregion

    private void OnEnable()
    {
        InitializeInputs();
    }

    private void InitializeInputs()
    {
        _playerInputActions = new PlayerInputActions();

        // Move
        _playerInputActions.CharacterControls.MoveInput.performed += OnMoveInput;
        _playerInputActions.CharacterControls.MoveInput.canceled += OnMoveInput;

        // Look Mouse
        _playerInputActions.CharacterControls.LookInputMouse.performed += OnLookInputMouse;
        _playerInputActions.CharacterControls.LookInputMouse.canceled += OnLookInputMouse;

        // Look Gamepad
        _playerInputActions.CharacterControls.LookInputGamepad.performed += OnLookInputGamepad;
        _playerInputActions.CharacterControls.LookInputGamepad.canceled += OnLookInputGamepad;

        // Crouch
        _playerInputActions.CharacterControls.Crouch.performed += OnCrouchInput;

        // Sprint 
        _playerInputActions.CharacterControls.Sprint.performed += OnSprintInput;

        // Jump
        _playerInputActions.CharacterControls.Jump.performed += OnJumpInput;

        // Free look
        _playerInputActions.CharacterControls.FreeLook.performed += OnFreeLookInput;

        // Camera mode switch
        _playerInputActions.CameraControls.CameraModeSwitch.performed += OnCameraModeSwitchInput;

        // Enable ccontrols for now
        _playerInputActions.Enable();
    }

    #region Callback Input Methods

    protected void OnMoveInput(InputAction.CallbackContext context) { _onMoveInputAction?.Invoke(context.ReadValue<Vector2>()); }

    private void OnLookInputMouse(InputAction.CallbackContext context) { _onLookInputMouseAction?.Invoke(context.ReadValue<Vector2>()); }

    private void OnLookInputGamepad(InputAction.CallbackContext context) { _onLookInputGamepadAction?.Invoke(context.ReadValue<Vector2>()); }

    private void OnCrouchInput(InputAction.CallbackContext context) { _onCrouchInputAction?.Invoke(context.ReadValueAsButton()); }

    private void OnSprintInput(InputAction.CallbackContext context) { _onSprintInputAction?.Invoke(context.ReadValueAsButton()); }

    private void OnJumpInput(InputAction.CallbackContext context) { _onJumpInputAction?.Invoke(context.ReadValueAsButton()); }
    
    private void OnFreeLookInput(InputAction.CallbackContext context) { _onFreeLookInputAction?.Invoke(context.ReadValueAsButton()); }

    private void OnCameraModeSwitchInput(InputAction.CallbackContext context) { _onCameraModeSwitchInput?.Invoke(context.ReadValueAsButton()); }

    #endregion

    #region Subscription Methods

    #region Add

    public void AddOnMoveInput(UnityAction<Vector2> action)
    {
        _onMoveInputAction -= action;
        _onMoveInputAction += action;
    }

    public void AddOnLookInputMouse(UnityAction<Vector2> action)
    {
        _onLookInputMouseAction -= action;
        _onLookInputMouseAction += action;
    }

    public void AddOnLookInputGamepad(UnityAction<Vector2> action)
    {
        _onLookInputGamepadAction -= action;
        _onLookInputGamepadAction += action;
    }

    public void AddOnCrouchInput(UnityAction<bool> action)
    {
        _onCrouchInputAction -= action;
        _onCrouchInputAction += action;
    }

    public void AddOnSprintInput(UnityAction<bool> action)
    {
        _onSprintInputAction -= action;
        _onSprintInputAction += action;
    }

    public void AddOnJumpInput(UnityAction<bool> action)
    {
        _onJumpInputAction -= action;
        _onJumpInputAction += action;
    }

    public void AddFreeLookInput(UnityAction<bool> action)
    {
        _onFreeLookInputAction -= action;
        _onFreeLookInputAction += action;
    }

    public void AddOnCameraModeSwitch(UnityAction<bool> action)
    {
        _onCameraModeSwitchInput -= action;
        _onCameraModeSwitchInput += action;
    }

    #endregion

    #region Remove

    public void RemoveOnMoveInput(UnityAction<Vector2> action) { _onMoveInputAction -= action; }

    public void RemoveOnLookInputMouse(UnityAction<Vector2> action) { _onLookInputMouseAction -= action; }

    public void RemoveOnLookInputGamepad(UnityAction<Vector2> action) { _onLookInputGamepadAction -= action; }

    public void RemoveOnCrouchInput(UnityAction<bool> action) { _onCrouchInputAction -= action; }

    public void RemoveOnSprintInput(UnityAction<bool> action) { _onSprintInputAction -= action; }

    public void RemoveOnJumpInput(UnityAction<bool> action) { _onJumpInputAction -= action; }

    public void RemoveFreeLookInput(UnityAction<bool> action) { _onFreeLookInputAction -= action; }

    public void RemoveOnCameraModeSwitch(UnityAction<bool> action) { _onCameraModeSwitchInput -= action; }

    #endregion

    #endregion
}
