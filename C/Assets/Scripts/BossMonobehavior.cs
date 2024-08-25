using Fungus.TMProLinkAnimEffects;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public void InitialData()
    {
        this.levelDynamicAttribute = GameController.Instance.levelDynamicAttribute;
        this.walkSpeed = GameController.Instance.levelDynamicAttribute.WalkSpeed; // 移動速度
        this.jumpForce = GameController.Instance.levelDynamicAttribute.JumpForce; //跳躍力道
        this.walkTime = GameController.Instance.levelDynamicAttribute.WalkTime; //移動時間
        this.waitTime = GameController.Instance.levelDynamicAttribute.WaitTime; //移動間隔等待時間
        this.directionChangeInterval = GameController.Instance.levelDynamicAttribute.DirectionChangeInterval; // 方向改變間隔時間
        this.maxBossHP = GameController.Instance.levelDynamicAttribute.MaxBossHP; //最大血量
        this.bossSize = GameController.Instance.levelDynamicAttribute.BossSize; //魔王大小
        this.bossAttack = GameController.Instance.levelDynamicAttribute.BossAttack; //魔王攻擊力
        this.bossResistance = GameController.Instance.levelDynamicAttribute.BossResistance; //魔王抵抗力
        this.levelType = GameController.Instance.levelType;

        currentHP = maxBossHP;
    }

    private LevelDynamicAttribute levelDynamicAttribute;


    //魔王難易度調整點
    public float walkSpeed; // 移動速度
    public float jumpForce; //跳躍力道
    public float walkTime; //移動時間
    public float waitTime; //移動間隔等待時間
    public float directionChangeInterval; // 方向改變間隔時間


    public float bossSize; //魔王大小
    public float maxBossHP; //魔王血量
    public float bossAttack; //魔王攻擊力
    public float bossResistance; //魔王抵抗力

    public LevelDynamicAttribute.LevelType levelType;

    public float currentHP; //目前血量
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