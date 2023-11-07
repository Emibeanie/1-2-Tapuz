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
    [SerializeField] GameObject levelDoor;


    private void Start()
    {
        vaultDoor.SetActive(true);
        levelDoor.SetActive(true);
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
            levelDoor.SetActive(false);
    }
}
