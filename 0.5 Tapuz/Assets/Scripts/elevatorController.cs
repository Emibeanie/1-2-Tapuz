using System.Collections;
using UnityEngine;

public class elevatorController : MonoBehaviour
{
    [SerializeField] GameObject activePlat;
    public float moveSpeed;

    private float leftLimit = -41.1f;
    private float rightLimit = -29f;
    private bool canMove = false;
    private bool isWaiting = false;
    private bool movingRight = false;

    void Update()
    {
        if (canMove && !isWaiting)
        {
            MoveElevator();
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

    void MoveElevator()
    {
        float horizontalMovement = movingRight ? moveSpeed : -moveSpeed;
        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime);

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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;
            collision.transform.SetParent(transform);
            activePlat.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;
            collision.transform.SetParent(null);
            activePlat.SetActive(false);
        }
    }
}
