using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainedPlat : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator anim;

    //private Vector2 originalPosition;
    //public float moveDistance;
    //public float moveSpeed;

    //private void Start()
    //{
    //    originalPosition = transform.position;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            anim.SetBool("Move", true);
            audioSource.Play();

            //StartCoroutine(MovePlatform());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Move", false);
        }
    }
    //private IEnumerator MovePlatform()
    //{
    //    // Move down
    //    float t = 0f;
    //    Vector2 targetPosition = originalPosition - Vector2.up * moveDistance;

    //    while (t < 0.2f)
    //    {
    //        t += Time.deltaTime * moveSpeed;
    //        transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
    //        yield return null;
    //    }

    //    yield return new WaitForSeconds(0.2f);

    //    //move back up
    //    t = 0f;
    //    while (t < 0.2f)
    //    {
    //        t += Time.deltaTime * moveSpeed;
    //        transform.position = Vector2.Lerp(targetPosition, originalPosition, t);
    //        yield return null;
    //    }
    //    // Ensure the platform is back at the original position
    //    transform.position = originalPosition;
    //}
}
