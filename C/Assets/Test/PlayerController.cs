using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    public int jumpCoda = 0;
    public bool canResetJumpCoda = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        print(jumpCoda+"-----"+canResetJumpCoda);
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        //isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
        if (jumpCoda>0 && Input.GetKeyDown(KeyCode.W))
        {
            jumpCoda--;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {

            jumpCoda = 1;
            isGrounded = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitBox"))
        {
            if(canResetJumpCoda)
            {
                jumpCoda ++;
                canResetJumpCoda = false;
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitBox"))
        {

            canResetJumpCoda = true;
        }
    }

    
}