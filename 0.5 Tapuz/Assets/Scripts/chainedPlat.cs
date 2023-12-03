using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainedPlat : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            anim.SetBool("Move", true);
            audioSource.Play();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("Move", false);
        }
    }
}
