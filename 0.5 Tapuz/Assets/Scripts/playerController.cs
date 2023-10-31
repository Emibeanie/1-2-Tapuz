using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    public float moveSpeed;
    public float jumpForce;
    public float jumpStartTime;
    public float checkRadius;
    public Transform feetPos;
    public LayerMask whatIsGround;

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

        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTime = jumpStartTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (isJumping)
        {
            if (jumpTime > 0.5f)
            {
                animator.SetBool("IsJumpingH", true);
                animator.SetBool("IsJumpingL", false);
                animator.SetBool("IsIdle", false);
            }
            else
            {
                animator.SetBool("IsJumpingH", false);
                animator.SetBool("IsJumpingL", true);
                animator.SetBool("IsIdle", false);
            }
        }
    }

    void OnDirection()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}

