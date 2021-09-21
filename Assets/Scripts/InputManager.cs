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
