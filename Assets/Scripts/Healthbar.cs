using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField, Tooltip("Slider for Healthbar")]
    private Slider m_slider;

    [SerializeField, Tooltip("Gradient for Healthbar")]
    private Gradient m_gradient;

    [SerializeField, Tooltip("Fill of the Healthbar")]
    private Image m_fill;

    public void GetCurrentHealth(float _currentHealth)
    {
        m_slider.value = _currentHealth;

        m_fill.color = m_gradient.Evaluate(m_slider.normalizedValue);
    }

    public void GetMaxHealth(float _maxHealth)
    {
        m_slider.maxValue = _maxHealth;
        m_slider.value = _maxHealth;

        m_fill.color = m_gradient.Evaluate(1f); 
    }
    
    
    
}
