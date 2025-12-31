using UnityEngine;

public class ElfMovement : MonoBehaviour
{
    public float speed = 10f;
    public Transform[] waypoints;

    int currentWaypointIndex = 0;
    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
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

        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);

        if(Vector3.Distance(currentPos, targetPos) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
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
