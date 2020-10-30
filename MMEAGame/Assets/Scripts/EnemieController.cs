using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _leftPoint, _rightPoint;
    private bool movingRight;
    private Rigidbody2D enemyRigidbody;
    public SpriteRenderer enemySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        _leftPoint.parent = null;
        _rightPoint.parent = null;
        movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            enemyRigidbody.velocity = new Vector2(_moveSpeed, enemyRigidbody.velocity.y);
            enemySpriteRenderer.flipX = true;
            if (transform.position.x > _rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        { 
            enemyRigidbody.velocity = new Vector2(-_moveSpeed, enemyRigidbody.velocity.y);
            enemySpriteRenderer.flipX = false;
            if (transform.position.x < _leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }
}
