using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPuzzle : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Vector2 playerStartPos;
    [SerializeField] GameObject Shard;

    [Header("Lights")] 
    [SerializeField] GameObject Light1;
    [SerializeField] GameObject Light2;
    [SerializeField] GameObject Light3;
    [Header("Platforms")] 
    [SerializeField] GameObject Plat1;
    [SerializeField] GameObject Plat2;
    [SerializeField] GameObject Plat3;
    [SerializeField] GameObject Plat4;
    [SerializeField] GameObject Plat5;

   

    public float waitTime;
    public float waitTime2;
    public float waitTime3;

    private bool LightsOn = false;

    void Start()
    {
        Light1.SetActive(false);
        Light2.SetActive(false);
        Light3.SetActive(false);

        StartCoroutine(LightsControl());
    }

    void Update()
    {
        if (!Shard.activeSelf)
            LightsOn = true;
    }

    IEnumerator LightsControl()
    {
        while (!LightsOn)
        {
            Lights1();

            Lights2();

            Lights3();
        }
        yield break;
    }
    IEnumerator Lights1()
    {
        Light1.SetActive(true);
        Plat1.SetActive(true);
        Plat2.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Light1.SetActive(false);
        Plat1.SetActive(true);
        Plat2.SetActive(true);
    }
    IEnumerator Lights2()
    {
        Light2.SetActive(true);
        Plat2.SetActive(true);
        Plat3.SetActive(true);
        Plat4.SetActive(true);
        yield return new WaitForSeconds(waitTime2);
        Light2.SetActive(false);
        Plat2.SetActive(false);
        Plat3.SetActive(false);
        Plat4.SetActive(false);
    }
    IEnumerator Lights3()
    {
        Light3.SetActive(true);
        Plat4.SetActive(true);
        Plat5.SetActive(true);
        yield return new WaitForSeconds(waitTime3);
        Light3.SetActive(false);
        Plat4.SetActive(false);
        Plat5.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerRB.transform.position = playerStartPos;
    }
}
