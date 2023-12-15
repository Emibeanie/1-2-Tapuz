using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer[] spriteRenderers;
    public AudioClip runSound;
    public AudioClip landSound;
    public AudioClip jumpSound;
    public Transform feetPos;
    public LayerMask whatIsGround;
    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _audioSource;
    private string _backJumpLayer = "Default";
    private string _forwardJumpLayer = "Player";

    [Header("Movement")]
    public float moveSpeed;
    private float _moveInput;
    private bool _isFacingRight = true;

    [Header("Jump")]
    public float jumpForce;
    
    private float _upGravityMultiplier = -40f;
    private float _downGravityMultiplier = -90f;
    private float _checkRadius = 0.2f;
    private float _cutJumpHeight = 0.5f;
    private bool _isGrounded;

    public float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Movement();
        FaceMoveDirection();
        Jump();
        ApplyGravity();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveInput * moveSpeed, _rb.velocity.y);
    }

    void Movement()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if (_moveInput != 0)
        {
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsIdle", false);
        }
        else
        {
            if (_isGrounded)
            {
                _animator.SetBool("IsRunning", false);
                _animator.SetBool("IsIdle", true);
            }
        }

        if (_moveInput > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
            FaceMoveDirection();
        }
        else if (_moveInput < 0 && _isFacingRight)
        {
            _isFacingRight = false;
            FaceMoveDirection();
        }
    }

    void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(feetPos.position, _checkRadius, whatIsGround);
        _animator.SetBool("IsGrounded", _isGrounded);

        if (_isGrounded)
            lastGroundedTime = Time.time;
    
        if(Input.GetButtonDown("Jump"))
            jumpButtonPressedTime = Time.time;

        if (Input.GetButtonDown("Jump") && Time.time - lastGroundedTime <= jumpButtonGracePeriod) //jump
        {
            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
            {
                _animator.SetBool("IsJumping", true);
                _audioSource.PlayOneShot(jumpSound);

                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                foreach (SpriteRenderer renderer in spriteRenderers)
                    renderer.sortingLayerName = _backJumpLayer;

                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0.01f) //cut jump on button release
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _cutJumpHeight);
            _animator.SetBool("IsJumping", false);

            foreach (SpriteRenderer renderer in spriteRenderers)
                renderer.sortingLayerName = _forwardJumpLayer;

            jumpButtonPressedTime = null;
            lastGroundedTime = null;
        }
    }
    void FaceMoveDirection()
    {
        bool flipped = !_isFacingRight;
        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _rb.velocity.y <= 0.01f)
            _rb.velocity = new Vector2(_rb.velocity.x, -1.0f);

        else if(!_isGrounded && _rb.velocity.y < 0.01f)
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + (_downGravityMultiplier * Time.deltaTime));

        else
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + (_upGravityMultiplier * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            Debug.Log("DETECTEDDDDDD");
            _animator.SetBool("IsJumping", false);
            _isGrounded = true;
        }
    }
}