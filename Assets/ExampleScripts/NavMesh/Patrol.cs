using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    public Transform[] m_Points;
    public float m_Accuracy = 0.5f;

    private NavMeshAgent m_Agent;
    private int m_Index = 0;
    
    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        NextPoint();
    }

    private void NextPoint()
    {
        if (m_Points.Length == 0) return;

        m_Agent.destination = m_Points[m_Index].position;
        m_Index = ++m_Index % m_Points.Length;
    }

    private void Update()
    {
        if (m_Agent.pathPending) return;
        if (m_Agent.remainingDistance > m_Accuracy) return;

        NextPoint();
    }
}
