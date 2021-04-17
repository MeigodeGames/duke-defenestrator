using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Window : MonoBehaviour
{
    [ReadOnly] public Transform[] m_Anchors = new Transform[2];



    //[SerializeField] private List<LineRenderer> m_Beams = new List<LineRenderer>();
    public LineRenderer[] m_Beams = new LineRenderer[2];
    [SerializeField] private EnemyController m_Target = null;
    private Vector3 m_Direction = Vector3.zero;

    public float m_Speed = 5.0f;

    private void Start()
    {
        m_Beams = GetComponentsInChildren<LineRenderer>();

        foreach (var beam in m_Beams)
        {
            beam.positionCount = 2;
            beam.SetPosition(0, beam.transform.position);
            beam.SetPosition(1, beam.transform.position - (Vector3.forward / 2));
        }
        
    }

    private void FixedUpdate()
    {
        SetupDefenestration();
        Defenestrate();
    }

    [ContextMenu("Draw Line")]
    public void SetupDefenestration()
    {
        if (!m_Target) return;

        foreach (var beam in m_Beams)
        {
            Vector3 step = Vector3.Lerp(beam.GetPosition(1), m_Target.transform.position, 2 * Time.fixedDeltaTime);
            //Vector3 step = (m_Target.transform.position - beam.GetPosition(1)) * 0.1f * m_Speed * Time.fixedDeltaTime;

            Debug.Log($"Enemy: {m_Target.transform.position} | Beam position: {beam.GetPosition(1)} | Step: {step}");

            beam.SetPosition(1, step);
        }
        /*
        for (int i = 0; i < 2; i++)
        {

            Vector3 step = (m_Target.transform.position - m_Beams[0].GetPosition(1)) * m_Speed * Time.fixedDeltaTime;
            m_Beams[i].SetPosition(1, step);
        }
        */

        if (Vector3.Distance(m_Beams[0].GetPosition(1), m_Beams[1].GetPosition(1)) < 0.1f)
        {
            m_Direction = (transform.position - m_Target.transform.position).normalized;

            m_Target.m_Body.isKinematic = false;
            m_Target.m_Body.useGravity = false;

            foreach (var beam in m_Beams)
                beam.SetPosition(1, m_Target.transform.position);
            /*
            for (int i = 0; i < 2; i++)
                m_Beams[i].SetPosition(1, m_Target.transform.position);
            */
        }
    }

    public void Defenestrate()
    {
        if (m_Direction.Equals(Vector3.zero)) return;

        if (Vector3.Distance(transform.position, m_Target.transform.position) > 0.5f)
        {
            m_Direction = (transform.position - m_Target.transform.position).normalized;
            m_Target.m_Body.AddForce(m_Direction * m_Speed);
            for (int i = 0; i < 2; i++)
                m_Beams[i].SetPosition(1, m_Target.transform.position);
        }
        else EraseLine();
    }

    private void EraseLine()
    {
        m_Direction = Vector3.zero;

        Debug.Log("Erase Line");
        m_Target.m_Body.velocity /= 2;
        m_Target.m_Body.AddForce(transform.forward * m_Speed * 10);
        m_Target.m_Body.isKinematic = false;
        m_Target.m_Body.useGravity = true;
        Destroy(m_Target.gameObject, 1.5f);
        Debug.Log("Target destroyed");
        m_Target = null;

        for (int i = 0; i < 2; i++)
            m_Beams[i].SetPosition(1, m_Beams[i].GetPosition(0));
    }
}
