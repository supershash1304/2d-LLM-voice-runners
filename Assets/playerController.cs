using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // --- Public Inspector Variables ---

    [Header("Movement Settings")]
    public float RunSpeed = 5.0f;
    public float JumpForce = 7.0f;

    // --- Private Variables ---

    private Rigidbody2D rb2d;
    private Animator animator;
    private bool isGrounded = true;

    // --- Unity Life Cycle Methods ---

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Null checks for safety
        if (rb2d == null) Debug.LogError("Rigidbody2D component missing!");
        

        // Set the IsRunning boolean to true immediately in Start() 
        if (animator != null)
        {
            animator.SetBool("IsRunning", true);
        }
    }

    void Update()
    {
        // Continuous Forward Movement (Endless runner logic)
        transform.position += Vector3.right * RunSpeed * Time.deltaTime;

        // Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    // --- Custom Methods ---

    private void Jump()
    {
        // 🚨 CRITICAL FIX: The correct property is rb2d.velocity.
        // It's also good practice to zero out the current Y velocity 
        // before applying the jump force for consistent jump height.
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, 0); 
        
        // Apply the upward force
        rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, JumpForce);
        
        // Set state flag and trigger the animation
        isGrounded = false;
        
        if (animator != null)
        {
            animator.SetTrigger("Jump");
            // Also, you might want to stop the running animation for the jump duration
            animator.SetBool("IsRunning", false);
        }
    }

    // --- Collision Detection for Grounding ---

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            
            // Ensure running animation resumes after landing
            if (animator != null)
            {
                animator.SetBool("IsRunning", true);
            }
        }
    }
}