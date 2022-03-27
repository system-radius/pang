using UnityEngine;

/// <summary>
/// Ball controller for the behavior when hitting the constraints,
/// splitting apart when hit by bullets, and for killing the player.
/// </summary>
public class Ball : MonoBehaviour
{
    // The event to be triggered on player hit.
    [SerializeField] private GameEvent killPlayer = null;

    // The event to be triggered on bullet hit.
    [SerializeField] private GameEvent ballPop = null;

    // For the splitting mechanic, the ball will spawn two other instances
    // similar to it.
    [SerializeField] private Ball ballPrefab = null;

    // Initial y-force.
    [SerializeField] private float yForce = 11.5f;

    // Initial x-force, this does not change regardless of size.
    [SerializeField] private float xForce = 2.5f;

    // The minimum size of the ball allowed.
    [SerializeField] private float minScale = 0.5f;

    private Rigidbody2D rb;

    // Current size.
    private float size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        size = transform.localScale.x;
    }

    private void Split()
    {
        // Try to decrease the size of the ball down to a minumum value.
        size /= 2;
        if (size >= minScale)
        {
            // Spawn two balls, left and right, distinguished
            // by the applied x force to them.
            SpawnBall(-xForce, yForce - 1, size);
            SpawnBall(xForce, yForce - 1, size);
        }

        // Finally, destroy itself.
        Destroy(gameObject);
    }

    private void SpawnBall(float xForce, float yForce, float size)
    {
        Transform ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform.parent).transform;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(xForce, yForce);
        ball.localScale = new Vector2(size, size);

        // Lower the y-force applied to the ball, as well as the succeeding generations.
        ball.GetComponent<Ball>().yForce = yForce;
    }

    /// <summary>
    /// Collection of collision behavior.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            // Apply the y-force to the ball, then allow it to
            // fall down again since it has a rigidbody.
            rb.velocity = new Vector2(rb.velocity.x, yForce);
        }
        else if (collision.tag == "Left" || collision.tag == "Right")
        {
            // Simply reverse the x-force upon hitting a side wall.
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        else if (collision.tag == "Top")
        {
            // Stop the ball then allow it to fall down.
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        // Ball events.
        else if (collision.tag == "Player")
        {
            killPlayer.Raise();
        }
        else if (collision.tag == "Bullet")
        {
            // Hitting a bullet triggers the split/pop.
            ballPop.Raise();
            Split();
        }
    }
}
