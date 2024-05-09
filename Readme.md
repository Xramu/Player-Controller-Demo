# First-Person and Third-Person Character Controller Demo in Unity 2021.3.32f1
Small demo project where I created a character controller that can be toggled between first and third-person mode.

The demo uses Unity's InputSystem to allow the input types to be decoupled from the character's movement logic. This allows the small demo to be played with a gamepad as well as with a keyboard and a mouse.

### [Check out PlayerCharacterController.cs for the core logic](Assets/Scripts/Player/PlayerCharacterController.cs)
The project includes an outdated test with a rigidbody controller called PlayerController.cs

No build of the project is available since it is just a small playground that I tinkered with.
### [C# scripts can be found in Assets/Scripts](Assets/Scripts)

## Features
- Supports Gamepad as well as Keyboard & Mouse
- Walking, running & sprinting
- Crouching underneath obstacles
- Varying jump height based on the jump input's length
- Animations for walking, running, sprinting, crouching and jumping
- Freelook toggling to allow you to keep your heading but look around in first and third-person
- Smooth acceleration & deceleration dampening the movement
- Smoothed out air control mode when the player is airborne from a jump
