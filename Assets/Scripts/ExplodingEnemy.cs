using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    //Variables
    public Transform target;
    public float speed = 2f;
    Rigidbody rig;

    public GameObject explosionEffect;
    public float explosionForce = 5f;
    public float radius = 2f;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        rig.MovePosition(pos);
        transform.LookAt(target);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
            Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider near in colliders)
        {
            Rigidbody rigB = near.GetComponent<Rigidbody>();

            if(rigB != null)
                rigB.AddExplosionForce(explosionForce, transform.position, radius, 1f, ForceMode.Impulse);
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Debug.Log("Player Damaged");
    }
}
