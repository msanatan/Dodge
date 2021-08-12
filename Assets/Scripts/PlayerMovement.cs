using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] float moveSpeed = 5f;
    Vector3 minScreenBounds;
    Vector3 maxScreenBounds;
    float width;
    float height;
    public UnityEvent gameOverEvent;
    private bool gameOver = false;

    private void Awake()
    {
        minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        playerControls = new PlayerControls();
        SpriteRenderer  spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.bounds.size.x;
        height = spriteRenderer.bounds.size.y;
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            Vector2 currentMovement = playerControls.Player.Movement.ReadValue<Vector2>();
            Vector3 movement = currentMovement * moveSpeed * Time.deltaTime;
            Vector3 currentPosition = transform.position;
            currentPosition += movement;

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
