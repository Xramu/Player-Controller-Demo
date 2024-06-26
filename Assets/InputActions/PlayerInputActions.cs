//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputActions/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace RamuInput
{
    public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""7ff3f4ae-0a45-4fbe-8a61-3caad72a623c"",
            ""actions"": [
                {
                    ""name"": ""MoveInput"",
                    ""type"": ""Value"",
                    ""id"": ""51f9d4e3-caa4-4608-a5db-d7f3e38a6c2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LookInputGamepad"",
                    ""type"": ""Value"",
                    ""id"": ""6e1fa9c2-0426-4ca7-9fa5-af5b817ccb35"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LookInputMouse"",
                    ""type"": ""Value"",
                    ""id"": ""bc4dd2f6-14de-4dcf-97bd-d8b9add40c98"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""d9c16ab6-1e91-49cd-a68d-bdbd152cc4f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""e4985d30-3329-4a7c-bdcd-e6b28635b4d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""79886e2f-8bd9-44ff-8459-aee7e38596d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FreeLook"",
                    ""type"": ""Button"",
                    ""id"": ""9615ff01-679d-4270-b952-ed8a1674520d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dc023770-fa8d-4656-8256-c2cc76114039"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ab5eb34f-2187-4efa-85ed-7410eb0d63cd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fafb6101-ab00-43f2-9178-c74d9688abcb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4785c2af-f3ab-4687-a17f-de771597e54d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""231d24e6-a9b4-47b9-bb1a-49f3180c72ff"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""51407e3d-3018-44d9-9da0-dcd24c10ebb4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c3c1366c-6b4a-4fc7-a6df-2ac551cae8e2"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookInputGamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6916b8a-e417-47bc-acf7-aa9af364007e"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7cb54149-fbac-46de-ac71-621eab81e4ba"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0448ea43-2307-412d-9d52-d14bb399ee6f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd1cf928-8f49-4444-acf3-8535ac80ac64"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0641997a-d614-4859-b7cf-eb28e222a5f6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""919d77e8-903f-408c-81fc-9063496e9c77"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88ae3204-dc49-47a6-84fe-fd539a139d5d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": """",
                    ""action"": ""LookInputMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba9ecf1c-b022-453c-a104-844443c96ca2"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11998b6c-fa81-4a69-954a-c35d5b5017b5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FreeLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraControls"",
            ""id"": ""7e2e0460-9a98-4f21-82e5-49e8e07c0633"",
            ""actions"": [
                {
                    ""name"": ""CameraModeSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""43ce2637-7543-43e2-b236-4c20bd38efa1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eea5fc99-b85c-4081-87cd-725976b59dc4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraModeSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebafffbe-ced9-41ee-88fc-c37d5703ee5b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraModeSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // CharacterControls
            m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
            m_CharacterControls_MoveInput = m_CharacterControls.FindAction("MoveInput", throwIfNotFound: true);
            m_CharacterControls_LookInputGamepad = m_CharacterControls.FindAction("LookInputGamepad", throwIfNotFound: true);
            m_CharacterControls_LookInputMouse = m_CharacterControls.FindAction("LookInputMouse", throwIfNotFound: true);
            m_CharacterControls_Crouch = m_CharacterControls.FindAction("Crouch", throwIfNotFound: true);
            m_CharacterControls_Sprint = m_CharacterControls.FindAction("Sprint", throwIfNotFound: true);
            m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
            m_CharacterControls_FreeLook = m_CharacterControls.FindAction("FreeLook", throwIfNotFound: true);
            // CameraControls
            m_CameraControls = asset.FindActionMap("CameraControls", throwIfNotFound: true);
            m_CameraControls_CameraModeSwitch = m_CameraControls.FindAction("CameraModeSwitch", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // CharacterControls
        private readonly InputActionMap m_CharacterControls;
        private List<ICharacterControlsActions> m_CharacterControlsActionsCallbackInterfaces = new List<ICharacterControlsActions>();
        private readonly InputAction m_CharacterControls_MoveInput;
        private readonly InputAction m_CharacterControls_LookInputGamepad;
        private readonly InputAction m_CharacterControls_LookInputMouse;
        private readonly InputAction m_CharacterControls_Crouch;
        private readonly InputAction m_CharacterControls_Sprint;
        private readonly InputAction m_CharacterControls_Jump;
        private readonly InputAction m_CharacterControls_FreeLook;
        public struct CharacterControlsActions
        {
            private @PlayerInputActions m_Wrapper;
            public CharacterControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveInput => m_Wrapper.m_CharacterControls_MoveInput;
            public InputAction @LookInputGamepad => m_Wrapper.m_CharacterControls_LookInputGamepad;
            public InputAction @LookInputMouse => m_Wrapper.m_CharacterControls_LookInputMouse;
            public InputAction @Crouch => m_Wrapper.m_CharacterControls_Crouch;
            public InputAction @Sprint => m_Wrapper.m_CharacterControls_Sprint;
            public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
            public InputAction @FreeLook => m_Wrapper.m_CharacterControls_FreeLook;
            public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
            public void AddCallbacks(ICharacterControlsActions instance)
            {
                if (instance == null || m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Add(instance);
                @MoveInput.started += instance.OnMoveInput;
                @MoveInput.performed += instance.OnMoveInput;
                @MoveInput.canceled += instance.OnMoveInput;
                @LookInputGamepad.started += instance.OnLookInputGamepad;
                @LookInputGamepad.performed += instance.OnLookInputGamepad;
                @LookInputGamepad.canceled += instance.OnLookInputGamepad;
                @LookInputMouse.started += instance.OnLookInputMouse;
                @LookInputMouse.performed += instance.OnLookInputMouse;
                @LookInputMouse.canceled += instance.OnLookInputMouse;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @FreeLook.started += instance.OnFreeLook;
                @FreeLook.performed += instance.OnFreeLook;
                @FreeLook.canceled += instance.OnFreeLook;
            }

            private void UnregisterCallbacks(ICharacterControlsActions instance)
            {
                @MoveInput.started -= instance.OnMoveInput;
                @MoveInput.performed -= instance.OnMoveInput;
                @MoveInput.canceled -= instance.OnMoveInput;
                @LookInputGamepad.started -= instance.OnLookInputGamepad;
                @LookInputGamepad.performed -= instance.OnLookInputGamepad;
                @LookInputGamepad.canceled -= instance.OnLookInputGamepad;
                @LookInputMouse.started -= instance.OnLookInputMouse;
                @LookInputMouse.performed -= instance.OnLookInputMouse;
                @LookInputMouse.canceled -= instance.OnLookInputMouse;
                @Crouch.started -= instance.OnCrouch;
                @Crouch.performed -= instance.OnCrouch;
                @Crouch.canceled -= instance.OnCrouch;
                @Sprint.started -= instance.OnSprint;
                @Sprint.performed -= instance.OnSprint;
                @Sprint.canceled -= instance.OnSprint;
                @Jump.started -= instance.OnJump;
                @Jump.performed -= instance.OnJump;
                @Jump.canceled -= instance.OnJump;
                @FreeLook.started -= instance.OnFreeLook;
                @FreeLook.performed -= instance.OnFreeLook;
                @FreeLook.canceled -= instance.OnFreeLook;
            }

            public void RemoveCallbacks(ICharacterControlsActions instance)
            {
                if (m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ICharacterControlsActions instance)
            {
                foreach (var item in m_Wrapper.m_CharacterControlsActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_CharacterControlsActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);

        // CameraControls
        private readonly InputActionMap m_CameraControls;
        private List<ICameraControlsActions> m_CameraControlsActionsCallbackInterfaces = new List<ICameraControlsActions>();
        private readonly InputAction m_CameraControls_CameraModeSwitch;
        public struct CameraControlsActions
        {
            private @PlayerInputActions m_Wrapper;
            public CameraControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @CameraModeSwitch => m_Wrapper.m_CameraControls_CameraModeSwitch;
            public InputActionMap Get() { return m_Wrapper.m_CameraControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraControlsActions set) { return set.Get(); }
            public void AddCallbacks(ICameraControlsActions instance)
            {
                if (instance == null || m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Add(instance);
                @CameraModeSwitch.started += instance.OnCameraModeSwitch;
                @CameraModeSwitch.performed += instance.OnCameraModeSwitch;
                @CameraModeSwitch.canceled += instance.OnCameraModeSwitch;
            }

            private void UnregisterCallbacks(ICameraControlsActions instance)
            {
                @CameraModeSwitch.started -= instance.OnCameraModeSwitch;
                @CameraModeSwitch.performed -= instance.OnCameraModeSwitch;
                @CameraModeSwitch.canceled -= instance.OnCameraModeSwitch;
            }

            public void RemoveCallbacks(ICameraControlsActions instance)
            {
                if (m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ICameraControlsActions instance)
            {
                foreach (var item in m_Wrapper.m_CameraControlsActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_CameraControlsActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public CameraControlsActions @CameraControls => new CameraControlsActions(this);
        public interface ICharacterControlsActions
        {
            void OnMoveInput(InputAction.CallbackContext context);
            void OnLookInputGamepad(InputAction.CallbackContext context);
            void OnLookInputMouse(InputAction.CallbackContext context);
            void OnCrouch(InputAction.CallbackContext context);
            void OnSprint(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnFreeLook(InputAction.CallbackContext context);
        }
        public interface ICameraControlsActions
        {
            void OnCameraModeSwitch(InputAction.CallbackContext context);
        }
    }
}
