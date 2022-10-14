using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
   public float multiplier = 1.4f;
   //public GameObject pickupEffect;
   public PlayerController playercontroller;
   void OnTriggerEnter(Collider other)
   {
    if (other.CompareTag("Player"))
    {
      StartCoroutine(Pickup(other));
    }
   }

   IEnumerator Pickup(Collider player)
   {
      //Instantiate(pickupEffect, transform.position, transform.rotation);
      
      //PlayerController moveSpeed = player.GetComponent<PlayerController>();
      //moveSpeed.speed += 2;
      playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
      playercontroller.speed += 20;

      GetComponent<MeshRenderer>().enabled = false;
      GetComponent<Collider>().enabled = false;
      Debug.Log("Speed Boost");
      yield return new WaitForSeconds (3f);

      Destroy(gameObject);
   }
}
