using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    private Rigidbody enemyRb;
    protected static GameObject player;
    [SerializeField] protected NavMeshAgent enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
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


    }
}
