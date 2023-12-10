using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer[] sr;
    public AudioClip runSound;
    public AudioClip landSound;
    public AudioClip jumpSound;
    public Transform feetPos;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private string backJumpLayer = "Default";
    private string forwardJumpLayer = "Player";

    [Header("Movement")]
    public float moveSpeed;
    private float moveInput;
    private bool isFacingRight = true;

    [Header("Jump")]
    public float jumpForce;
    public float upDrag;
    public float downDrag;
    public float reguDrag;
    //public float coyoteTime = 0.2f;
    //private float lastTimeGrounded;
    //private bool canJump = true;
    private float checkRadius = 0.2f;
    public float cutJumpHeight = 0.5f;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        Movement();
        FaceMoveDirection();
        Jump();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Movement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            if (isGrounded)
            {
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsIdle", true);
            }
        }

        if (moveInput > 0 && !isFacingRight)
        {
            isFacingRight = true;
            FaceMoveDirection();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            isFacingRight = false;
            FaceMoveDirection();
        }
    }

    void FaceMoveDirection()
    {
        bool flipped = !isFacingRight;
        transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump")) //jump
            {
                rb.gravityScale = upDrag; // gravity fix

                animator.SetBool("IsJumping", true);
                audioSource.PlayOneShot(jumpSound);

                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                foreach (SpriteRenderer renderer in sr)
                {
                    renderer.sortingLayerName = backJumpLayer;
                }
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.01f) //cut jump on button release
            {
                rb.gravityScale = downDrag; // gravity fix

                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
                animator.SetBool("IsJumping", false);

                foreach (SpriteRenderer renderer in sr)
                {
                    renderer.sortingLayerName = forwardJumpLayer;
                }
            }
        }


        //if (Input.GetButtonDown("Jump") && canJump)
        //{
        //    animator.SetBool("IsJumping", true);
        //    audioSource.PlayOneShot(jumpSound);
        //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        //    foreach (SpriteRenderer renderer in sr)
        //    {
        //        renderer.sortingLayerName = backJumpLayer;
        //    }

        //    canJump = false;
        //}

        //if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.01f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
        //    animator.SetBool("IsJumping", false);

        //    foreach (SpriteRenderer renderer in sr)
        //    {
        //        renderer.sortingLayerName = forwardJumpLayer;
        //    }
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            Debug.Log("DETECTEDDDDDD");
            animator.SetBool("IsJumping", false);
            isGrounded = true;
            rb.drag = reguDrag;
        }
    }
}