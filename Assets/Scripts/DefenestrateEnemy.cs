using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenestrateEnemy : MonoBehaviour
{
    private Transform[] m_Anchors = null;
    private Transform m_WindowTransform = null;
    private Vector3 m_Direction;

    private Rigidbody m_Body;
    private LineRenderer m_Line;

    [Header("Target")]
    public float m_Speed = 20.0f;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Line = gameObject.AddComponent<LineRenderer>();

        m_Line.positionCount = 0;
    }

    private void FixedUpdate()
    {
        Defenestrate();
    }

    public void DrawLine(Window window)
    {
        m_Anchors = window.m_Anchors;
        m_WindowTransform = window.transform;

        m_Line.positionCount = 3;

        m_Line.SetPosition(0, m_Anchors[0].position);
        m_Line.SetPosition(1, transform.position);
        m_Line.SetPosition(2, m_Anchors[1].position);

        m_Body.isKinematic = false;
        m_Body.useGravity = false;

        m_Direction = (m_WindowTransform.position - transform.position).normalized;
    }

    private void Defenestrate()
    {
        if (m_Line.positionCount != 3) return;

        if (Vector3.Distance(transform.position, m_WindowTransform.position) > 0.1f)
        {
            m_Body.AddForce(m_Direction * m_Speed);
            m_Line.SetPosition(1, transform.position);
        }
        else EraseLine();
    }


    private void EraseLine()
    {
        m_Body.velocity /= 2;
        m_Body.useGravity = true;

        m_Line.positionCount = 0;

        Destroy(this, 2.0f);
    }
}
