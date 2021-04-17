using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [Header("General Parameters")]
    public float m_CooldownTime;
    public float m_AttackSpeed;
    public float m_AttackDamage;

    public abstract void Attack();

    public bool IsReady { get; set; } = true;

    public IEnumerator Cooldown()
    {
        IsReady = false;
        yield return new WaitForSeconds(m_CooldownTime);

        IsReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit - Weapon");
    }
}
