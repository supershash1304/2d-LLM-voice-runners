using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runMultiplier = 1.5f;
    public float jumpForce = 15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator anim;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Animation updates
        anim.SetBool("Jump", !isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Attack example (optional)
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        bool isRunning = (Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(inputX) > 0); // Shift + A/D

        float speed = moveSpeed;
        if (isRunning)
            speed *= runMultiplier;

        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

        // Flip sprite
        if (inputX > 0 && !facingRight)
            Flip();
        else if (inputX < 0 && facingRight)
            Flip();

        // Update animation states
        anim.SetBool("Walk", Mathf.Abs(inputX) > 0.1f && !isRunning && isGrounded);
        anim.SetBool("Run", isRunning && isGrounded);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
