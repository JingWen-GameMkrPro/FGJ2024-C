/*using UnityEngine;

public class HealthController : MonoBehaviour
{
    public SpriteRenderer healthBarSprite; // 指向血条的 SpriteRenderer
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageAmount = 20f; // 每次扣除的血量
    public GameObject targetObject; // 触发扣血的指定对象

    private bool hasTakenDamage = false; // 是否已经扣血的标记

    void Start()
    {
        currentHealth = maxHealth; // 初始化当前血量
    }

    void Update()
    {
        // 这里可以添加一些其他逻辑，或者用来重置 `hasTakenDamage` 的条件
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否与指定的目标对象发生碰撞
        if (collision.gameObject == targetObject && !hasTakenDamage)
        {
            TakeDamage(damageAmount);
            hasTakenDamage = true; // 标记已经扣血
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 当玩家离开碰撞物体时，重置扣血标记，允许再次扣血
        if (collision.gameObject == targetObject)
        {
            hasTakenDamage = false;
        }
    }

    void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 确保血量不会低于0

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // 计算当前血量的比例
        float healthPercent = currentHealth / maxHealth;

        // 缩放血条 Sprite 的宽度
        healthBarSprite.transform.localScale = new Vector3(healthPercent, 1, 1);
    }
}*/



using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public SpriteRenderer healthBarSprite; // 指向血条的 SpriteRenderer
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageAmount = 20f; // 每次扣除的血量
    public GameObject damageTriggerObject; // 触发扣血的物件
    public GameObject noDamageTriggerObject; // 不触发扣血的物件

    private bool hasTakenDamage = false; // 是否已经扣血的标记

    void Start()
    {
        Debug.Log("HealthController Start()");
        currentHealth = maxHealth; // 初始化当前血量
    }

    void Update()
    {
        // 这里可以添加一些其他逻辑，或者用来重置 hasTakenDamage 的条件
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.LogWarning("OnCollisionEnter2D->"+collision.gameObject.name);
        // 检查是否与触发扣血的物件发生碰撞
        if (collision.gameObject == damageTriggerObject && !hasTakenDamage)
        {
            TakeDamage(damageAmount);
            hasTakenDamage = true; // 标记已经扣血
        }
        // 检查是否与不触发扣血的物件发生碰撞
        else if (collision.gameObject == noDamageTriggerObject)
        {
            // 不触发扣血，不改变 hasTakenDamage
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 当碰撞对象离开碰撞体时，重置扣血标记，允许再次扣血
        if (collision.gameObject == damageTriggerObject)
        {
            hasTakenDamage = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogWarning("OnTriggerEnter2D===>"+other.gameObject.name);
    }

    public void Hit()
    {
        Debug.LogWarning("hit!!!!"); 
    }//99
    
    
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 确保血量不会低于0

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // 计算当前血量的比例
        float healthPercent = currentHealth / maxHealth;

        // 缩放血条 Sprite 的宽度
        healthBarSprite.transform.localScale = new Vector3(healthPercent, 1, 1);
    }
}