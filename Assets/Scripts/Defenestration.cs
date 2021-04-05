using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Defenestration : MonoBehaviour
{
    public Transform m_PontoEsquerdo;
    public Transform m_PontoDireito;
    public float m_Speed = 10.0f;

    private LineRenderer m_Line;
    private Vector3 m_Direction;

    public Enemy m_Target = null;

    private void Awake()
    {
        m_Line = GetComponent<LineRenderer>();
        m_Line.positionCount = 0;

        DrawLine();
    }

    public void DrawLine()
    {
        m_Line.positionCount = 3;

        m_Line.SetPosition(0, m_PontoEsquerdo.position);
        m_Line.SetPosition(1, m_Target.transform.position);
        m_Line.SetPosition(2, m_PontoDireito.position);

        
        m_Direction = (this.transform.position - m_Target.transform.position).normalized;
    }

    [ContextMenu("Defenestrate")]
    private void Defenestrate()
    {
        if (!m_Target) return;

        //m_Target.m_Body.isKinematic = true;
        m_Target.m_Body.useGravity = false;

        if (Vector3.Distance(this.transform.position, m_Target.transform.position) > 1.0f)
        {
            //Debug.Log($"Movendo de {m_Target.transform.position} para {this.transform.position}");
            m_Target.m_Body.AddForce(m_Direction * m_Speed);
            //m_Target.m_Body.MovePosition(m_Target.transform.position + m_Direction * Time.deltaTime * m_Speed * 10);

            //Debug.Log($"Desenhando em {m_Target.transform.position}");
            m_Line.SetPosition(1, m_Target.transform.position);
        } else
        {
            EraseLine();
        }
    }

    [ContextMenu("Reset")]
    private void EraseLine()
    {
        m_Target.m_Body.velocity /= 2;
        m_Target.m_Body.isKinematic = false;
        m_Target.m_Body.useGravity = true;
        m_Target = null;
        m_Line.positionCount = 0;
    }

    private void FixedUpdate()
    {
        Defenestrate();
    }
}
