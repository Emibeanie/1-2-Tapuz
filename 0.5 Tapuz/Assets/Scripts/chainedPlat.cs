using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainedPlat : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
         
    }
}
