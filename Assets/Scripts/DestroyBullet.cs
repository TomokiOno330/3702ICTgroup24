using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    public int objectHP;
    //calls the method when colliding.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shell"))
        {
            Destroy(other.gameObject); 
        }
          
    }
}