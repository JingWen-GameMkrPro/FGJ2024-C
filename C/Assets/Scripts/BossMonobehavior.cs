using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //魔王難易度調整點
    public float walkSpeed = 2f; // 移動速度
    public float jumpForce = 5f; //跳躍力道
    public float walkTime = 2f; //移動時間
    public float waitTime = 1f; //移動間隔等待時間
    public float directionChangeInterval = 1f; // 方向改變間隔時間



    public ColliderStater ColliderStater;    
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
        directionChangeTimer = directionChangeInterval;

        isWalking = true;
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

        print(ColliderStater.isColliding);
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