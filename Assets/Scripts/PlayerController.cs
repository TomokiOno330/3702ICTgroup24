using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed = 2.0f;
    public AudioClip takeDamage;
    //float speed = 3f;
    Rigidbody rb;  

    public GunController theGun;

    void Start()
    {
       rb = GetComponent<Rigidbody> (); 
    }

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
    }

    void HandleMovementInput()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rb.AddForce(x * speed, 0, z * speed, ForceMode.Impulse);

    }
    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
        if(Input.GetMouseButtonDown(0))
            theGun.isFiring = true;
        if(Input.GetMouseButtonUp(0))
            theGun.isFiring = false;
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Slime"){
            PlayerHealth playerhealth = GetComponent<PlayerHealth>();
            playerhealth.curHealth -= 20;
            AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        }
        if(other.gameObject.tag == "Bat"){
            PlayerHealth playerhealth = GetComponent<PlayerHealth>();
            playerhealth.curHealth -= 40;
            AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        }
        if(other.gameObject.tag == "Ghost"){
            PlayerHealth playerhealth = GetComponent<PlayerHealth>();
            playerhealth.curHealth -= 30;
            AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        }
        if(other.gameObject.tag == "EnemyBullet"){
            PlayerHealth playerhealth = GetComponent<PlayerHealth>();
            playerhealth.curHealth -= 20;
            AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        }
        if(other.gameObject.tag == "Golem"){
            PlayerHealth playerhealth = GetComponent<PlayerHealth>();
            playerhealth.curHealth -= 50;
            AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        }
    }
}

