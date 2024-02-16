using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigSpikesController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] GameObject Elevator;

    [Header("Physics")]
    public float upTime;
    public float downTime;
    public float moveSpace;
    public float startUpMoveSpeed;
    public float maxUpMoveSpeed;
    public float downMoveSpeed;

    public Vector2 ElevatorStartPos;
    private Vector2 _initialPosition;
    private bool _spikesUp = false;
    private float _currentUpMoveSpeed;

    void Start()
    {
        _initialPosition = transform.position;
        _currentUpMoveSpeed = startUpMoveSpeed;
        ToggleSpikes();
    }

    void ToggleSpikes()
    {
        _spikesUp = !_spikesUp;
        if (_spikesUp)
            StartCoroutine(MoveSpike(_initialPosition + new Vector2(0, moveSpace), upTime, _currentUpMoveSpeed));
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
        if(_spikesUp && _currentUpMoveSpeed < maxUpMoveSpeed)
            _currentUpMoveSpeed += Time.deltaTime;
        else
            _currentUpMoveSpeed = startUpMoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 newPos = new Vector2(-27f, 1f);
            playerRB.MovePosition(newPos);

            Elevator.transform.position = ElevatorStartPos;
        }
    }
}
