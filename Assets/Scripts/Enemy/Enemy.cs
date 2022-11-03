using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy References")]
    protected static GameObject player;
    protected static Rigidbody enemyRb;
    [SerializeField] protected NavMeshAgent enemy;
    NavMeshHit hit;

    [Header("Enemy Parameters")]
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 roamPoint;
    [SerializeField] protected bool walkPointSet;
    [SerializeField] protected float roamPointRangeX;
    [SerializeField] protected float roamPointRangeZ;
    [SerializeField] protected float negRoamPointRangeX;
    [SerializeField] protected float negRoamPointRangeZ;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Enemy Attack")]
    [SerializeField] protected float timeBetweenAttack;
    protected bool alreadyAttacked;

    [Header("Enemy States")]
    [SerializeField] protected float sightRange;
    [SerializeField] protected float attackRange;
    protected bool playerInSight;
    protected bool attackPlayer;

    // Start is called before the first frame update
    void Awake()
    { 
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        attackPlayer = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSight && !attackPlayer)
        {
            Patrolling();
        }

        if(playerInSight && !attackPlayer)
        {
            ChasePlayer();
        }
        
        if(transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }

    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchPatrolPoint();
        }
        if (walkPointSet)
        {
            enemy.SetDestination(roamPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - roamPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            Debug.Log("Setting a new walk point");
            walkPointSet = false;
        }
    }

    private void SearchPatrolPoint()
    {
        float randomX = Random.Range(-negRoamPointRangeX, roamPointRangeX);
        float randomZ = Random.Range(-negRoamPointRangeZ, roamPointRangeZ);

        roamPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(roamPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.transform.position);
    }
}
