using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _leftPoint, _rightPoint;
    [SerializeField] private float _moveTime, _waitTime;
    private float moveCount, waitCount;
    private bool movingRight;
    private Rigidbody2D enemyRigidbody;
    public SpriteRenderer enemySpriteRenderer;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        _leftPoint.parent = null;
        _rightPoint.parent = null;
        movingRight = true;
        moveCount = _moveTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (moveCount > 0)
        {

            moveCount -= Time.deltaTime;
            
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

            if (moveCount <= 0)
            {
                waitCount = Random.Range(_waitTime * .75f, _waitTime * 1.25f );
            }
            anim.SetBool("isMoving", true);
        } else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            enemyRigidbody.velocity = new Vector2(0f, enemyRigidbody.velocity.y);
            if (waitCount <= 0)
            {
                moveCount = Random.Range(_moveTime * .75f, _moveTime * .85f);
            }
            anim.SetBool("isMoving", false);
        }
    }
}
