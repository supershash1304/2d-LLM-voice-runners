using UnityEngine;

public class SimpleTestMove : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(input * 5f, rb.linearVelocity.y);
    }
}
