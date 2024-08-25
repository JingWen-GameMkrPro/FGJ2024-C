/*using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [HideInInspector] public bool isColliding = false;

    public GameObject targetObject; // Assign this in the Unity Inspector
    public float projectileSpeed = 10f; // Speed of the object when it moves
    public float destroyDelay = 7f; // Time before the object is deactivated

    private bool isActive = false;

    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)
            {
                DeactivateTarget();
            }
            else
            {
                ActivateTarget();
            }
        }
    }

    // Public method to activate the target object and move it towards the mouse click direction
    public void ActivateTarget()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Activate the target object
            isActive = true; // Set the active state to true
            MoveObjectTowardsMouse(); // Move the object towards the mouse click direction
            StartCoroutine(DeactivateTargetAfterDelay(destroyDelay)); // Schedule deactivation
        }
        else
        {
            Debug.LogWarning("targetObject is not assigned in the Inspector.");
        }
    }

    // Public method to deactivate the target object
    public void DeactivateTarget()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false); // Deactivate the target object
            isActive = false; // Set the active state to false
        }
    }

    // Method to move the target object towards the mouse click direction
    public void MoveObjectTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Reset z-axis to 0 as it's a 2D game

        // Calculate direction from the object to the mouse position
        Vector2 direction = (mousePosition - targetObject.transform.position).normalized;

        // Apply force to the target object to move it
        Rigidbody2D rb = targetObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }

    // Coroutine to deactivate the target object after a delay
    private IEnumerator DeactivateTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        DeactivateTarget(); // Deactivate the target object
    }
}
*/




/*
using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject fireball; // 这是火球母体对象，用于激活和发射
    public float fireballSpeed = 10f; // 火球移动速度
    public float destroyDelay = 7f; // 火球存在的时间

    private bool isActive = false;

    void Update()
    {
        // 检查是否按下 F 键
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isActive)
            {
                DeactivateFireball();
            }
            else
            {
                ActivateFireball();
            }
        }

        // 检查是否点击鼠标左键，并且火球母体是激活状态
        if (isActive && Input.GetMouseButtonDown(0))
        {
            ShootFireball();
        }
    }

    // 激活火球母体的方法
    public void ActivateFireball()
    {
        if (fireball != null)
        {
            fireball.SetActive(true); // 激活火球母体对象
            isActive = true; // 设置状态为激活
        }
        else
        {
            Debug.LogWarning("Fireball object is not assigned in the Inspector.");
        }
    }

    // 禁用火球母体的方法
    public void DeactivateFireball()
    {
        if (fireball != null)
        {
            fireball.SetActive(false); // 禁用火球母体对象
            isActive = false; // 设置状态为未激活
        }
    }

    // 发射火球的方法
    public void ShootFireball()
    {
        if (fireball != null)
        {
            // 获取鼠标在世界坐标中的位置
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 确保Z轴为0，因为这是2D游戏

            // 计算火球发射的方向
            Vector2 direction = (mousePosition - fireball.transform.position).normalized;

            // 创建火球副本并设置其位置
            GameObject fireballInstance = Instantiate(fireball, fireball.transform.position, Quaternion.identity);
            
            // 确保火球副本激活并且火球母体不受影响
            fireballInstance.SetActive(true);
            fireballInstance.GetComponent<SpriteRenderer>().enabled = true; // 显示火球副本

            // 为火球副本添加速度，使其朝向鼠标点击的方向移动
            Rigidbody2D rb = fireballInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * fireballSpeed;
            }

            // 启动协程，在指定时间后销毁火球副本
            StartCoroutine(DestroyObjectAfterDelay(fireballInstance, destroyDelay));
        }
    }

    // 协程：在指定延迟后销毁对象
    private IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定时间
        Destroy(obj); // 销毁对象
    }
}
*/

using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject fireballPrefab; // 火球预制体
    public Transform fireballAnchor; // 火球锚点（空物体，作为火球母体的挂点）
    public float fireballSpeed = 10f; // 火球移动速度
    public float destroyDelay = 7f; // 火球存在的时间

    private bool isFireballActive = false; // 火球是否激活

    void Update()
    {
        // 检查是否按下 F 键
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isFireballActive)
            {
                DeactivateFireball();
            }
            else
            {
                ActivateFireball();
            }
        }

        // 检查是否点击鼠标左键，并且火球处于激活状态
        if (isFireballActive && Input.GetMouseButtonDown(0))
        {
            ShootFireball();
        }

        // 更新火球母体的位置，以应对角色翻转
        UpdateFireballAnchorPosition();
    }

    // 激活火球的方法
    public void ActivateFireball()
    {
        if (fireballPrefab != null && fireballAnchor != null)
        {
            fireballPrefab.SetActive(true); // 激活火球预制体（母体）
            isFireballActive = true; // 设置状态为激活
        }
        else
        {
            Debug.LogWarning("Fireball prefab or anchor point is not assigned in the Inspector.");
        }
    }

    // 禁用火球的方法
    public void DeactivateFireball()
    {
        if (fireballPrefab != null)
        {
            fireballPrefab.SetActive(false); // 禁用火球预制体（母体）
            isFireballActive = false; // 设置状态为未激活
        }
    }

    // 发射火球的方法
    public void ShootFireball()
    {
        if (fireballPrefab != null && fireballAnchor != null)
        {
            // 获取鼠标在世界坐标中的位置
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // 确保Z轴为0，因为这是2D游戏

            // 计算火球发射的方向
            Vector2 direction = (mousePosition - fireballAnchor.position).normalized;

            // 创建火球副本并设置其位置
            GameObject fireballInstance = Instantiate(fireballPrefab, fireballAnchor.position, Quaternion.identity);

            // 激活火球副本
            fireballInstance.SetActive(true);

            // 为火球副本添加速度，使其朝向鼠标点击的方向移动
            Rigidbody2D rb = fireballInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * fireballSpeed;
            }

            // 启动协程，在指定时间后销毁火球副本
            StartCoroutine(DestroyObjectAfterDelay(fireballInstance, destroyDelay));
        }
    }

    // 协程：在指定延迟后销毁对象
    private IEnumerator DestroyObjectAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定时间
        Destroy(obj); // 销毁对象
    }

    // 更新火球母体位置以应对角色翻转
    private void UpdateFireballAnchorPosition()
    {
        if (fireballAnchor != null)
        {
            // 使火球锚点的位置和角色头部的位置保持一致
            fireballAnchor.localPosition = new Vector3(
                Mathf.Abs(fireballAnchor.localPosition.x) * Mathf.Sign(transform.localScale.x),
                fireballAnchor.localPosition.y,
                fireballAnchor.localPosition.z
            );
        }
    }
}
