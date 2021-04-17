using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimation : MonoBehaviour
{
    private Animator anim;

    public float m_CooldownTime;
    public float m_AttackSpeed;

    [ContextMenu("Attack")]
    public void Attack()
    {
        if (!IsReady) return;

        IsReady = false;
        anim.speed *= m_AttackSpeed;
        anim.SetTrigger("Attack");
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        IsReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit;");
        anim.SetTrigger("Hit");
    }

    public bool IsReady { get; set; } = true;

    public void EnterCooldown()
    {
        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        Debug.Log("Enter Cooldown");
        yield return new WaitForSeconds(m_CooldownTime);
        Debug.Log("Exit Cooldown");

        IsReady = true;
    }
}
