using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemyScript : MonoBehaviour
{

    public Transform player;

    public float chaseDistance;

    public float stoppingDistance;

    private bool isActive = false;

    private NavMeshAgent agent;

    private float shotDelay;
    public float delayStart;

    public GameObject projectile;

    public Transform shotSpawn;



    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       agent.stoppingDistance = stoppingDistance; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, transform.position) <= chaseDistance)
        {
            isActive = true;
        }

        if(isActive == true)
        {
            agent.SetDestination(player.position);

            if(Vector3.Distance(player.position, transform.position) <= agent.stoppingDistance)
            {
                agent.transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

                if(shotDelay <= 0)
                {
                    Instantiate(projectile, shotSpawn.position, shotSpawn.rotation);
                    shotDelay = delayStart;
                }
                else
                {
                    shotDelay -= Time.deltaTime;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}