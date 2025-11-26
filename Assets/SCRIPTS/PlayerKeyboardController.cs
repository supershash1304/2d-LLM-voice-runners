using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpForce = 10f;

    Rigidbody2D rb;
    bool grounded;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        // Always move forward
        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            grounded = true;

        if (col.collider.CompareTag("Obstacle"))
            FindObjectOfType<GameManager>().GameOver();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            grounded = false;
    }
}
