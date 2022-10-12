using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    private Rigidbody fastEnemyRb;
    private GameObject findPlayer;

    // Start is called before the first frame update
    void Start()
    {
        fastEnemyRb = GetComponent<Rigidbody>();
        findPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (findPlayer.transform.position - transform.position).normalized;

        fastEnemyRb.AddForce(lookDirection * speed * Time.deltaTime);

        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
        }
    }
}
