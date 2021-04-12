using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Defenestration : MonoBehaviour
{
    [SerializeField] private Transform m_Left;
    [SerializeField] private Transform m_Right;
    private LineRenderer m_Line;
    private Vector3 m_Direction;

    [Header("Target")]
    public float m_Speed = 10.0f;
    [SerializeField] private EnemyController m_Target = null;

    private void Awake()
    {
        m_Line = GetComponent<LineRenderer>();
        m_Line.positionCount = 0;

        //DrawLine();
    }

    private void FixedUpdate()
    {
        Defenestrate();
    }

    [ContextMenu("Draw Line")]
    public void DrawLine()
    {
        if (!m_Target) return;

        m_Line.positionCount = 3;

        m_Line.SetPosition(0, m_Left.position);
        m_Line.SetPosition(1, m_Target.transform.position);
        m_Line.SetPosition(2, m_Right.position);

        m_Direction = (this.transform.position - m_Target.transform.position).normalized;
    }

    public void DrawLine(EnemyController enemy)
    {
        if (!enemy.IsDefenestrable) return;

        m_Target = enemy;
        DrawLine();
    }

    [ContextMenu("Defenestrate")]
    private void Defenestrate()
    {
        if (!m_Target || m_Line.positionCount == 0) return;

        m_Target.m_Body.isKinematic = false;
        m_Target.m_Body.useGravity = false;

        if (Vector3.Distance(this.transform.position, m_Target.transform.position) > 1.0f)
        {
            m_Target.m_Body.AddForce(m_Direction * m_Speed);
            m_Line.SetPosition(1, m_Target.transform.position);
        }
        else EraseLine();
    }

    public void Defenestrate(EnemyController enemy)
    {
        if (!enemy.IsDefenestrable) return;

        m_Target = enemy;
        Defenestrate();
    }

    [ContextMenu("Reset")]
    private void EraseLine()
    {
        Debug.Log("Erase Line");
        m_Target.m_Body.velocity /= 2;
        m_Target.m_Body.isKinematic = false;
        m_Target.m_Body.useGravity = true;
        Destroy(m_Target.gameObject, 1.5f);
        Debug.Log("Target destroyed");
        m_Target = null;
        m_Line.positionCount = 0;
    }
}
