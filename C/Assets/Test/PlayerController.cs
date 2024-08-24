/*using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        //isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            print("awdadw");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isGrounded = false;
        }
    }
}
*/
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    bool isTouchingWall = false;
    bool isWalking = false;
    bool isJumping = false;
    bool canJump = true;
    bool canControl = false; // 新添加的变量

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        bool keydn = Input.anyKey;

        if (!keydn)
        {
            anim.SetBool("mov", false);
            anim.SetBool("jump", false);
        }

        if (!Input.GetKey(KeyCode.W) && !canJump)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump && canControl) // 添加了canControl条件
        {
            anim.SetBool("jump", true);
            JumpCharacter(); // 跳躍
        }

        if (Input.GetKey(KeyCode.A) && canControl) // 添加了canControl条件
        {
            anim.SetBool("mov", true);
            MoveCharacter(-1f); // 向左移動
            FlipCharacter(true); // 水平翻轉
        }

        if (Input.GetKey(KeyCode.D) && canControl) // 添加了canControl条件
        {
            anim.SetBool("mov", true);
            MoveCharacter(1f); // 向右移動
            FlipCharacter(false); // 恢復水平方向
        }
    }

    void MoveCharacter(float direction)
    {
        Vector2 targetVelocity = new Vector2(direction * 3f, rb.velocity.y);
        rb.velocity = targetVelocity;
        isWalking = true;
    }

    void JumpCharacter()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 16f);
            canJump = false;
            isJumping = true;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    void FlipCharacter(bool flip)
    {
        if (flip)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        {
            isTouchingWall = true;
            isWalking = false;
            isJumping = false;
        }


        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("jump", false);
            canControl = true; // 当接触地面时，启用控制
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canControl = false; // 当离开地面时，禁用控制
        }
    }
}