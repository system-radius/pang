using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameEvent killPlayer = null;

    [SerializeField] private GameEvent ballPop = null;

    [SerializeField] private Ball ballPrefab = null;

    [SerializeField] private float yForce = 11.5f;

    [SerializeField] private float xForce = 2.5f;

    [SerializeField] private float minScale = 0.5f;

    private Rigidbody2D rb;

    private float size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        size = transform.localScale.x;
    }

    private void Split()
    {
        size /= 2;
        if (size >= minScale)
        {
            SpawnBall(-xForce, yForce - 1, size);
            SpawnBall(xForce, yForce - 1, size);
        }
        Destroy(gameObject);
    }

    private void SpawnBall(float xForce, float yForce, float size)
    {
        Transform ball = Instantiate(ballPrefab, transform.position, Quaternion.identity, transform.parent).transform;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(xForce, yForce);
        ball.localScale = new Vector2(size, size);

        ball.GetComponent<Ball>().yForce = yForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            rb.velocity = new Vector2(rb.velocity.x, yForce);
        }
        else if (collision.tag == "Left" || collision.tag == "Right")
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        else if (collision.tag == "Top")
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else if (collision.tag == "Player")
        {
            killPlayer.Raise();
        }
        else if (collision.tag == "Bullet")
        {
            ballPop.Raise();
            Split();
        }
    }
}
