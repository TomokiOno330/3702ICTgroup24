using UnityEngine;
using System.Collections;

public class StateMachine: MonoBehaviour
{
    public enum FSMState
    {
        None,
        Patrol,
        Dead,
        Chase,
    }

    // Current state that the NPC is reaching
    public FSMState curState;

    public float moveSpeed = 12.0f; // Speed of the tank
    public float rotSpeed = 2.0f; // Tank Rotation Speed

    protected Transform playerTransform;// Player Transform
    protected Vector3 destPos; // Next destination position of the NPC Tank
    protected GameObject[] pointList; // List of points for patrolling

    // Whether the NPC is destroyed or not
    protected bool bDead;
    public int health = 100;

    // Effects for death
    public GameObject explosion;


    float meleeDistance = 5f;
    float chaseDistance = 6f;
    float playerDistance;

    [SerializeField] GameObject Ghost;
    float GhostRotationSpeed = 10;



    /*
     * Initialize the Finite state machine for the creature
     */
    void Start()
    {
        curState = FSMState.Patrol;

        bDead = false;

        // Get the list of patrol points
        pointList = GameObject.FindGameObjectsWithTag("WayPoint");
        FindNextPoint();  // Set a random destination point first

        // Get the target (Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");
    }


    // Update each frame
    void Update()
    {
        if (!bDead && playerTransform != null)
        {
            playerDistance = Vector3.Distance(transform.position, playerTransform.position);

            //Ghost look at player
            Quaternion GhostRotation = Quaternion.LookRotation(destPos - transform.position);
            Ghost.transform.rotation = Quaternion.Slerp(Ghost.transform.rotation, GhostRotation,
            Time.deltaTime * GhostRotationSpeed);

            // Go to dead state if no health left
            if (health <  1)
            {
                curState = FSMState.Dead;
            }
            else if (playerDistance <= chaseDistance)
            {
                curState = FSMState.Chase;
            }
            else
            {
                curState = FSMState.Patrol;
            }

            switch (curState)
            {
                case FSMState.Patrol: UpdatePatrolState(); break;
                case FSMState.Dead: UpdateDeadState(); break;
                case FSMState.Chase: UpdateChaseState(); break;
            }
        }
    }

    void OnDrawGizmos()
    {
        float radius = 0;
        switch (curState)
        {
            case FSMState.Patrol: Gizmos.color = Color.green; radius = chaseDistance; break;
            case FSMState.Chase: Gizmos.color = Color.yellow; radius = meleeDistance; break;
        }
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(destPos, 2);
    }


    /*
     * Patrol state
     */
    protected void UpdatePatrolState()
    {
        // Find another random patrol point if the current point is reached
        if (Vector3.Distance(transform.position, destPos) <= 3.0f)
        {
            FindNextPoint();
        }

        // Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));

        // Go Forward
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
    }

    /*
     * Dead state
     */
    protected void UpdateDeadState()
    {
        // Show the dead animation with some physics effects
        if (!bDead)
        {
            bDead = true;
            Explode();
        }
    }


    protected void UpdateChaseState()
    {
        destPos = playerTransform.position;

        // Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed));

        // Go Forward
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * moveSpeed);
    }



    // Find the next semi-random patrol point
    protected void FindNextPoint()
    {
        int randomIndex = Random.Range(0, pointList.Length);
        destPos = pointList[randomIndex].transform.position;
    }



    protected void Explode()
    {
        float randomX = Random.Range(8.0f, 12.0f);
        float randomZ = Random.Range(8.0f, 12.0f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<Rigidbody>().AddExplosionForce(10.0f, transform.position - new Vector3(randomX, 2.0f, randomZ), 45.0f, 40.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(randomX, 10.0f, randomZ));
        }
        Invoke("CreateFinalExplosionEffect", 1.4f);
        Destroy(gameObject, 1.5f);
    }


    protected void CreateFinalExplosionEffect()
    {
        if (explosion)
            Instantiate(explosion, transform.position, transform.rotation);
    }

    void ApplyDamage(int dmg)
    {
        health -= dmg;
    }
}
