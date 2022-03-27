using UnityEngine;

/// <summary>
/// Controls the movement of the bullet on spawn,
/// spawns other necessary stuff such as the anchor
/// point on the ground to attach a line renderer
/// for the trail.
/// </summary>
public class BulletController : MonoBehaviour
{
    // The anchor to be generated.
    [SerializeField] private GameObject anchorPrefab = null;

    // The trail to be generated.
    [SerializeField] private GameObject trailPrefab = null;

    // The score data which is updated when hitting a ball.
    [SerializeField] private PlayerData scoreData = null;

    // The speed of the bullet.
    [SerializeField] private float speed = 15f;

    private Rigidbody2D rb;

    private Transform anchor;

    private LineRenderer trail;

    // Saved collider for the line renderer, it is updated
    // everytime the bullet moves, so that it can also trigger hits.
    private CapsuleCollider2D lineCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        // Also create an anchor point.
        Vector3 anchorPosition = new Vector3(transform.position.x, -3, transform.position.z);
        anchor = Instantiate(anchorPrefab, anchorPosition, Quaternion.identity).transform;

        // Create the trail between the anchor point and the bullet.
        trail = Instantiate(trailPrefab, transform.position, transform.rotation, transform).GetComponent<LineRenderer>();
        lineCollider = trail.gameObject.GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        // The bullet's movement is done via rigidbody, so fixed update is used
        // as it is physics-based.

        // Reset the trail points (mostly, only the bullet should change).
        trail.SetPosition(0, anchor.position);
        trail.SetPosition(1, transform.position);

        // Retrieve the distance between the two points.
        float magnitude = (transform.position - anchor.position).magnitude;

        // And apply that distance as the size of the line collider.
        lineCollider.size = new Vector2(0.1f, magnitude);

        // Also shift the position of the collider so it is between
        // the two points.
        lineCollider.transform.position = anchor.position + (transform.position - anchor.position) / 2;
    }

    void OnDestroy()
    {
        // When destroying the bullet, destroy the other objects as well.
        if (anchor != null) Destroy(anchor.gameObject);
        if (trail != null) Destroy(trail.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Top")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Ball")
        {
            // Add score when hitting a ball.
            scoreData.value += 100;
            Destroy(gameObject);
        }
    }
}
