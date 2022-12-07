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
            SearchPatrolPoint();
        }
        if (roamPointSet)
        {
            enemy.SetDestination(roamPoint);
        }

        distanceToRoamPoint = transform.position - roamPoint;

        if(distanceToRoamPoint.magnitude < 1f)
        {
            StartCoroutine(WaitForNewPoint(1.5f));
            roamPointSet = false;
        }
    }

    private IEnumerator WaitForNewPoint(float waitTime)
    {
        enemy.isStopped = true;
        forceField.SetActive(true);
        Debug.Log("waitng for 1.5 seconds");
        yield return new WaitForSeconds(waitTime);
        forceField.SetActive(false);
        enemy.isStopped = false;
    }

    protected override void SearchPatrolPoint()
    {
        base.SearchPatrolPoint();
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
