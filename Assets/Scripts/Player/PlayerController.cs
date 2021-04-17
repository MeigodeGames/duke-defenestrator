using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MovePlayer))]
[RequireComponent(typeof(JumpPlayer))]
[RequireComponent(typeof(DashPlayer))]
[RequireComponent(typeof(AttackPlayer))]
public class PlayerController : MonoBehaviour
{
    private MovePlayer m_MoveBehaviour;
    private JumpPlayer m_JumpBehaviour;
    private DashPlayer m_DashBehaviour;
    private AttackPlayer m_AttackBehaviour;

    //public PlayerInput m_PlayerInput;
    //public InputAction m_JumpAction;

    private float m_Horizontal;
    private float m_Vertical;
    public bool m_Jump;

    private void Awake()
    {
        m_MoveBehaviour = GetComponent<MovePlayer>();
        m_JumpBehaviour = GetComponent<JumpPlayer>();
        m_DashBehaviour = GetComponent<DashPlayer>();
        m_AttackBehaviour = GetComponent<AttackPlayer>();

        //m_PlayerInput = GetComponent<PlayerInput>();
        //m_JumpAction = m_PlayerInput.actions["Jump"];
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log($"[{this.name}] Update");

        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //bool jump = Input.GetButtonDown("Jump");

        //Debug.Log($"[{this.name}] Update: {horizontal} | {vertical} | {jump}");

        //var Movement = new Vector2(m_Horizontal, m_Vertical);

        m_MoveBehaviour.Movement.Set(m_Horizontal, m_Vertical);
        //Debug.Log(m_Jump);
    }

    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        m_Horizontal = input.x;
        m_Vertical = input.y;

        //m_Horizontal = Input.GetAxis("Horizontal");
        //m_Vertical = Input.GetAxis("Vertical");
    }
    
    public void OnJump(InputValue value)
    {
        m_JumpBehaviour.IsJumping = value.isPressed;
        //m_Jump = value.isPressed;

        Debug.Log("Jump");
        //Debug.Log(value.isPressed);
        //m_Jump = value.isPressed;
        //m_Jump = Input.GetButtonDown("Jump");
    }
    
    public void OnDash(InputValue value)
    {
        m_DashBehaviour.IsDashing = value.isPressed;

        Debug.Log("Dash");
    }

    public void OnAttack(InputValue value)
    {
        m_AttackBehaviour.IsAttacking = value.isPressed;

        Debug.Log("Attack");
    }
}
