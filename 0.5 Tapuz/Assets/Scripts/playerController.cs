using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    public Transform feetPos;
    public LayerMask whatIsGround;

    [Header("Physics")]
    public float moveSpeed;
    public float jumpForce;
    public float jumpStartTime;
    public float checkRadius = 2f;
    private float moveInput;
    
    private float jumpTime;
    private bool isGrounded;
    private bool isJumping;
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
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdle", true);
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
        if (isJumping)
        {
            if (rb.velocity.y < 0)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }
        else
        {
            if (isGrounded)
            {
                if (isGrounded)
                    animator.SetBool("IsJumping", false);


                if (Input.GetButtonDown("Jump"))
                {
                    isJumping = true;
                    //jumpTime = jumpStartTime;
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTime = 0.0f;

                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsIdle", false);
                    animator.SetBool("IsJumping", true);
                }

                if (Input.GetButton("Jump") && isJumping)
                {
                    if (jumpTime < jumpStartTime)
                    {
                        rb.velocity = Vector2.up * jumpForce;
                        jumpTime += Time.deltaTime;
                    }
                }

                if (Input.GetButtonUp("Jump"))
                {
                    isJumping = false;

                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsIdle", true);
                    animator.SetBool("IsJumping", false);
                }

            }
        }

    }
}

