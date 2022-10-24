using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    protected static GameObject player;
    protected static Rigidbody enemyRb;
    [SerializeField] protected NavMeshAgent enemy;
    NavMeshHit hit;

    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        //enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

        //if (transform.position.y < -10)
        //{
        //    gameObject.SetActive(false);
        //}

        
        
        enemy.SetDestination(player.transform.position);
        
        if(transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }


    }
}
