using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    public Vector2 Movement = Vector2.zero;

    public float m_MoveSpeed = 2.0f;
    public float m_RotateSpeed = 45.0f;
    private Rigidbody m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Debug.Log($"[{this.name}] FixedUpdate {Movement}");
        Move();
        Turn();
    }

    private void Move()
    {
        var position = m_Body.position;
        position += transform.forward * Movement.y * m_MoveSpeed * Time.deltaTime;

        //Debug.Log($"{Movement} | {m_Body.position} | {position} | {Movement.y * m_MoveSpeed}");

        m_Body.MovePosition(position);
    }

    private void Turn()
    {
        var rotation = m_Body.rotation.eulerAngles;
        rotation.y += Movement.x * m_RotateSpeed * Time.deltaTime;
        m_Body.MoveRotation(Quaternion.Euler(rotation));
    }
}
