using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerAction
{
    // The player movement speed.
    [SerializeField] private float playerSpeed = 2.0f;

    // The time limit for the invulnerability state of the player.
    //[SerializeField] private float immuneTimer = 3f;

    private Rigidbody2D rb;

    private Vector2 movement = Vector2.zero;

    // Necessary for maintaining computing the direction of the character.
    private float originalXScale;
    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originalXScale = transform.localScale.x;
    }

    void Update()
    {
        if (!CanAct())
        {
            rb.velocity = Vector2.zero;
            return;
        }
        ProcessInput();
    }

    private void ProcessInput()
    {
        float xVelocity = movement.x * playerSpeed;
        float yVelocity = rb.velocity.y;
        playerData.movementSpeed.value = Mathf.Abs(xVelocity);

        // If the direction of the player and the velocity does not match,
        // flip the character.
        if (xVelocity * direction < 0 && Time.timeScale > 0) FlipCharacter();

        // Apply the velocity to the rigidbody.
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void FlipCharacter()
    {
        // Flip the direction of the character.
        direction *= -1;

        // Retrieve the current scale.
        Vector3 scale = transform.localScale;

        // Set the new direction using the original scale and the current direction.
        scale.x = originalXScale * direction;

        // Reset the local scale taking the newly calculated direction.
        transform.localScale = scale;
    }

    /// <summary>
    /// Method to be registered on the PlayerInput component.
    /// </summary>
    /// <param name="context">Contains the movement values.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
