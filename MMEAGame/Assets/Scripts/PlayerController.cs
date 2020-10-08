using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

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

    public void PlayerMovement()
    {
        Rigidbody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), Rigidbody.velocity.y);
    }
}
