using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health.")]
    [Tooltip("Maximum Healthpoints.")] public float m_MaxHealthPoints = 100f;
    [Tooltip("Current Healthpoints.")] public float m_CurrentHealthPoints = 100f;

    [Header("Stamina.")]
    [Tooltip("Maximum StaminaPoints.")] public float m_MaxStaminaPoints = 100f;
    [Tooltip("Current StaminaPoints.")] public float m_CurrentStaminaPoints = 100f;

    [Header("Level and Pigments.")]
    [Tooltip("Level of the Player.")] public int m_Level = 1;
    [Tooltip("Amount of Pigments the Player has.")] public int m_Pigments = 0;
    
    [Header("Attributes.")]
    [Tooltip("Vitality Statpoints.")] public int m_Vitality = 10;
    [Tooltip("Constitution Statpoints.")] public int m_Constitution = 10;
    [Tooltip("Strength Statpoints.")] public int m_Strength = 10;
    [Tooltip("Dexterity Statpoints.")] public int m_Dexterity = 10;

    [Header("Movementspeed.")]
    [Tooltip("MovementSpeeds of the Player.")] public Vector2 m_MovementSpeed= new Vector2(5.0f, 10.0f);
    [Tooltip("Player is Sprinting.")] public bool m_isSprinting = false;
    
    [Header("Damage.")]
    [Tooltip("Base Damage.")] public float m_BaseDamage = 10.0f;
    [Tooltip("Actual Damage Dealt.")] public float m_ActualDamage = 0f;
    
    [Header("Target.")]
    [Tooltip("Targeted Enemy.")] public GameObject m_targetEnemy = null;

    
    private void Update()
    {
        if (m_CurrentStaminaPoints < m_MaxStaminaPoints)
        {
            m_CurrentStaminaPoints += Mathf.Clamp(Time.deltaTime * (m_Dexterity *1.5f), 0, m_MaxStaminaPoints);
            UIManager.Instance.UpdateStaminaBar(m_MaxStaminaPoints,m_CurrentStaminaPoints);
        }
    }
}
