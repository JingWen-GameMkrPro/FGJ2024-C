using UnityEngine;

public class MonsterController : MonoBehaviour
{
    MonsterController(LevelDynamicAttribute levelDynamicAttribute)
    {
        this.levelDynamicAttribute = levelDynamicAttribute;
        //�]�������׽վ��I
        this.walkSpeed = levelDynamicAttribute.WalkSpeed; // ���ʳt��
        this.jumpForce = levelDynamicAttribute.JumpForce; //���D�O�D
        this.walkTime = levelDynamicAttribute.WalkTime; //���ʮɶ�
        this.waitTime = levelDynamicAttribute.WaitTime; //���ʶ��j���ݮɶ�
        this.directionChangeInterval = levelDynamicAttribute.DirectionChangeInterval; // ��V���ܶ��j�ɶ�
    }

    private LevelDynamicAttribute levelDynamicAttribute;


    //�]�������׽վ��I
    public float walkSpeed; // ���ʳt��
    public float jumpForce; //���D�O�D
    public float walkTime; //���ʮɶ�
    public float waitTime; //���ʶ��j���ݮɶ�
    public float directionChangeInterval; // ��V���ܶ��j�ɶ�


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