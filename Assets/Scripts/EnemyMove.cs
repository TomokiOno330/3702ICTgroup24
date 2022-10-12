using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private EnemyStatus _status;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); // keep NavMeshAgent
        _status = GetComponent<EnemyStatus>();
    }

    // setting CollisionDetector onTriggerStay to detect collision
    public void OnDetectObject(Collider collider)
    {
        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }
        
        //if detected object has tag player
        if (collider.CompareTag("Player"))
        {
        
                _agent.destination = collider.transform.position;
    
        }
    }
}