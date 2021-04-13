using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyController : MonoBehaviour
{
    public Window m_WindowTest;
    public Rigidbody m_Body;
    private Health m_Health;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Health = GetComponent<Health>();
    }

    public bool IsDefenestrable => m_Health.IsZero;

    public void Defenestrate(Window window)
    {
        if (!IsDefenestrable) return;

        gameObject.AddComponent<DefenestrateEnemy>().DrawLine(window);
    }

    [ContextMenu("Defenestrate")]
    private void DefenestrateTest()
    {
        Defenestrate(m_WindowTest);
    }
}
