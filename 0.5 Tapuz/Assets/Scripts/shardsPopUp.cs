using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardsPopUp : MonoBehaviour
{
    [SerializeField] GameObject ShardPanel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            ShardPanel.SetActive(true);
    }

    public void ExitButton()
    {
        ShardPanel.SetActive(false);
    }

}
