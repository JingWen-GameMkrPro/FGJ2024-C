/*using UnityEngine;

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
                GameController.Instance.combo++;
                canResetJumpCoda = false;
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isGrounded = false;
            GameController.Instance.combo = 0;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitBox"))
        {
            canResetJumpCoda = true;
        }
    }

    
}*/




using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    public int jumpCoda = 0;
    private bool canResetJumpCoda = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        Move();
        Jump();
        //print(jumpCoda+"-----"+canResetJumpCoda);
    }

    void HandleInput()
    {
        if (!Input.anyKey)
        {
            anim.SetBool("mov", false);
            anim.SetBool("jump", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("mov", true);
            FlipCharacter(true); // 水平翻轉
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("mov", true);
            FlipCharacter(false); // 恢復水平方向
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("jump", true);
        }

        if (!Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("jump", false);
        }

    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (jumpCoda > 0 && Input.GetKeyDown(KeyCode.W))
        {
            jumpCoda--;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            jumpCoda = 1;
            isGrounded = true;
            canResetJumpCoda = true;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitBox"))
        {
            if (canResetJumpCoda)
            {
                jumpCoda++;
                GameController.Instance.combo++;
                canResetJumpCoda = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Map"))
        {
            isGrounded = false;
            GameController.Instance.combo = 0;
            anim.SetBool("jump",  false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHitBox"))
        {
            canResetJumpCoda = true;
        }
    }


}
