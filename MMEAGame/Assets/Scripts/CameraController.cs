using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = .5f;
    [SerializeField] private float _minHeight, _maxHeight;
    
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
       // CameraBoundary();
    }

    private void CameraBoundary()
    {
        // Camera boundary, might change/delete later
        float clampedY = Mathf.Clamp(transform.position.y, _minHeight, _maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

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
