using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float verticalThreshold = 2.5f;
    [SerializeField] InputManager inputManager;
    public UnityEvent gameOverEvent;
    bool gameOver = false;
    PlayerControls playerControls;
    Vector3 minScreenBounds;
    Vector3 maxScreenBounds;
    Vector3 movement;
    float width;
    float height;

    private void Awake()
    {
        minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        playerControls = new PlayerControls();
        SpriteRenderer  spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x;
        height = spriteRenderer.bounds.size.y;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        inputManager.OnStartKeyboard += MoveWithKeyboard;
        inputManager.OnEndKeyboard += StopMoving;
        inputManager.OnStartTouchJoystick += MoveWithTouchJoystick;
        inputManager.OnEndTouchJoystick += StopMoving;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        inputManager.OnStartKeyboard -= MoveWithKeyboard;
        inputManager.OnEndKeyboard -= StopMoving;
    }

    void MoveWithTouchJoystick(Vector2 moveDirection, float time)
    {
        this.movement = moveDirection;
    }

    void MoveWithKeyboard(Vector2 moveDirection, float time)
    {
        this.movement = moveDirection;
    }

    void StopMoving(Vector2 screenPosition, float time)
    {
        this.movement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !PausedManager.paused)
        {
            Vector3 currentPosition = transform.position;
            currentPosition += this.movement * moveSpeed * Time.deltaTime;

            currentPosition.x = Mathf.Clamp(currentPosition.x, minScreenBounds.x + width / 2, maxScreenBounds.x - width / 2);
            currentPosition.y = Mathf.Clamp(currentPosition.y, minScreenBounds.y + height / 2, maxScreenBounds.y - height / 2);
            transform.position = currentPosition;
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        gameOver = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        gameOverEvent.Invoke();
    }
}
