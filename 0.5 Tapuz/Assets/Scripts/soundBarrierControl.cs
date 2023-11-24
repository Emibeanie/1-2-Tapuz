using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundBarrierControl : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSource;
    private void Start()
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            for (int i = 0; i < audioSource.Length; i++)
            {
                audioSource[i].enabled = true;
            }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            for (int i = 0; i < audioSource.Length; i++)
            {
                audioSource[i].enabled = false;
            }
    }
}
