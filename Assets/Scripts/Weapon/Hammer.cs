using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    [Header("Hammer Parameters")]
    public int m_AttackPhase = 0;
    public float m_AttackAngle = 65;

    private void FixedUpdate()
    {
        VerticalSwing();
        VerticalPull();
    }

    [ContextMenu("Attack")]
    public override void Attack()
    {
        if (!IsReady) return;

        m_AttackPhase = 1;
        IsReady = false;
    }

    [ContextMenu("Pull")]
    public void Pull()
    {
        m_AttackPhase = 2;
    }

    private void VerticalSwing()
    {
        if (!m_AttackPhase.Equals(1)) return;

        //Debug.Log(transform.localRotation);
        Vector3 endRotation = transform.rotation.eulerAngles;
        endRotation.x = m_AttackAngle;

        //var rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(m_AttackAngle, 0, 0), m_AttackSpeed * Time.fixedDeltaTime);
        var rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(endRotation), m_AttackSpeed * Time.fixedDeltaTime);
        transform.rotation = rotation;
        //transform.Rotate(rotation.eulerAngles, Space.Self);
        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(endRotation)) < 0.5f)
        {
            m_AttackPhase++;
        }
    }

    private void VerticalPull()
    {
        if (!m_AttackPhase.Equals(2)) return;

        Vector3 endRotation = transform.rotation.eulerAngles;
        endRotation.x = 0;

        var rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(endRotation), m_AttackSpeed * Time.fixedDeltaTime);
        transform.rotation = rotation;

        if (Quaternion.Angle(transform.rotation, Quaternion.Euler(endRotation)) < 0.5f)
        {
            m_AttackPhase = 0;
            StartCoroutine(Cooldown());
            //m_AttackPhase++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit - Hammer");
        m_AttackPhase = 2;
        Health otherHealth = other.gameObject.GetComponent<Health>();


        if (otherHealth)
        {
            Debug.Log($"Damaging Health of {other.gameObject}");
            otherHealth.Damage(m_AttackDamage);
        }
    }
}
