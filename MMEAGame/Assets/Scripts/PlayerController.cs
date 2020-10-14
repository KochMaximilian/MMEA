﻿using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Serialized Fields
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _doupleJumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayerMask;
    public Vector2 ropeHook;
    public float swingForce = 4f;


    // Basic Variables
    public Rigidbody2D playerRigidbody;
    private bool isOnGround;
    private bool canDoubleJump;
    public bool isSwinging;
    private float horizontalInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
   
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerAnimation();
    }

    
    private void PlayerMovement()
    {
        // MAX: Move left and right
        playerRigidbody.velocity = new Vector2(_moveSpeed * Input.GetAxis("Horizontal"), playerRigidbody.velocity.y);
        
        // MAX: Jump
        isOnGround = Physics2D.OverlapCircle(_groundCheck.position, .2f, _groundLayerMask); // MAX: Check if there is a collision with any other objects (Circle Objct on Ground)
        if (isOnGround)
        {
            canDoubleJump = true;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _jumpForce);
            }
            else
            {
                if (canDoubleJump)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _doupleJumpForce);
                    canDoubleJump = false;
                }
            }
        }
    }

    
    private void PlayerAnimation()
    {
        // change animation form left to right (flip left = ture , flip right = false)
        if(playerRigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerRigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        animator.SetFloat("moveSpeed",Math.Abs(playerRigidbody.velocity.x));
        animator.SetBool("isOnGround", isOnGround);
    }

    private void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));
            spriteRenderer.flipX = horizontalInput < 0f;

            if (isSwinging)
            {
                animator.SetBool("IsSwinging", true);
                var playerToHookDirection = (ropeHook - (Vector2) transform.position).normalized;
                
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y,playerToHookDirection.x);
                    var leftPerpPos = (Vector2) transform.position - perpendicularDirection * -4f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2) transform.position + perpendicularDirection * 4f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green);
                    var force = perpendicularDirection * swingForce;
                    playerRigidbody.AddForce(force, ForceMode2D.Force);
                }
            }
            else
            {
              animator.SetBool("IsSwinging", false);
              animator.SetFloat("moveSpeed", 0f);
            }

            if (!isSwinging)
            {
                if (!isOnGround) return;
                if (canDoubleJump)
                {
                    playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, _jumpForce);
                }    
            }
        }
    }
}

