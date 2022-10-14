using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    [Tooltip("patrol points")]
    private Transform[] waypoints;

    private int wayPointNumber;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[0].position);
    }


    void Update()
    {     
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {  
            wayPointNumber = (wayPointNumber + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[wayPointNumber].position);
        }
    }
}