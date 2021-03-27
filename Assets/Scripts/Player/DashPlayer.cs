using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DashPlayer : MonoBehaviour
{
    public bool IsDashing { get; set; } = false;

    public Vector2 Movement = Vector2.zero;
    public float m_DashDistance = 100.0f;

    private Rigidbody m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Debug.Log($"[{this.name}] FixedUpdate {Movement}");
        Dash();
    }

    private void Dash()
    {
        if (!IsDashing) return;

        var position = m_Body.position;
        position += transform.forward * m_DashDistance;

        Debug.Log($"{Movement} | {m_Body.position} | {position} | {Movement.y * m_DashDistance}");

        m_Body.MovePosition(position);
        IsDashing = false;
    }    
}
