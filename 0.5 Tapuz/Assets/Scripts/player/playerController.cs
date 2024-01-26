using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer[] spriteRenderers;
    [SerializeField] ParticleSystem jumpParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem inAirParticle;
    public AudioClip runSound;
    public AudioClip landSound;
    public AudioClip jumpSound;
    public Transform feetPos;
    public LayerMask whatIsGround;
    private Rigidbody2D _rb;
    private Animator _animator;
    private AudioSource _audioSource;
    private string _backJumpLayer = "PlayerJumpBack";
    private string _forwardJumpLayer = "Player";

    [Header("Movement")]
    public float moveSpeed;
    private float _moveInput;
    private bool _isFacingRight = true;
    private bool jumpPressed;
    private bool jumpRelease;

    [Header("Jump")]
    public float jumpForce;
    private float _upGravityMultiplier = -40f;
    private float _downGravityMultiplier = -90f;
    private float _checkRadius = 0.2f;
    private float _cutJumpHeight = 0.5f;
    private bool _isGrounded;
    public float coyoteTime = 0.2f; // to fix
    private float coyoteTimeCounter;

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
      
        if (jumpPressed) // jump
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            jumpPressed = false;
            inAirParticle.Play();
        }
        else if (jumpRelease) // jump cut
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _cutJumpHeight);
            jumpRelease = false;
            inAirParticle.Stop();
        }

    }

    void Movement()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if (_moveInput != 0)
        {
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsIdle", false);
            fallParticle.Stop();
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
        if(_rb.velocity.y > 0.01f)
            _animator.SetBool("IsJumping", false);

        _isGrounded = Physics2D.OverlapCircle(feetPos.position, _checkRadius, whatIsGround);
        _animator.SetBool("IsGrounded", _isGrounded);

        if (_rb.velocity.y > 0)
            coyoteTimeCounter = 0;

        if(_isGrounded || coyoteTimeCounter > 0)
        {
            if(Input.GetButtonDown("Jump")) //jump
            {
                _animator.SetBool("IsJumping", true);
                _audioSource.PlayOneShot(jumpSound);
                jumpParticle.Play();

                jumpPressed = true;
                jumpRelease = false;
                foreach (SpriteRenderer renderer in spriteRenderers)
                    renderer.sortingLayerName = _backJumpLayer;

                coyoteTimeCounter = 0f; // coyote reset
            }
        }

        if(Input.GetButtonUp("Jump") && _rb.velocity.y > 0.01f) //cut jump on button release
        {
            Debug.Log("Release");
            jumpRelease = true;
            
            _animator.SetBool("IsJumping", false);

            foreach (SpriteRenderer renderer in spriteRenderers)
                renderer.sortingLayerName = _forwardJumpLayer;
        }

        if(!_isGrounded)
            coyoteTimeCounter -= Time.deltaTime;
        else
            coyoteTimeCounter = coyoteTime;
    }
    private void FaceMoveDirection()
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

    private void OnCollisionEnter2D(Collision2D collision) // add new landing anim
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Debug.Log("DETECTEDDDDDD");
            _animator.SetBool("IsIdle", true);

            if (_rb.velocity.y < 0.0f)
                fallParticle.Play();
        }
    }
}