using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    public Image m_BarUI;
    public Text m_TextUI;

    [Header("Color")]
    public Color m_MinColor = new Color(0.90f, 0.29f, 0.23f, 1.0f);
    public Color m_MaxColor = new Color(0.94f, 0.76f, 0.06f, 1.0f);

    public float m_MaxValue = 100.0f;
    public float m_CurrentValue = 100.0f;

    public void Awake()
    {
        UpdateValue(m_CurrentValue);
    }

    public void UpdateValue(float current, float max)
    {
        m_MaxValue = max;
        UpdateValue(current);
    }

    public void UpdateValue(float value)
    {
        m_CurrentValue = Mathf.Clamp(value, 0.0f, m_MaxValue);
        float valueRatio = m_CurrentValue / m_MaxValue;
        if (m_BarUI)
        {
            m_BarUI.fillAmount = valueRatio;
            m_BarUI.color = Color.Lerp(m_MinColor, m_MaxColor, valueRatio);
        }

        if (m_TextUI)
        {
            m_TextUI.text = $"{(int)m_CurrentValue} / {(int)m_MaxValue}";
        }
    }
}
