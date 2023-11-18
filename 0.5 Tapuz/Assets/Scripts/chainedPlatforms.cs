using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainedPlatforms : MonoBehaviour
{
    public float moveSpace;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            transform.Translate(Vector2.down * )
    }
}
