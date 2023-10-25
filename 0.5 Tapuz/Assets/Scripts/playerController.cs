using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
       
        if (Input.GetKey("space"))
        {
            rb.velocity = new Vector2(0,7f);
        }

    }
}
