using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject anchorPrefab;

    [SerializeField] private GameObject trailPrefab;

    [SerializeField] private PlayerData scoreData;

    [SerializeField] private float speed = 15f;

    private Rigidbody2D rb;

    private Transform anchor;

    private LineRenderer trail;

    private CapsuleCollider2D lineCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        Vector3 anchorPosition = new Vector3(transform.position.x, -3, transform.position.z);
        // Also create an anchor point.
        anchor = Instantiate(anchorPrefab, anchorPosition, Quaternion.identity).transform;

        // Create the trail between the anchor point and the bullet.
        trail = Instantiate(trailPrefab, transform.position, transform.rotation, transform).GetComponent<LineRenderer>();
        lineCollider = trail.gameObject.GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        trail.SetPosition(0, anchor.position);
        trail.SetPosition(1, transform.position);

        float magnitude = (transform.position - anchor.position).magnitude;
        lineCollider.size = new Vector2(0.1f, magnitude);
        lineCollider.transform.position = anchor.position + (transform.position - anchor.position) / 2;
    }

    void OnDestroy()
    {
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
            scoreData.value += 100;
            Destroy(gameObject);
        }
    }
}
