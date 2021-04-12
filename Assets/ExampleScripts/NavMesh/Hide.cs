using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Hide : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    public Transform m_Target;
    public float m_DetectionRange = 5.0f;

    public LayerMask m_ObstacleLayer;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SeePlayer();
        var obstacles = DetectObstacles();
        var spots = DetectHidingSpots(obstacles);

        if (spots.Count > 0) m_Agent.destination = spots[0];
    }

    private void SeePlayer()
    {
        var direction = m_Target.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Player")) {
                Debug.DrawRay(transform.position, direction, Color.red);
            } else {
                Debug.DrawRay(transform.position, direction, Color.green);
            }
        }
    }

    private Collider[] DetectObstacles()
    {
        var center = transform.position;
        return Physics.OverlapSphere(center, m_DetectionRange, m_ObstacleLayer);
    }

    private List<Vector3> DetectHidingSpots(Collider[] obstacles)
    {
        var positions = new List<Vector3>();

        foreach (var obstacle in obstacles)
        {
            var position = obstacle.transform.position - m_Target.position;
            var space = position.normalized * 1.5f;

            positions.Add(position + space);
        }

        return positions;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.1f);
        Gizmos.DrawSphere(transform.position, m_DetectionRange);
    }
}
