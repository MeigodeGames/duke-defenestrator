using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public bool IsAttacking { get; set; } = false;

    //[SerializeField] private GameObject m_WeaponSlot;
    private Weapon m_Weapon;

    void Awake()
    {
        m_Weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Attack();
    }

    private void Attack()
    {
        if (!IsAttacking) return;

        m_Weapon.Attack();
        IsAttacking = false;
    }
}
