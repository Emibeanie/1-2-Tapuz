using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainDoorControl : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] GameObject[] shards;
    [SerializeField] GameObject panel;
    [SerializeField] AudioSource doorAudioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            ShardsCheck();
    }
    private void Update()
    {
        if(panel.activeSelf)
            if(Input.GetKeyDown(KeyCode.Escape))
                panel.SetActive(false);
    }
    private void ShardsCheck()
    {
        int i = 0;

        foreach (GameObject shard in shards)
            if (shard.activeSelf)
                i++;
        Debug.Log("Number of active symbols: " + i);

        if (i == 4)
        {
            doorAudioSource.Play();
            panel.SetActive(true);
        }
        else
            Debug.Log("door is closed");
    }
}
