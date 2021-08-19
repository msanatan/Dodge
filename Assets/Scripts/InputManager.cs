using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;
    public delegate void StartKeyboardEvent(Vector2 position, float time);
    public event StartKeyboardEvent OnStartKeyboard;
    public delegate void EndKeyboardEvent(Vector2 position, float time);
    public event EndKeyboardEvent OnEndKeyboard;
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
        playerControls.Player.TouchPress.started += ctx => StartTouch(ctx);
        playerControls.Player.TouchPress.canceled += ctx => EndTouch(ctx);
        playerControls.Player.Keyboard.started += ctx => StartKeyboard(ctx);
        playerControls.Player.Keyboard.canceled += ctx => EndKeyboard(ctx);
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            Debug.Log("Screen Coordinates: " + playerControls.Player.TouchPosition.ReadValue<Vector2>());
            OnStartTouch(playerControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(playerControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
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
            OnEndKeyboard(playerControls.Player.Keyboard.ReadValue<Vector2>(), (float)context.time);
        }
    }
}
