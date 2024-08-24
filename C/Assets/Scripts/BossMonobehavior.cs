using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //�]�������׽վ��I
    public float walkSpeed = 2f; // ���ʳt��
    public float jumpForce = 5f; //���D�O�D
    public float walkTime = 2f; //���ʮɶ�
    public float waitTime = 1f; //���ʶ��j���ݮɶ�
    public float directionChangeInterval = 1f; // ��V���ܶ��j�ɶ�



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
        currentDirection = Random.Range(0, 2) * 2 - 1; // ��l�Ƥ�V


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
                currentDirection = Random.Range(0, 2) * 2 - 1; // ���ܤ�V
                directionChangeTimer = directionChangeInterval; // ���m��V���ܭp�ɾ�
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