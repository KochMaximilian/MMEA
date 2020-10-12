using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* MAX: Todo Grappling Hook
- Create aiming system.
- Use a line renderer and distance joint to create a rope.
- Make the rope wrap around objects in your game.
- Calculate an angle for swinging on a rope and add force in that direction.
- Raycast

*/

public class RopeSystem : MonoBehaviour
{
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite; 
    public PlayerController PlayerController;
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    
    // Max: Awake runs when the game starts and disables the rope joint and sets player position to current position 
    private void Awake()
    {
        ropeJoint.enabled = false;
        playerPosition = transform.position;
        ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
        ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // MAX_ Get position of the mouse with ScreenToWorldPoint method
        var worldMousePosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDirection = worldMousePosition - transform.position; // MAX: calculate facing direction by subtracting player position from the mouse position in the world
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x); // MAX: represents the Angel of the mouse cursor
        if (aimAngle < 0f)
        {
            // MAX: Keep the value positive
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        //MAX: convert the radian angle to an angle in degrees
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPosition = transform.position;  // MAX: Track player position 

        if (!ropeAttached)
        {
            
        }
        else
        {
            
        }
    }
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }
}
