using System.Collections;
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
}