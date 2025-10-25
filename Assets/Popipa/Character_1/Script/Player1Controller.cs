using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player1Controller : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.15f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private bool facingRight = true;
    private bool isRunning;
    private bool jumpQueued;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevents tipping over
    }

    void Update()
    {
        // Queue jump input for physics step
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            jumpQueued = true;

        HandleAnimations();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();

        Debug.Log("RB velocity: " + rb.linearVelocity);
    }

    void HandleMovement()
    {
        float moveInput = 0f;

        // --- MOVEMENT INPUTS ---
        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1f;
            isRunning = Input.GetKey(KeyCode.LeftShift);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f;
            isRunning = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            isRunning = false;
        }

        // --- APPLY MOVEMENT ---
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInput * targetSpeed, rb.linearVelocity.y);

        // --- FLIP SPRITE ---
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();
    }

    void HandleJump()
    {
        // --- EXECUTE JUMP ---
        if (jumpQueued && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim?.SetBool("Jump", true);
            jumpQueued = false;
        }

        // --- RESET JUMP WHEN GROUNDED ---
        if (IsGrounded())
            anim?.SetBool("Jump", false);
    }

    bool IsGrounded()
    {
        // Simple circle overlap at feet
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        return isGrounded;
    }

    void HandleAnimations()
    {
        bool movingHorizontally = Mathf.Abs(rb.linearVelocity.x) > 0.05f;

        if (movingHorizontally && !isRunning)
        {
            anim?.SetBool("Walk", true);
            anim?.SetBool("Run", false);
        }
        else if (movingHorizontally && isRunning)
        {
            anim?.SetBool("Run", true);
            anim?.SetBool("Walk", false);
        }
        else
        {
            anim?.SetBool("Walk", false);
            anim?.SetBool("Run", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    // Optional animation helpers
    public void Dead(bool state) => anim?.SetBool("Dead", state);
    public void Attack(bool state) => anim?.SetBool("Attack", state);

    // Debug Gizmo for ground check
    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
