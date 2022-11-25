using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : StandardBullet
{
    private Rigidbody bulletRb;

    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
    }
    protected override void Start()
    {
        bulletRb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Debug.Log("Player is taking damage: " + bulletDamage);
            Destroy(this.gameObject);

        }
        Destroy(this.gameObject);
    }
}
