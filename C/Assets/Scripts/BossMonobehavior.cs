using Fungus.TMProLinkAnimEffects;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public void InitialData()
    {
        this.levelDynamicAttribute = GameController.Instance.levelDynamicAttribute;
        print(levelDynamicAttribute);
        this.walkSpeed = GameController.Instance.levelDynamicAttribute.WalkSpeed; // ���ʳt��
        this.jumpForce = GameController.Instance.levelDynamicAttribute.JumpForce; //���D�O�D
        this.walkTime = GameController.Instance.levelDynamicAttribute.WalkTime; //���ʮɶ�
        this.waitTime = GameController.Instance.levelDynamicAttribute.WaitTime; //���ʶ��j���ݮɶ�
        this.directionChangeInterval = GameController.Instance.levelDynamicAttribute.DirectionChangeInterval; // ��V���ܶ��j�ɶ�
        this.maxBossHP = GameController.Instance.levelDynamicAttribute.MaxBossHP; //�̤j��q
        this.bossSize = GameController.Instance.levelDynamicAttribute.BossSize; //�]���j�p
        this.bossAttack = GameController.Instance.levelDynamicAttribute.BossAttack; //�]�������O
        this.bossResistance = GameController.Instance.levelDynamicAttribute.BossResistance; //�]����ܤO


        currentHP = maxBossHP;
    }

    private LevelDynamicAttribute levelDynamicAttribute;


    //�]�������׽վ��I
    public float walkSpeed; // ���ʳt��
    public float jumpForce; //���D�O�D
    public float walkTime; //���ʮɶ�
    public float waitTime; //���ʶ��j���ݮɶ�
    public float directionChangeInterval; // ��V���ܶ��j�ɶ�


    public float bossSize; //�]���j�p
    public float maxBossHP; //�]����q
    public float bossAttack; //�]�������O
    public float bossResistance; //�]����ܤO



    public float currentHP; //�ثe��q
    public ColliderStater ColliderStater;    
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private float walkTimer;
    private float waitTimer;
    private float directionChangeTimer;
    private int currentDirection;
    private bool isGrounded;
    private bool isWalking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        InitialData();

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

    public void TakeDamaged(int value)
    { 
        currentHP -= value;
        
    }
}