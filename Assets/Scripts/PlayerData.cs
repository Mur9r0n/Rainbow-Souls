using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float m_MaxHealth;
    public float m_CurrentHealth;

    public float m_MaxStamina;
    public float m_CurrentStamina;

    public int m_Level;
    public int m_Pigments;

    public int m_Vitality;
    public int m_Constitution;
    public int m_Strength;
    public int m_Dexterity;
    
    public float[] m_Position;

    public PlayerData(PlayerStats _playerStats)
    {
        m_MaxHealth = _playerStats.m_MaxHealthPoints;
        m_CurrentHealth = _playerStats.m_CurrentHealthPoints;

        m_MaxStamina = _playerStats.m_MaxStaminaPoints;
        m_CurrentStamina = _playerStats.m_CurrentStaminaPoints;

        m_Level = _playerStats.m_Level;
        m_Pigments = _playerStats.m_Pigments;

        m_Vitality = _playerStats.m_Vitality;
        m_Constitution = _playerStats.m_Constitution;
        m_Strength = _playerStats.m_Strength;
        m_Dexterity = _playerStats.m_Dexterity;

        m_Position = new float[3];
        m_Position[0] = _playerStats.transform.position.x;
        m_Position[1] = _playerStats.transform.position.y;
        m_Position[2] = _playerStats.transform.position.z;
    }
}
