using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip spikesSound;
    [SerializeField] AudioClip[] dyingSound;
    private AudioClip ranDyingSound;

    [Header("Physics")]
    public float upTime;
    public float downTime;
    public float moveSpace;
    public float startUpMoveSpeed;
    public float maxUpMoveSpeed;
    public float downMoveSpeed;
    public float waitTime;

    public Vector2 playerStartPos;
    private Vector2 _initialPosition;
    private bool _spikesUp = false;
    private float _currentUpMoveSpeed;

    void Start()
    {
        _initialPosition = transform.position;
        _currentUpMoveSpeed = startUpMoveSpeed;
        Invoke("ToggleSpikes", waitTime);
    }

    void ToggleSpikes()
    {
        _spikesUp = !_spikesUp;
        if (_spikesUp)
        {
            StartCoroutine(MoveSpike(_initialPosition + new Vector2(0, moveSpace), upTime, _currentUpMoveSpeed));
            audioSource.clip = spikesSound;
            audioSource.Play();
        }
        else
            StartCoroutine(MoveSpike(_initialPosition, downTime, downMoveSpeed));

        Invoke("ToggleSpikes", _spikesUp ? upTime : downTime);
    }

    IEnumerator MoveSpike(Vector2 targetPosition, float duration, float speed)
    {
        float startTime = Time.time;
        Vector2 startPosition = transform.position;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, t * speed);
            yield return null;
        }

        transform.position = targetPosition;
    }

    private void Update()
    {
        if (_spikesUp && _currentUpMoveSpeed < maxUpMoveSpeed)
            _currentUpMoveSpeed += Time.deltaTime;
        else
            _currentUpMoveSpeed = startUpMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RandomDeath();
            playerRB.transform.position = playerStartPos;
        }
    }

    void RandomDeath()
    {
        int index = Random.Range(0, dyingSound.Length);
        ranDyingSound = dyingSound[index];

        audioSource.clip = ranDyingSound;
        audioSource.Play();
    }
}
