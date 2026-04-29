using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class KNIGHTAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public List<Transform> waypoints = new List<Transform>();
    public float patrolSpeed = 2.0f;
    public float chaseSpeed = 5.0f;
    public float waypointThreshold = 1.0f;

    [Header("Vision Settings")]
    public float viewDistance = 10.0f;
    public float viewAngle = 60.0f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Header("Ending Settings")]
    

    private NavMeshAgent agent;
    private Animator animator;
    private int currentWaypointIndex;
    private Transform player;
    public bool isChasing;

    private static readonly int IsChasingHash = Animator.StringToHash("isChasing");

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (waypoints.Count > 0)
        {
            SetDestinationToWaypoint();
        }
    }

    void Update()
    {
        if (player == null) return;

        if (CanSeePlayer())
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);
        }
        else if (isChasing)
        {
            // If lost sight, go back to patrolling
            isChasing = false;
            agent.speed = patrolSpeed;
            // SetDestinationToWaypoint();
        }

        if (animator != null)
        {
            animator.SetBool(IsChasingHash, isChasing);
        }

        if (!isChasing && !agent.pathPending && agent.remainingDistance < waypointThreshold)
        {
            IterateWaypointIndex();
            SetDestinationToWaypoint();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("FALSE ENDING");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           //SceneManager.LoadScene("FALSE ENDING");
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < viewDistance)
        {
            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position + Vector3.up * 1.5f, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void SetDestinationToWaypoint()
    {
        if (waypoints.Count == 0) return;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void IterateWaypointIndex()
    {
        if (waypoints.Count == 0) return;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize Vision Cone
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 1.5f, transform.position + Vector3.up * 1.5f + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position + Vector3.up * 1.5f, transform.position + Vector3.up * 1.5f + rightBoundary * viewDistance);
    }
}
