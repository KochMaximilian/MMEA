using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    // MAX: Aiming Rope
    public GameObject ropeHingeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite; 
    public PlayerController PlayerController; // MAX: Player movement
    private bool ropeAttached;
    private Vector2 playerPosition;
    private Rigidbody2D ropeHingeAnchorRb;
    private SpriteRenderer ropeHingeAnchorSprite;
    
    // MAX: Shooting Rope
    public LineRenderer ropeRenderer;
    public LayerMask ropeLayerMask;
    private float ropeMaxCastDistance = 20f;
    private List<Vector2> ropePositions = new List<Vector2>();
    private bool distanceSet;
    
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
            setCrosshairPosition(aimAngle);
        }
        else
        {
            crosshairSprite.enabled = false;
        }
        
        HandleInput(aimDirection);
        UpdateRopePosition();
    }

    // MAX: position crosshair based on the aimAngle
    private void setCrosshairPosition(float aimAngle)
    {
        // Todo MAX: enable sprite for now constantly later on the press of a button
        if (!crosshairSprite.enabled)
        {
            crosshairSprite.enabled = true;
        }

        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);
        
        var crossHairPosition = new Vector3(x, y,0f);
        crosshair.transform.position = crossHairPosition;
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetMouseButton(0))
        {
            Console.Write("Left Mouse Button hit");
            // MAX: When a left mouse click is registered, the rope line renderer is enabled and a 2D raycast is fired out from the player position
            if (ropeAttached) return;
            ropeRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxCastDistance, ropeLayerMask);
            /*
             * MAX:
             * If a valid raycast hit is found, ropeAttached is set to true, and
             * a check is done on the list of rope vertex positions to make sure the
             * point hit isn't in there already.
             */
            
            if (hit.collider != null)
            {
                ropeAttached = true;
                if (!ropePositions.Contains(hit.point))
                {
                    // MAX: jump a little to the distance the player from the ground after grappling something
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    ropeJoint.enabled = true;
                    ropeHingeAnchorSprite.enabled = true;

                }
            }
            // MAX: If the raycast doesn't hit anything, then the rope line renderer and rope joint are disabled
            else
            {
                ropeRenderer.enabled = false;
                ropeAttached = false;
                ropeJoint.enabled = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            Console.Write("Right Mouse Button hit");
            ResetRope();
        }
    }

    private void UpdateRopePosition()
    {
        // 1
        if (!ropeAttached)
        {
            return;
        }
        
        // 2 
        ropeRenderer.positionCount = ropePositions.Count + 1;
        
        // 3 

        for (var i = ropeRenderer.positionCount -1; i >= 0; i--)
        {
            if (i != ropeRenderer.positionCount - 1) // if not the last point of line renderer
            {
                ropeRenderer.SetPosition(i, ropePositions[i]);
                
                // 4
                if (i == ropePositions.Count - 1 || ropePositions.Count == 1) 
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1)
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeHingeAnchorRb.transform.position = ropePosition;
                        if (!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                            distanceSet = true;
                        }
                    }
                } else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeHingeAnchorRb.transform.position = ropePosition;
                    if (!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                // 6
                ropeRenderer.SetPosition(i, transform.position);
            }
        }
    }

    // MAX: If the right mouse button is clicked, the ResetRope() method is called, which will disable and reset all rope/grappling hook related parameters
    private void ResetRope()
    {
        ropeJoint.enabled = false;
        ropeAttached = false;
        PlayerController.isSwinging = false;
        ropeRenderer.positionCount = 2;
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, transform.position);
        ropePositions.Clear();
        ropeHingeAnchorSprite.enabled = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
