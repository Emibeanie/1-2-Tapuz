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
    public float checkRadius = 2f;
    private float moveInput;

    private float jumpPressedRememberTime = 0.2f;
    private float jumpPressedRemember = 0;
    private float groundedRemember = 0;
    public float groundedRememberTime;
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
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        if(isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("IsJumping", true);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("IsJumping", false);
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
}



//void Jump()
//{
//    isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
//    animator.SetBool("IsGrounded", isGrounded);
//    fGroundedRemember -= Time.deltaTime;
//    fJumpPressedRemember -= Time.deltaTime;

//    if (isJumping)
//    {
//        if (rb.velocity.y < 0)
//        {
//            isJumping = false;
//            animator.SetBool("IsJumping", false);
//        }
//    }
//    else
//    {
//        if (isGrounded)
//        {
//            animator.SetBool("IsJumping", false);
//            fGroundedRemember = fGroundedRememberTime;


//            if (Input.GetButtonDown("Jump"))
//            {
//                isJumping = true;
//                jumpTime = 0.0f;
//                isJumpButtonDown = true;
//                fJumpPressedRemember = fJumpPressedRememberTime;

//                animator.SetBool("IsRunning", false);
//                animator.SetBool("IsIdle", false);
//                animator.SetBool("IsJumping", true);

//                rb.velocity = Vector2.up * shortJumpForce;
//            }
//        }
//    }

//    if (Input.GetButton("Jump") && isJumping)
//    {
//        if (isJumpButtonDown)
//        {
//            rb.velocity = new Vector2(rb.velocity.x, shortJumpForce);
//            isJumpButtonDown = false;
//        }
//        if (jumpTime < maxJumpTime)
//        {
//            rb.velocity = new Vector2(rb.velocity.x, shortJumpForce);
//            jumpTime += Time.deltaTime;
//        }
//        else
//        {
//            rb.velocity = new Vector2(rb.velocity.x, longJumpForce);
//        }
//    }


//    if (Input.GetButtonUp("Jump"))
//    {
//        isJumping = false;
//        isJumpButtonDown = false;

//        animator.SetBool("IsRunning", false);
//        animator.SetBool("IsIdle", true);
//        animator.SetBool("IsJumping", false);
//    }
//}
//}


