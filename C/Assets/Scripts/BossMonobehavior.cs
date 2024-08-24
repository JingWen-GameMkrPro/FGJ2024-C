using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float jumpForce = 5f;
    public float walkTime = 2f;
    public float waitTime = 1f;
    public float directionChangeInterval = 1f; // 新增的方向改變間隔時間
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float walkTimer;
    private float waitTimer;
    private bool isWalking;
    private float directionChangeTimer;
    private int currentDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        walkTimer = walkTime;
        waitTimer = waitTime;
        isWalking = true;
        directionChangeTimer = directionChangeInterval;
        currentDirection = Random.Range(0, 2) * 2 - 1; // 初始化方向
    }

    void Update()
    {
        if (isWalking)
        {
            Walk();
        }
        else
        {
            Wait();
        }
    }

    void Walk()
    {
        walkTimer -= Time.deltaTime;
        directionChangeTimer -= Time.deltaTime;

        if (walkTimer <= 0)
        {
            isWalking = false;
            waitTimer = waitTime;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            if (directionChangeTimer <= 0)
            {
                currentDirection = Random.Range(0, 2) * 2 - 1; // 改變方向
                directionChangeTimer = directionChangeInterval; // 重置方向改變計時器
            }

            rb.velocity = new Vector2(walkSpeed * currentDirection, rb.velocity.y);

            if (isGrounded && Random.Range(0, 100) < 10)
            {
                Jump();
            }
        }
    }

    void Wait()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            isWalking = true;
            walkTimer = walkTime;
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}