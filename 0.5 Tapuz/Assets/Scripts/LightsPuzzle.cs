using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsPuzzle : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Vector2 playerStartPos;
    [SerializeField] GameObject Light1;
    [SerializeField] GameObject Light2;
    [SerializeField] GameObject Light3;
    [SerializeField] GameObject Shard;

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
        if (Shard == null)
            LightsOn = true;
    }

    IEnumerator LightsControl()
    {
        while (!LightsOn)
        {
            Light1.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            Light1.SetActive(false);

            Light2.SetActive(true);
            yield return new WaitForSeconds(waitTime2);
            Light2.SetActive(false);

            Light3.SetActive(true);
            yield return new WaitForSeconds(waitTime3);
            Light3.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerRB.transform.position = playerStartPos;
    }
}
