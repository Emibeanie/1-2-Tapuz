using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCheck : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public Vector2 playerStartPos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
          rb.transform.position = playerStartPos;
    }
}
