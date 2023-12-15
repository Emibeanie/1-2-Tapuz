using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainedPlat : MonoBehaviour
{
    private AudioSource _audioSource;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            _anim.SetBool("Move", true);
            _audioSource.Play();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _anim.SetBool("Move", false);
        }
    }
}
