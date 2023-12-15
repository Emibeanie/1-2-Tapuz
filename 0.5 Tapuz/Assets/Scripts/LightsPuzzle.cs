using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightsPuzzle : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Vector2 playerStartPos;
    [SerializeField] GameObject Panel;

    [Header("Lights")]
    [SerializeField] GameObject[] lights;

    [Header("Platforms")]
    [SerializeField] GameObject[] platforms;

    private float _light1Duration = 1.3f;
    private float _light2Duration = 1.3f;
    private float _light3Duration = 1.3f;
    private bool _playerInRoom = false;

    void Start()
    {
        foreach (var light in lights)
            light.SetActive(false);
    }
    private void Update()
    {
        LightsActive();
    }
    void LightsActive()
    {
        if (Panel.activeSelf)
        {
            lights[2].SetActive(true);
            lights[1].SetActive(false);
            lights[0].SetActive(false);

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].SetActive(true);
            }
        }
    }

    IEnumerator LightsControl()
    {
        while (_playerInRoom && !Panel.activeSelf)
        {
            yield return Lights1(_light1Duration);
            yield return Lights2(_light2Duration);
            yield return Lights3(_light3Duration);
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerInRoom = true;
            StartCoroutine(LightsControl());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerInRoom = false;
            StopCoroutine(LightsControl());
        }
    }
}
