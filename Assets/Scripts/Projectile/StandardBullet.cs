using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int bulletDamage;

    public Vector3 hitPoint;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * bulletSpeed * Time.deltaTime);
    }

    
    virtual protected void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Debug.Log("Enemy is taking damage: " + bulletDamage);
            Destroy(this.gameObject);

        }
        Destroy(this.gameObject);
    }
}
