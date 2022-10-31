using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float bulletLifetime;

    private Rigidbody bulletRb;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (target.transform.position - transform.position).normalized;
        transform.position += moveDirection * bulletSpeed * Time.deltaTime;
        transform.LookAt(target);
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                bulletRb = collision.gameObject.GetComponent<Rigidbody>();
                //Vector3 away = -collision.contacts[0].normal;
                Destroy(gameObject);
            }
        }
    }
}
