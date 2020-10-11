using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _parallaxOffset = .5f;
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
        // Parallax
        var amoutToMoveX = transform.position.x - lastXPosition;
        backgroundFar.position += new Vector3(amoutToMoveX, 0f, 0f);
        backgroundMiddle.position += new Vector3(amoutToMoveX * _parallaxOffset, 0f, 0f);
        lastXPosition = transform.position.x;
        
    }
}
