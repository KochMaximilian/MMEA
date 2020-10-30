using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _leftPoint, _rightPoint;
    private bool movingRight;
    private Rigidbody2D enemyRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        _leftPoint.parent = null;
        _rightPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
