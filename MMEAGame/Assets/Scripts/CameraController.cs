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
    private float lastXPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastXPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        ParallaxScroll();
        CameraBoundary();
    }

    private void CameraBoundary()
    {
        // Camera boundary, might change/delete later
        float clampedY = Mathf.Clamp(transform.position.y, _minHeight, _maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

    }

    private void ParallaxScroll()
    {
        // Parallax
        var amoutToMoveX = transform.position.x - lastXPosition;
        backgroundFar.position += new Vector3(amoutToMoveX, 0f, 0f);
        backgroundMiddle.position += new Vector3(amoutToMoveX * _parallaxOffset, 0f, 0f);
        lastXPosition = transform.position.x;
    }
}
