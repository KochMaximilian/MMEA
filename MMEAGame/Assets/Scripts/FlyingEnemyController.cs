using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;
    public SpriteRenderer SpriteRenderer;
    public float distanceToAttackPlayer, chaseSpeed;
    private Vector3 attackTarget;
    private bool hasAttacked;
    public float waitAfterAttack;
    private float attackCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    void Update()
    {
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            AttackPlayer();
        }
    }

    public void EnemyMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, points[currentPoint].position) <.05f)
        {
            currentPoint++;

            if(currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }

        if (transform.position.x < points[currentPoint].position.x)
        {
            SpriteRenderer.flipX = true;
        }
        else if (transform.position.x > points[currentPoint].position.x)
        {
            SpriteRenderer.flipX = false;
        }
    }

    public void AttackPlayer()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
        {
            attackTarget = Vector3.zero;
            EnemyMovement();
        }
        else
        {
            // Attacking the Player
            if (attackTarget == Vector3.zero)
            {
                attackTarget = PlayerController.instance.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, attackTarget,
                chaseSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, attackTarget) <= .1f)
            {
                hasAttacked = true;
                attackCounter = waitAfterAttack;
                attackTarget = Vector3.zero;
            }
        }
    }
}
