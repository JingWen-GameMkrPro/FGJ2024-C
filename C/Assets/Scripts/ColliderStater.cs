using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStater : MonoBehaviour
{
    [HideInInspector]
    public bool isColliding = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.Instance.monsterController.TakeDamaged(10);
            isColliding = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
}
