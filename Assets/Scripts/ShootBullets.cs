using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    public GameObject shellPrefab;
    public float shotSpeed;
    //public AudioClip shotSound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
    
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);

     
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

   
            shellRb.AddForce(transform.forward * shotSpeed);

            Destroy(shell, 3.0f);
        }
    }
}