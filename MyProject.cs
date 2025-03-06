using UnityEngine;
public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5000f;                                                 
    public bool isFacingRight = true;
    public bool isJumping = false;
    public bool isGrounded = true;
    public float jumpForce = 400f;


    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer; 

    private float moveInput;
    public LayerMask groundLayer;
    public int countJump;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the GameObject.");
        }
    }

    void FixedUpdate() 
    {
        if (rb != null)
        {
            moveInput = Input.GetAxisRaw("Horizontal");

            float horizontalMovement = moveInput * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);

            Flip();
        }
    }

    void Update()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayer);

        if (groundHit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            countJump = 0;
        }
            Jump();
    }

    private void Flip()
    {
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Jump()
    {

        if (countJump < 1 && Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.AddForce(transform.up * jumpForce);
            countJump++;
        }
    }
}
