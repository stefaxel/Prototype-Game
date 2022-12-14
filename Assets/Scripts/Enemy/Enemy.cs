using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy References")]
    protected static Transform player;
    protected static Rigidbody enemyRb;
    [SerializeField] protected NavMeshAgent enemy;
    NavMeshHit hit;
    public Transform bulletHolder;

    [Header("Enemy Parameters")]
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 roamPoint;
    [SerializeField] protected bool roamPointSet = false;
    [SerializeField] protected float roamPointRangeX;
    [SerializeField] protected float roamPointRangeZ;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Enemy Attack")]
    [SerializeField] protected float timeBetweenAttack;
    protected bool alreadyAttacked = false;
    [SerializeField] protected GameObject bullet;

    [Header("Enemy States")]
    [SerializeField] protected float sightRange;
    [SerializeField] protected float attackRange;
    protected bool playerInSight;
    protected bool attackPlayer;
    protected Vector3 distanceToRoamPoint;

    // Start is called before the first frame update
    virtual protected void Awake()
    { 
        player = GameObject.Find("Player").transform;
        enemyRb = GetComponent<Rigidbody>();
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        AIChecks();
    }

    virtual protected void AIChecks()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        attackPlayer = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !attackPlayer)
        {
            Patrolling();
        }

        if (playerInSight && !attackPlayer)
        {
            ChasePlayer();
        }

        if (playerInSight && attackPlayer)
        {
            AttackPlayer();
        }

        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }

    virtual protected void Patrolling()
    {
        if (!roamPointSet)
        {
            Debug.Log("Getting a patrolling point");
            SearchPatrolPoint();
        }
        if (roamPointSet)
        {
            enemy.SetDestination(roamPoint);
        }

        distanceToRoamPoint = transform.position - roamPoint;

        if(distanceToRoamPoint.magnitude < 5f)
            roamPointSet = false;
        
    }

    virtual protected void SearchPatrolPoint()
    {
        float randomX = Random.Range(-roamPointRangeX, roamPointRangeX);
        float randomZ = Random.Range(-roamPointRangeZ, roamPointRangeZ);

        roamPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(roamPoint, -transform.up, 2f, whatIsGround))
        {
            roamPointSet = true;
        }
    
    }

    virtual protected void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    virtual protected void AttackPlayer()
    {
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody bulletRb = Instantiate(bullet, bulletHolder.position, transform.rotation).GetComponent<Rigidbody>();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    virtual protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    virtual protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
