using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpPlayer : MonoBehaviour
{
    public bool IsJumping { get; set; }
    public float m_JumpForce = 100.0f;

    public Transform m_Foot;

    private Rigidbody m_Body;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Debug.Log($"[{this.name}] Update: {IsJumping} | {IsGrounded}");
        Jump();

        if (IsGrounded) IsJumping = false;
    }

    private bool IsGrounded => Physics.Raycast(m_Foot.position, Vector3.down, 0.1f);
    
    private void Jump()
    {
        //Debug.Log($"[{this.name}] Update: {IsJumping} | {IsGrounded}");

        if (!IsJumping) return;
        if (!IsGrounded) return;

        //Debug.Log("Jump OUT");

        m_Body.AddForce(Vector3.up * m_JumpForce);
    }
}
