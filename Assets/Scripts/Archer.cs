using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class Archer : MonoBehaviour
{
    private Bow m_Bow;

    [ContextMenu("Attack")]
    private void Attack()
    {
        m_Bow.Shoot();
    }

    private void Awake()
    {
        m_Bow = GetComponentInChildren<Bow>();
    }
}
