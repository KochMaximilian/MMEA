using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    public Rigidbody2D Rigidbody;


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
        Rigidbody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), Rigidbody.velocity.y);
        
        // MAX: Jump
        if (Input.GetButtonDown("Jump"))
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, jumpForce);
        }
    }
}

