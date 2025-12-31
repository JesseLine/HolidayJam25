using UnityEngine;
using UnityEngine.AI;

public class ElfMovement : MonoBehaviour
{
    public float speed = 10f;
    public float chaseSpeed = 15f;
    public float rotationSpeed = 5f;
    public Transform[] waypoints;

    public float chaseTimer = 5f;
    [SerializeField] private float timer;

    public float viewRange = 5f;
    public float viewAngle = 45f;
    public Transform player;
    public Transform viewingCone;
    public LayerMask layerMask;

    bool playerInView;
    NavMeshAgent agent;

    int currentWaypointIndex = 0;
    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInView = false;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerInView();
        if (playerInView)
        {
            timer = chaseTimer;
        }
        
        if(timer >= 0)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
        timer -= Time.deltaTime;

    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        Vector3 currentPos = transform.position;
        Vector3 targetPos = targetWaypoint.position;

        currentPos.y = 0f;
        targetPos.y = 0f;
        

        agent.SetDestination(targetPos);

        if(Vector3.Distance(currentPos, targetPos) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void Chase()
    {
        Debug.Log("Chasing!");

        agent.SetDestination(player.position);
    }

    void PlayerInView()
    {

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer > viewRange)
        {
            playerInView = false;
            return;
        }

        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if(angle < viewAngle / 2f)
        {
            if(Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, distanceToPlayer, layerMask))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("player in view");
                    playerInView = true;
                    return;
                }
                
            }
        }

        playerInView = false;

 

    }

    private void OnDrawGizmos()
    {
        if(waypoints != null && waypoints.Length > 0)
        {
            Gizmos.color = Color.red;
            foreach(Transform waypoint in waypoints)
            {
                if(waypoint != null)
                {
                    Gizmos.DrawSphere(waypoint.position, 0.3f);
                }
            }

            Gizmos.color = Color.green;
            for(int i = 0; i < waypoints.Length; i++)
            {
                if(waypoints[i] != null && waypoints[(i+1)% waypoints.Length] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
                }
            }
        }

        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, viewRange);
        Vector3 left = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, left * viewRange);
        Gizmos.DrawRay(transform.position, right * viewRange);
    }
}
