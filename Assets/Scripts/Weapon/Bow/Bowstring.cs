using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowstring : MonoBehaviour
{
    private LineRenderer m_Bowstring;
    [SerializeField] private Transform[] m_Points = new Transform[3];

    private void Awake()
    {
        m_Bowstring = GetComponent<LineRenderer>();
        m_Bowstring.positionCount = m_Points.Length;
    }

    public void DrawBowstring(Transform arrowPosition) => m_Points[1] = arrowPosition;

    private void FixedUpdate()
    {
        for (int i = 0; i < m_Points.Length; i++)
            m_Bowstring.SetPosition(i, m_Points[i].position);
    }
}
