using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion
    
    public Image m_Healthbar;
    public Image m_Staminabar;
    public TMP_Text m_Pigment;
    
    public void UpdateHealthBar(float _maxHealth, float _currentHealth)
    {
        m_Healthbar.fillAmount = _currentHealth / _maxHealth;
    }

    public void UpdateStaminaBar(float _maxStamina, float _currentStamina)
    {
        m_Staminabar.fillAmount = _currentStamina / _maxStamina;
    }

    public void UpdatePigmentCounter(int _pigmentAmount)
    {
        m_Pigment.text = _pigmentAmount.ToString();
    }
    
    public void RefreshUI(float _maxHealth, float _currentHealth,float _maxStamina, float _currentStamina, int _pigmentAmount)
    {
        UpdateHealthBar(_maxHealth, _currentHealth);
        UpdateStaminaBar(_maxStamina,_currentStamina);
        UpdatePigmentCounter(_pigmentAmount);
    }
    
}
