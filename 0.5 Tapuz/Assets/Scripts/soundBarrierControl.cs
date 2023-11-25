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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            for (int i = 0; i < audioSource.Length; i++)
            {
                audioSource[i].enabled = true;
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            for (int i = 0; i < audioSource.Length; i++)
            {
                audioSource[i].enabled = false;
            }
    }
}
