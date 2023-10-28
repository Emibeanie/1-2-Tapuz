using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardsController : MonoBehaviour
{
    [SerializeField] GameObject ShardPanel;
    [SerializeField] GameObject ShardSprite;
    [SerializeField] GameObject SymbolSprite;
    [SerializeField] SpriteRenderer LockShard;
    public string LockShardMaterialPath;

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

        LockShard.material = Resources.Load<Material>(LockShardMaterialPath);
    }

}
