using System;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float m_MaxHealth = 100;
    [SerializeField] private float m_Health = 100;
    [SerializeField] private HealthBar m_HealthBar;

    [Header("Health Multipliers")]
    public float m_HealingMultiplier = 1;
    public float m_DefenseMultiplier = 1;

    private void Awake()
    {
        m_HealthBar.UpdateValue(m_Health, m_MaxHealth);
    }

    public void Damage(float value)
    {
        m_Health = Math.Max(m_Health - (value * m_DefenseMultiplier), 0);
        m_HealthBar.UpdateValue(m_Health);
    }

    public void Heal(float value)
    {
        m_Health = Math.Min(m_Health + (value * m_HealingMultiplier), m_MaxHealth);
        m_HealthBar.UpdateValue(m_Health);
    }

    public bool IsZero => (m_Health == 0);

    [ContextMenu("Take Damage (10)")]
    public void TakeFixedDamageTest()
    {
        Damage(10.0f);
    }
}
