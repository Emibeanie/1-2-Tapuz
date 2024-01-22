using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class tunnelEnd : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] GameObject LightPanel;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LightOn());
            playerRB.transform.position = new Vector2(-6.81f, -4.08f);
            
        }
    }

    IEnumerator LightOn()
    {
        LightPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        LightPanel.SetActive(false);
    }
}
