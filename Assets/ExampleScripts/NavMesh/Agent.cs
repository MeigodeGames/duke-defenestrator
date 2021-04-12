using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    [Header("Target")]
    public Transform m_Goal;

    [Header("Wander")]
    public float m_WanderRadius = 2.0f;
    public float m_WanderDistance = 10.0f;


    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Seek();
        //Flee();
        Wander();
    }

    private void Seek()
    {
        //m_Agent.destination = m_Goal != null ? m_Goal.position : transform.position;
        if (!m_Goal) return;

        m_Agent.destination = m_Goal.position;
    }

    private void Flee()
    {
        if (!m_Goal) return;

        var direction = (transform.position - m_Goal.position).normalized;
        var destination = transform.position + direction;

        m_Agent.destination = destination;
    }

    private void Wander()
    {
        var x = Random.Range(-1.0f, 1.0f);
        var z = Random.Range(-1.0f, 1.0f);
        var circle = new Vector3(x, 0.0f, z);
        circle = circle.normalized * m_WanderRadius;


        var direction = transform.position + transform.forward;
        direction = direction.normalized * m_WanderDistance;


        var destination = direction + circle;
        m_Agent.destination = destination;
    }
}
