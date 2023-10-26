using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatController : MonoBehaviour
{
    public float moveSpeed;
    private float leftLimit = -41.07f;
    private float rightLimit = -30.00299f;

    private bool canMove = false; 

    void Update()
    {
        if (canMove)
        {
           
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= rightLimit)
            {
                moveSpeed = -Mathf.Abs(moveSpeed);
            }
            else if (transform.position.x <= leftLimit)
            {
                moveSpeed = Mathf.Abs(moveSpeed);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;

            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
            collision.transform.SetParent(null);
        }
    }
}
