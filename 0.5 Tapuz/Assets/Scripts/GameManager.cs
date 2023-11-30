using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] AudioSource ambienceAudioSource;
    [SerializeField] AudioClip ambienceStart;
    [SerializeField] AudioClip ambienceLoop;

    [Header("Vault Puzzle")]
    [SerializeField] GameObject[] correctSymbols;
    [SerializeField] Animator shardLockAnimator;
    [SerializeField] Collider2D spikeLockCollider;

    [Header("Level Door Control")]
    [SerializeField] GameObject[] shards;
    [SerializeField] Animator levelLockAnimator;

    private void Start()
    {
        StartCoroutine(ambienceControl());
    }
    private void Update()
    {
        VaultDoor();
        LevelDoor();
    }

    private void VaultDoor()
    {
        int i = 0;

        foreach (GameObject symbol in correctSymbols)
            if (symbol.activeSelf)
                i++;
        Debug.Log("Number of active symbols: " + i);

        if (i == 3)
        {
            shardLockAnimator.SetBool("Open", true);
            spikeLockCollider.enabled = false;
        }
    }
    private void LevelDoor()
    {
        int l = 0;

        foreach (GameObject shard in shards)
            if (shard.activeSelf)
                l++;

        Debug.Log("Number of active shards: " + l);

        if (l == 3)
            levelLockAnimator.SetBool("Open", true);
    }

    IEnumerator ambienceControl()
    {
        ambienceAudioSource.clip = ambienceStart;
        ambienceAudioSource.Play();

        yield return new WaitForSeconds(ambienceAudioSource.clip.length);

        ambienceAudioSource.clip = ambienceLoop;
        ambienceAudioSource.Play();
    }
}
