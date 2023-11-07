using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightsPuzzle : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Vector2 playerStartPos;
    [SerializeField] GameObject Panel;

    [Header("Lights")]
    [SerializeField] GameObject[] lights;

    [Header("Platforms")]
    [SerializeField] GameObject[] platforms;

    public float light1Duration;
    public float light2Duration;
    public float light3Duration;

    void Start()
    {
        foreach (var light in lights)
            light.SetActive(false);

        StartCoroutine(LightsControl());
    }
    private void Update()
    {
        LightsActive();
    }
    void LightsActive()
    {
        if (Panel.activeSelf)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].SetActive(true);
            }
        }
    }

    IEnumerator LightsControl()
    {
        while (!Panel.activeSelf)
        {
            yield return Lights1(light1Duration);
            yield return Lights2(light2Duration);
            yield return Lights3(light3Duration);
        }
    }
    IEnumerator Lights1(float duration)
    {
        lights[0].SetActive(true);
        platforms[0].SetActive(true);
        platforms[1].SetActive(true);
        yield return new WaitForSeconds(duration);
        lights[0].SetActive(false);
        platforms[0].SetActive(false);
        platforms[1].SetActive(false);
    }
    IEnumerator Lights2(float duration)
    {
        lights[1].SetActive(true);
        platforms[1].SetActive(true);
        platforms[2].SetActive(true);
        platforms[3].SetActive(true);
        yield return new WaitForSeconds(duration);
        lights[1].SetActive(false);
        platforms[1].SetActive(false);
        platforms[2].SetActive(false);
        platforms[3].SetActive(false);
    }
    IEnumerator Lights3(float duration)
    {
        lights[2].SetActive(true);
        platforms[3].SetActive(true);
        platforms[4].SetActive(true);
        yield return new WaitForSeconds(duration);
        lights[2].SetActive(false);
        platforms[3].SetActive(false);
        platforms[4].SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerRB.transform.position = playerStartPos;
    }
}
