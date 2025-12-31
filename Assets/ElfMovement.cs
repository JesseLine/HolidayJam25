using UnityEngine;

public class ElfMovement : MonoBehaviour
{
    public float speed = 10f;
    public float chaseSpeed = 15f;
    public float rotationSpeed = 5f;
    public Transform[] waypoints;

    public float viewRange = 5f;
    public float viewAngle = 45f;
    public Transform player;
    public Transform viewingCone;

    bool playerInView;

    int currentWaypointIndex = 0;
    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInView = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerInView();
        if (playerInView)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
        

    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        Vector3 currentPos = transform.position;
        Vector3 targetPos = targetWaypoint.position;

        currentPos.y = 0f;
        targetPos.y = 0f;

        Vector3 direction = (targetPos - currentPos).normalized;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        if(Vector3.Distance(currentPos, targetPos) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void Chase()
    {
        Debug.Log("Chasing!");
        Vector3 currentPos = transform.position;
        Vector3 targetPos = player.position;

        Vector3 direction = (targetPos - currentPos).normalized;

        if(direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        rb.MovePosition(transform.position + direction * chaseSpeed * Time.deltaTime);
    }

    void PlayerInView()
    {
        if (viewingCone.GetComponent<MeshCollider>().bounds.Intersects(player.GetComponent<CapsuleCollider>().bounds))
        {
            Debug.Log("Player in view");
            playerInView = true;
        }
        else
        {
            playerInView = false;
        }

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
    }
}
