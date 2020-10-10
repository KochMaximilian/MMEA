using System;
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

    // Basic Variables
    public Rigidbody2D playerRigidbody;
    private bool isOnGround;
    private bool canDoubleJump;

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
}

