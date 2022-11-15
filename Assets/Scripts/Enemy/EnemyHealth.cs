using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int health;
    private bool isEnemyDead;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            isEnemyDead = true;
            DestoryEnemy();
        }
    }

    void DestoryEnemy()
    {
        if (isEnemyDead == true)
            Destroy(gameObject);
    }
}
