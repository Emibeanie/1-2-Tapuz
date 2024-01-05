using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardsController : MonoBehaviour
{
    [SerializeField] GameObject ShardPanel;
    [SerializeField] GameObject ShardSprite;
    [SerializeField] GameObject SymbolSprite;
    [SerializeField] GameObject LockShardActive;
    //[SerializeField] AudioSource audioSource;
    //[SerializeField] AudioClip startSound;
    //[SerializeField] AudioClip loopSound;

    private void Start()
    {
        //StartCoroutine(shardSound());
    }

    private void Update()
    {
        if (ShardPanel.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
                ExitButton();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ShardPanel.SetActive(true);
    }

    public void ExitButton()
    {
        ShardPanel.SetActive(false);
        ShardSprite.SetActive(false);
        SymbolSprite.SetActive(true); 
        LockShardActive.SetActive(true);
    }
    //IEnumerator shardSound()
    //{
    //    audioSource.clip = startSound;
    //    audioSource.Play();

    //    yield return new WaitForSeconds(audioSource.clip.length);

    //    audioSource.clip = loopSound;
    //    audioSource.Play();
    //}
}
