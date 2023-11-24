using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainedPlat : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private Vector2 originalPosition;
    public float moveDistance;
    public float moveSpeed;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            StartCoroutine(MovePlatform());
        }
    }
    private IEnumerator MovePlatform()
    {
        // Move down
        float t = 0f;
        Vector2 targetPosition = originalPosition - Vector2.up * moveDistance;

        while (t < 0.5f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        //move back up
        t = 0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector2.Lerp(targetPosition, originalPosition, t);
            yield return null;
        }
        // Ensure the platform is back at the original position
        transform.position = originalPosition;
    }
}
