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

    // Start is called before the first frame update
    void Awake()
    { 
        player = GameObject.Find("Player").transform;
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

        if(playerInSight && attackPlayer)
        {
            AttackPlayer();
        }
        
        if(transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }

    }

    private void Patrolling()
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

        Vector3 distanceToRoamPoint = transform.position - roamPoint;

        if(distanceToRoamPoint.magnitude < 5f)
            roamPointSet = false;
        
    }

    private void SearchPatrolPoint()
    {
        float randomX = Random.Range(-roamPointRangeX, roamPointRangeX);
        float randomZ = Random.Range(-roamPointRangeZ, roamPointRangeZ);

        roamPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(roamPoint, -transform.up, 2f, whatIsGround))
        {
            roamPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        enemy.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        enemy.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody bulletRb = Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
