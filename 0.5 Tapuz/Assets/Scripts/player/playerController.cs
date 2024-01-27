using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer[] spriteRenderers;
    public Transform feetPos;
    public LayerMask whatIsGround;
    private Rigidbody2D _rb;
    private Animator _animator;
    private string _backJumpLayer = "PlayerJumpBack";
    private string _forwardJumpLayer = "Player";

    [Header("Particles")]
    [SerializeField] ParticleSystem jumpParticle;
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem inAirParticle;

    [Header("Sound")]
    public AudioClip runSound;
    public AudioClip landSound;
    public AudioClip jumpSound;
    private AudioSource _audioSource;

    //Movement
    private float _moveSpeed = 6f;
    private float _moveInput;
    private bool _isFacingRight = true;
    private bool _jumpPressed;
    private bool _jumpRelease;

    //Jump
    private float _jumpForce = 25f;
    private float _upGravityMultiplier = -40f;
    private float _downGravityMultiplier = -90f;
    private float _checkRadius = 0.2f;
    private float _cutJumpHeight = 0.5f;
    private float _coyoteTime = 0.05f;
    private float _coyoteTimeCounter;
    private bool _isGrounded;

    private void Awake()
    {
        _audioSource.volume = 1;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MoveInput();
        FaceMoveDirection();
        JumpControl();
    }

    private void FixedUpdate()
    {
         _rb.velocity = new Vector2(_moveInput * _moveSpeed, _rb.velocity.y);
      
        if (_jumpPressed) // jump
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpPressed = false;
            inAirParticle.Play();
        }
        else if (_jumpRelease) // jump cut
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * _cutJumpHeight);
            _jumpRelease = false;
            inAirParticle.Stop();
        }


        //Apply Gravity
        if (_isGrounded && _rb.velocity.y <= 0.01f)
            _rb.velocity = new Vector2(_rb.velocity.x, -1.0f);

        else if (!_isGrounded && _rb.velocity.y < 0.01f)
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + (_downGravityMultiplier * Time.deltaTime));

        else
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y + (_upGravityMultiplier * Time.deltaTime));

    }

    void MoveInput()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");

        if (_moveInput != 0)
        {
            _animator.SetBool("IsRunning", true);
            _animator.SetBool("IsIdle", false);
            _audioSource.clip = runSound;
            _audioSource.Play();
            fallParticle.Stop();
        }
        else
        {
            if (_isGrounded)
            {
                _animator.SetBool("IsRunning", false);
                _animator.SetBool("IsIdle", true);
                _audioSource.Stop();
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

    void JumpControl()
    {
        if(_rb.velocity.y > 0.01f)
            _animator.SetBool("IsJumping", false);

        _isGrounded = Physics2D.OverlapCircle(feetPos.position, _checkRadius, whatIsGround);
        _animator.SetBool("IsGrounded", _isGrounded);

        if (_rb.velocity.y > 0)
            _coyoteTimeCounter = 0;

        if(_isGrounded || _coyoteTimeCounter > 0)
        {
            if(Input.GetButtonDown("Jump")) //jump
            {
                _animator.SetBool("IsJumping", true);
                _audioSource.PlayOneShot(jumpSound);
                jumpParticle.Play();

                _jumpPressed = true;
                _jumpRelease = false;
                foreach (SpriteRenderer renderer in spriteRenderers)
                    renderer.sortingLayerName = _backJumpLayer;

                _coyoteTimeCounter = 0f; // coyote reset
            }
        }

        if(Input.GetButtonUp("Jump") && _rb.velocity.y > 0.01f) //cut jump on button release
        {
            Debug.Log("Release");
            _jumpRelease = true;
            
            _animator.SetBool("IsJumping", false);

            foreach (SpriteRenderer renderer in spriteRenderers)
                renderer.sortingLayerName = _forwardJumpLayer;
        }

        if(!_isGrounded)
            _coyoteTimeCounter -= Time.deltaTime;
        else
            _coyoteTimeCounter = _coyoteTime;
    }
    private void FaceMoveDirection()
    {
        bool flipped = !_isFacingRight;
        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    private void OnCollisionEnter2D(Collision2D collision) // add new landing anim
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Debug.Log("DETECTEDDDDDD");
            _animator.SetBool("IsIdle", true);

            if (_rb.velocity.y < 0.0f)
            {
                fallParticle.Play();
                _audioSource.clip = landSound;
                _audioSource.Play();
            }
            }
    }
}