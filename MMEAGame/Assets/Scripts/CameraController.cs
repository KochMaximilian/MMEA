using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = .5f;
    public Transform backgroundFar, backgroundMiddle;
    private Vector2 lastPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ParallaxScroll();
    }
    
    private void ParallaxScroll()
    {
        // Parallax Horizontal
        Vector2 amountToMove = new Vector2(transform.position.x - lastPosition.x, transform.position.y - lastPosition.y);
        
        backgroundFar.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
        backgroundMiddle.position += new Vector3(amountToMove.x , amountToMove.y, 0f) * _parallaxOffset;
        lastPosition = transform.position;
    }
}
