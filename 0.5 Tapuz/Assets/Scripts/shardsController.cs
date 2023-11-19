using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardsController : MonoBehaviour
{
    [SerializeField] GameObject ShardPanel;
    [SerializeField] GameObject ShardSprite;
    [SerializeField] GameObject SymbolSprite;
    [SerializeField] GameObject LockShardActive;

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
}
