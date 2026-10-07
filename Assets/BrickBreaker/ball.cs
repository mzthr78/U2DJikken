using UnityEngine;

public class ball : MonoBehaviour
{
    [SerializeField] GameObject paddle;

    Rigidbody2D rb;
    float speed = 10f;

    float minSpeed = 9f;
    float maxSpeed = 15f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Fire()
    {
        Vector2 direction = (paddle.transform.position - transform.position).normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 direction = rb.linearVelocity.normalized;
        float speed = Mathf.Clamp(rb.linearVelocity.magnitude, minSpeed, maxSpeed);

        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
