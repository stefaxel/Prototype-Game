using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected int bulletForceFieldDamage;
    [HideInInspector]
    public Vector3 hitPoint;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * bulletSpeed * Time.deltaTime);
    }


    virtual protected void OnCollisionEnter(Collision collision)
    {
        ForceField col = gameObject.GetComponent<ForceField>(); 

        if (collision.gameObject.tag == "Force Field")
        {
            if(collision == null)
            {
                Debug.Log("collision returned null");
            }

            if(col == null)
            {
                Debug.Log("gameObject.GetComponent returned null");
            }
            col.DamageForceField(bulletDamage);
            //collision.gameObject.GetComponent<ForceField>().DamageForceField(bulletForceFieldDamage);
            Debug.Log("Force Field is taking damage: " + bulletForceFieldDamage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Debug.Log("Enemy is taking damage: " + bulletDamage);
            Destroy(this.gameObject);

        }
        Destroy(this.gameObject);
    }
}
