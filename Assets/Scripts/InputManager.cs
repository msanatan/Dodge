using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public delegate void StartKeyboardEvent(Vector2 position, float time);
    public event StartKeyboardEvent OnStartKeyboard;
    public delegate void EndKeyboardEvent(Vector2 position, float time);
    public event EndKeyboardEvent OnEndKeyboard;
    public delegate void StartTouchJoystickEvent(Vector2 position, float time);
    public event StartKeyboardEvent OnStartTouchJoystick;
    public delegate void EndTouchJoystickEvent(Vector2 position, float time);
    public event EndKeyboardEvent OnEndTouchJoystick;
    PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls.Player.Keyboard.started += ctx => StartKeyboard(ctx);
        playerControls.Player.Keyboard.canceled += ctx => EndKeyboard(ctx);
        playerControls.Player.TouchJoystick.performed += ctx => StartTouchJoystick(ctx);
        playerControls.Player.TouchJoystick.canceled += ctx => EndTouchJoystick(ctx);
    }

    void StartKeyboard(InputAction.CallbackContext context)
    {
        if (OnStartKeyboard != null)
        {
            OnStartKeyboard(playerControls.Player.Keyboard.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    void EndKeyboard(InputAction.CallbackContext context)
    {
        if (OnEndKeyboard != null)
        {
            OnEndKeyboard(Vector2.zero, (float)context.time);
        }
    }

    void StartTouchJoystick(InputAction.CallbackContext context)
    {
        if (OnStartTouchJoystick != null)
        {
            OnStartTouchJoystick(playerControls.Player.TouchJoystick.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    void EndTouchJoystick(InputAction.CallbackContext context)
    {
        if (OnEndTouchJoystick != null)
        {
            OnEndTouchJoystick(Vector2.zero, (float)context.time);
        }
    }
}
