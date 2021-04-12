using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody m_Body;
    private bool m_IsFlying = false;
    //private bool m_HasHit = true;
    [SerializeField] private float m_Damage = 10;


    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        m_Body.isKinematic = true;
    }

    private void FixedUpdate()
    {
        /*
        if (m_HasHit) return;

        if (m_Body.velocity != Vector3.zero) m_Body.MoveRotation(Quaternion.LookRotation(m_Body.velocity, transform.up));
        */

        if (!m_IsFlying) return;
        m_Body.MoveRotation(Quaternion.LookRotation(m_Body.velocity, transform.up));
    }

    private void OnTriggerEnter(Collider other)
    {
        Stop(other.transform);

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().Damage(m_Damage);
        }
    }

    private void Stop(Transform newParent)
    {
        //m_HasHit = true;

        //m_Body.velocity = Vector3.zero;
        //m_Body.angularVelocity = Vector3.zero;
        m_Body.constraints = RigidbodyConstraints.FreezeAll;
        m_Body.isKinematic = true;
        m_IsFlying = false;

        transform.SetParent(newParent);
        
        Destroy(gameObject, 2.0f);
    }

    public void Fire(float force)
    {
        m_Body.isKinematic = false;
        m_Body.AddForce(transform.forward * force);
        m_IsFlying = true;

        transform.SetParent(null);

        //m_HasHit = false;
    }

    public void Fire(float force, float damageMultiplier)
    {
        m_Damage *= damageMultiplier;
        Fire(force);
    }
}
