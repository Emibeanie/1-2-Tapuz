using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatController : MonoBehaviour
{
    public float moveSpeed;
    private float leftLimit = -41.07f;
    private float rightLimit = -30.00299f;
    private bool canMove = false;
    private bool isWaiting = false;
    private bool movingRight = false;

    void Update()
    {
        if (canMove)
        {
            if (!isWaiting)
            {
                if (movingRight)
                    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                else
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

                if (transform.position.x <= leftLimit && !movingRight)
                {
                    isWaiting = true;
                    StartCoroutine(WaitForPlayer());
                }
                else if (transform.position.x >= rightLimit && movingRight)
                {
                    isWaiting = true;
                    StartCoroutine(WaitForPlayer());
                }
            }
        }
    }

    IEnumerator WaitForPlayer()
    {
        yield return new WaitUntil(() => !canMove);
        isWaiting = false;

        if (transform.position.x <= leftLimit)
            movingRight = true;
        else if (transform.position.x >= rightLimit)
            movingRight = false;
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
