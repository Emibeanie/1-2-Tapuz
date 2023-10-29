using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    public float moveSpeed;
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpStartTime;
    
    private float moveInput;
    private float jumpTime;
    private bool isGrounded;
    private bool isJumping;
    private bool isFacingRight = true;
   
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput > 0 && !isFacingRight)
        {
            isFacingRight = true;
            FaceMoveDirection();
        }
        else if(moveInput < 0 && isFacingRight)
        {
            isFacingRight = false;
            FaceMoveDirection();
        }
        FaceMoveDirection();
        Jump();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
    void FaceMoveDirection()
    {
        bool flipped = !isFacingRight;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
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
                isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
            isJumping = false;
    }

    void OnDirection()
    {
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}
