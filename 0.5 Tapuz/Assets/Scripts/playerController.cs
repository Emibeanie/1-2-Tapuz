using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer[] sr;
    [SerializeField] Animator animator;
    public AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip landSound;
    public AudioClip jumpSound;
    public Transform feetPos;
    public LayerMask whatIsGround;
    
    [Header("Physics")]
    public float moveSpeed;
    public float jumpForce;
    public float checkRadius = 2f;
    private float moveInput;

    private string backJumpLayer = "Default";
    private string forwardJumpLayer = "Player";
    public float jumpPressedRememberTime = 0.2f;
    private float jumpPressedRemember;
    private float groundedRemember;
    public float groundedRememberTime = 0.2f;
    public float cutJumpHeight;
    private bool isGrounded;
    private bool isFacingRight = true;

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
            //audioSource.clip = runSound;       ----------------- runnig sound ?!
            //audioSource.Play();
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

        groundedRemember -= Time.deltaTime;

        if (isGrounded)
            groundedRemember = groundedRememberTime;

        jumpPressedRemember -= Time.deltaTime;

        if (Input.GetButtonDown("Jump")) //coyote time jump
        {
            jumpPressedRemember = jumpPressedRememberTime;

            if (groundedRemember > 0 && jumpPressedRemember > 0)
            {
                animator.SetBool("IsJumping", true);
                audioSource.PlayOneShot(jumpSound);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                foreach (SpriteRenderer renderer in sr)
                {
                    renderer.sortingLayerName = backJumpLayer;
                }
            }
        }
       
        if(isGrounded)
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("IsJumping", true);
                audioSource.PlayOneShot(jumpSound);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                foreach (SpriteRenderer renderer in sr)
                {
                    renderer.sortingLayerName = backJumpLayer;
                }
            }

        if (Input.GetButtonUp("Jump"))
            if (rb.velocity.y > 0.01f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * cutJumpHeight);
                animator.SetBool("IsJumping", false);
            }
        

        if (Input.GetButtonUp("Jump"))
            animator.SetBool("IsJumping", false);

        if (rb.velocity.y <= 0.3)
        {

            foreach (SpriteRenderer renderer in sr)
            {
                renderer.sortingLayerName = forwardJumpLayer;
            }

           // audioSource.PlayOneShot(landSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            Debug.Log("DETECTEDDDDDD");
            animator.SetBool("IsJumping", false);
            isGrounded = true;
        }
    }

    //public void LandSound()
    //{
    //    audioSource.clip = landSound;
    //    audioSource.PlayOneShot(landSound);
    //}
}