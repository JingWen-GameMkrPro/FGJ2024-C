/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStater : MonoBehaviour
{
    [HideInInspector]
    public bool isColliding = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning(collision.gameObject.name);

        if (collision.gameObject.name == "Player 1")
        {
            HealthController h = collision.gameObject.GetComponent<HealthController>();
            if (h != null)
            {
                h.Hit();
            }
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameController.Instance.monsterController.TakeDamaged(10);
            print(GameController.Instance.monsterController.currentHP);
            isColliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isColliding = false;
        }
    }
}*/

//TECHER

using System.Collections;
using UnityEngine;

public class ColliderStater : MonoBehaviour
{
    [HideInInspector]
    public bool isColliding = false;

    public GameObject targetObject; // Assign this in the Unity Inspector

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning(collision.gameObject.name);

        if (collision.gameObject.name == "Player 1")
        {
            HealthController h = collision.gameObject.GetComponent<HealthController>();
            if (h != null)
            {
                h.Hit();
                StartCoroutine(ActivateAndDeactivateTarget(0.5f)); // Start coroutine
            }
        }
        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameController.Instance.monsterController.TakeDamaged(10);
            print(GameController.Instance.monsterController.currentHP);
            isColliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isColliding = false;
        }
    }

    private IEnumerator ActivateAndDeactivateTarget(float delay)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Activate the target object
            yield return new WaitForSeconds(delay); // Wait for specified time
            targetObject.SetActive(false); // Deactivate the target object
        }
    }
}

