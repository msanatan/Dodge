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
        inputManager.OnStartTouch += MoveWithTouch;
        inputManager.OnEndTouch += StopMoving;
        inputManager.OnStartKeyboard += MoveWithKeyboard;
        inputManager.OnEndKeyboard += StopMoving;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        inputManager.OnStartTouch -= MoveWithTouch;
        inputManager.OnEndTouch -= StopMoving;
        inputManager.OnStartKeyboard -= MoveWithTouch;
        inputManager.OnEndKeyboard -= StopMoving;
    }

    void MoveWithTouch(Vector2 screenPosition, float time)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));
        worldPosition.z = 0;
        Vector2 moveDirection;
        Debug.Log("World Coordinates: " + worldPosition);

        if (worldPosition.y < verticalThreshold && worldPosition.y > -verticalThreshold)
        {
            // Left or right movement
            if (worldPosition.x > 0)
            {
                moveDirection = new Vector2(1, 0);
            }
            else
            {
                moveDirection = new Vector2(-1, 0);
            }
        }
        else
        {
            // Up or down movement
            if (worldPosition.y > 0)
            {
                moveDirection = new Vector2(0, 1);
            }
            else
            {
                moveDirection = new Vector2(0, -1);
            }
        }

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
