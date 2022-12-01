using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldEnemy : Enemy
{
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
        base.AIChecks();
    }

    protected override void Patrolling()
    {
        base.Patrolling();
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
