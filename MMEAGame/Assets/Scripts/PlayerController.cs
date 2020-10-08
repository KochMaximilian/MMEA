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
    
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
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
}

