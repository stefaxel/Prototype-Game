using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldEnemy : Enemy
{
    [Header("Enemy Force Field")]
    [SerializeField] protected GameObject forceField;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        AIChecks();
    }

    protected override void AIChecks()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        attackPlayer = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !attackPlayer)
        {
            forceField.SetActive(false);
            Patrolling();
        }

        if (playerInSight && !attackPlayer)
        {
            forceField.SetActive(true);
            ChasePlayer();
        }

        if (playerInSight && attackPlayer)
        {
            forceField.SetActive(true);
            AttackPlayer();
        }

        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void Patrolling()
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

    private IEnumerator WaitForNewPoint(float waitTime)
    {
        forceField.SetActive(true);
        Debug.Log("waitng for 1.5 seconds");
        yield return new WaitForSeconds(waitTime);
        forceField.SetActive(false);
    }

    protected override void SearchPatrolPoint()
    {
        float randomX = Random.Range(-roamPointRangeX, roamPointRangeX);
        float randomZ = Random.Range(-roamPointRangeZ, roamPointRangeZ);

        roamPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(roamPoint, -transform.up, 2f, whatIsGround))
        {
            StartCoroutine(WaitForNewPoint(1.5f));
            roamPointSet = true;
        }
    }

    protected override void ChasePlayer()
    {
        base.ChasePlayer();
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
    }

    protected override void ResetAttack()
    {
        base.ResetAttack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
    }
}
