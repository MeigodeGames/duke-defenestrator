using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody m_Body;
    private bool m_HasHit = true;
    public float m_ArrowForce = 50.0f;


    // Start is called before the first frame update
    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        //m_Body.rotation = Quaternion.LookRotation(m_Body.velocity);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_HasHit) return;

        //Debug.Log($"{m_Body.velocity} | {transform.up}");

        if (m_Body.velocity != Vector3.zero) m_Body.MoveRotation(Quaternion.LookRotation(m_Body.velocity, transform.up));

        //m_Body.rotation = Quaternion.LookRotation(m_Body.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Stop(other.transform);
    }

    private void Stop(Transform newParent)
    {
        m_HasHit = true;

        //m_Body.velocity = Vector3.zero;
        //m_Body.angularVelocity = Vector3.zero;
        m_Body.constraints = RigidbodyConstraints.FreezeAll;
        m_Body.isKinematic = true;

        transform.SetParent(newParent);
        
        Destroy(gameObject, 2.0f);
    }

    public void Fire()
    {
        m_Body.isKinematic = false;
        m_Body.AddForce(transform.forward * m_ArrowForce);

        m_HasHit = false;
    }
}
