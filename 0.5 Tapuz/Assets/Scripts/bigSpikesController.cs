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

    private Vector2 playerStartPos;
    public Vector2 ElevatorStartPos;
    private Vector2 initialPosition;
    private bool spikesUp = false;
    private float currentUpMoveSpeed;

    void Start()
    {
        initialPosition = transform.position;
        currentUpMoveSpeed = startUpMoveSpeed;
        ToggleSpikes();
    }

    void ToggleSpikes()
    {
        spikesUp = !spikesUp;
        if (spikesUp)
            StartCoroutine(MoveSpike(initialPosition + new Vector2(0, moveSpace), upTime, currentUpMoveSpeed));
        else
            StartCoroutine(MoveSpike(initialPosition, downTime, downMoveSpeed));

        Invoke("ToggleSpikes", spikesUp ? upTime : downTime);
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
        if(spikesUp && currentUpMoveSpeed < maxUpMoveSpeed)
            currentUpMoveSpeed += Time.deltaTime;
        else
            currentUpMoveSpeed = startUpMoveSpeed;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStartPos = new Vector2(-27.16f, 1.12f);
            playerRB.transform.position = playerStartPos;
            Elevator.transform.position = ElevatorStartPos;
            Debug.Log("Player start position: " + playerStartPos);
            Debug.Log("Player position after respawn: " + playerRB.transform.position);
        }
    }
}
