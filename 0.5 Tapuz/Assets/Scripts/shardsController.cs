using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardsController : MonoBehaviour
{
    [SerializeField] GameObject ShardPanel;
    [SerializeField] GameObject ShardSprite;
    [SerializeField] GameObject SymbolSprite;
    [SerializeField] GameObject LockShard;
    [SerializeField] GameObject LockShardActive;
    [SerializeField] GameObject Particles;
    [SerializeField] GameObject RoomLight;
   
    
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
        ShardActivation();
    }

    void ShardActivation()
    {
        LockShard.SetActive(false);
        LockShardActive.SetActive(true);
      //  RoomLight.SetActive(true);
      //  Particles.SetActive(true);
    }
}
