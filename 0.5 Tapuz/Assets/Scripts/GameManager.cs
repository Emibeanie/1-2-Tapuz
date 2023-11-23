using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Vault Puzzle")]
    [SerializeField] GameObject[] correctSymbols;
    [SerializeField] GameObject vaultDoor;
    

    [Header("Level Door Control")]
    [SerializeField] GameObject[] shards;
    [SerializeField] GameObject levelLockR;
    [SerializeField] GameObject levelLockL;

    private Vector2 lockR = new Vector2(13.4f, 47.215f);
    private Vector2 lockL = new Vector2(2.67f, 47.215f);
    private void Start()
    {
        vaultDoor.SetActive(true);
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
            vaultDoor.SetActive(false);
    }
    private void LevelDoor()
    {
        int l = 0;

        foreach (GameObject shard in shards)
            if (shard.activeSelf)
                l++;

        Debug.Log("Number of active shards: " + l);

        if (l == 3)
            levelUnlock();
    }

    void levelUnlock()
    {
        levelLockR.transform.position = lockR;
        levelLockL.transform.position = lockL;
    }
}
