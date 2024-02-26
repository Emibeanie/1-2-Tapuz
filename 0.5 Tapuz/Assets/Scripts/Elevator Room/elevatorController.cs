using System.Collections;
using UnityEngine;

public class elevatorController : MonoBehaviour
{
    [SerializeField] GameObject activePlat;
    [SerializeField] AudioClip elevatorStart;
    [SerializeField] AudioClip elevatorLoop;

    private float _moveSpeed = 1.5f;
    private float _leftLimit = -41.1f;
    private float _rightLimit = -29f;
    private bool _canMove = false;
    private bool _isWaiting = false;
    private bool _movingRight = false;
    private AudioSource _audioSource;
    private bool _startSoundPlayed = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_canMove && !_isWaiting)
            MoveElevator();
    }

    IEnumerator WaitForPlayer()
    {
        yield return new WaitUntil(() => !_canMove);
        _isWaiting = false;

        if (transform.position.x <= _leftLimit)
            _movingRight = true;
        else if (transform.position.x >= _rightLimit)
            _movingRight = false;
    }

    void MoveElevator()
    {
        float horizontalMovement = _movingRight ? _moveSpeed : -_moveSpeed;
        transform.Translate(Vector2.right * horizontalMovement * Time.deltaTime);

        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = elevatorLoop;
            _audioSource.Play();
        }

        if (transform.position.x <= _leftLimit && !_movingRight)
        {
            _isWaiting = true;
            StartCoroutine(WaitForPlayer());
        }
        else if (transform.position.x >= _rightLimit && _movingRight)
        {
            _isWaiting = true;
            StartCoroutine(WaitForPlayer());
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_startSoundPlayed)
        {
            _audioSource.clip = elevatorStart;
            _audioSource.Play();
            _startSoundPlayed = true; // start sound played check
            _canMove = true;
            collision.transform.SetParent(transform);
            activePlat.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _canMove = false;
            collision.transform.SetParent(null);
            activePlat.SetActive(false);
            _startSoundPlayed = false;
        }
    }
}