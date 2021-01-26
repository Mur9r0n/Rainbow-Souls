using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelMenuBehaviours : MonoBehaviour
{
    //TODO FIX THIS MOTHERFUCKING PIECE OF SHIT SCRIPT
    #region SerializeFields

    [Header("Level/Pigments")] [SerializeField]
    private TextMeshProUGUI m_currentLevel;

    [SerializeField] private TextMeshProUGUI m_finalLevel;
    [SerializeField] private TextMeshProUGUI m_neededPigements;
    [SerializeField] private TextMeshProUGUI m_pigments;

    [Header("Attributes")] [SerializeField]
    private TextMeshProUGUI m_vitalityAmount;

    [SerializeField] private TextMeshProUGUI m_constitutionAmount;
    [SerializeField] private TextMeshProUGUI m_strengthAmount;
    [SerializeField] private TextMeshProUGUI m_dexterityAmount;

    [Header("Attributes Buttons")] 
    [SerializeField] private Button m_vitalityLessButton;
    [SerializeField] private Button m_constitutionLessButton;
    [SerializeField] private Button m_strengthLessButton;
    [SerializeField] private Button m_dexterityLessButton;
    [SerializeField] private Button m_vitalityGreaterButton;
    [SerializeField] private Button m_constitutionGreaterButton;
    [SerializeField] private Button m_strengthGreaterButton;
    [SerializeField] private Button m_dexterityGreaterButton;

    [Header("PlayerStats")] [SerializeField]
    private TextMeshProUGUI m_health;

    [SerializeField] private TextMeshProUGUI m_stamina;
    [SerializeField] private TextMeshProUGUI m_damage;
    [SerializeField] private TextMeshProUGUI m_movementSpeed;
    [SerializeField] private TextMeshProUGUI m_physicalDefense;
    [SerializeField] private TextMeshProUGUI m_bleedingResistance;
    [SerializeField] private TextMeshProUGUI m_poisonResistance;

    [Header("Confirm Button")] [SerializeField]
    private Button m_cancel;

    [SerializeField] private Button m_confirm;

    #endregion

    private PlayerStats m_playerStats = null;
    private int m_TLEV;
    private int m_TVIT;
    private int m_TCON;
    private int m_TSTR;
    private int m_TDEX;
    private int m_TPIG;

    private int m_levelGained = 0;
    
    //TEMP
    private int pigmentsneeded = 5;

    private void Start()
    {
        m_playerStats = FindObjectOfType<PlayerStats>();
        m_TLEV = m_playerStats.m_Level;
        m_TVIT = m_playerStats.m_Vitality;
        m_TCON = m_playerStats.m_Constitution;
        m_TSTR = m_playerStats.m_Strength;
        m_TDEX = m_playerStats.m_Dexterity;
        m_TPIG = m_playerStats.m_Pigments;
    }

    public void Update()
    {
        if (m_TPIG >= pigmentsneeded)
        {
            m_vitalityGreaterButton.interactable = true;
            m_constitutionGreaterButton.interactable = true;
            m_strengthGreaterButton.interactable = true;
            m_dexterityGreaterButton.interactable = true;
        }
        else
        {
            m_vitalityGreaterButton.interactable = false;
            m_constitutionGreaterButton.interactable = false;
            m_strengthGreaterButton.interactable = false;
            m_dexterityGreaterButton.interactable = false;
        }

        if (m_levelGained == 0)
        {
            m_vitalityLessButton.interactable = false;
            m_constitutionLessButton.interactable = false;
            m_strengthLessButton.interactable = false;
            m_dexterityLessButton.interactable = false;
        }

        m_currentLevel.text = m_TLEV.ToString();
        m_finalLevel.text = (m_TLEV + m_levelGained).ToString();
        m_neededPigements.text = (m_TLEV * 5).ToString();
        m_pigments.text = m_TPIG.ToString();

        m_vitalityAmount.text = m_TVIT.ToString();
        m_constitutionAmount.text = m_TCON.ToString();
        m_strengthAmount.text = m_TSTR.ToString();
        m_dexterityAmount.text = m_TDEX.ToString();

        // m_health.text = m_temporaryPlayerStats.m_MaxHealthPoints.ToString();
        // m_stamina.text = m_temporaryPlayerStats.m_MaxStaminaPoints.ToString();
        // m_damage.text = m_temporaryPlayerStats.m_BaseDamage.ToString();
        // m_movementSpeed.text = m_temporaryPlayerStats.m_MovementSpeed.x.ToString();
        // m_physicalDefense.text = m_temporaryPlayerStats.m_PhysicalDefense.ToString();
        // m_bleedingResistance.text = m_temporaryPlayerStats.m_BleedingResistance.ToString();
        // m_poisonResistance.text = m_temporaryPlayerStats.m_PoisonResistance.ToString();
    }

    public void IncreaseStat(int _id)
    {
        switch (_id)
        {
            case 0:
            {
                m_TVIT++;
                m_TPIG -= pigmentsneeded;
                m_levelGained++;
                m_vitalityLessButton.interactable = true;
                break;
            }
            case 1:
            {
                m_TCON++;
                m_TPIG -= pigmentsneeded;
                m_levelGained++;
                m_constitutionLessButton.interactable = true;
                break;
            }
            case 2:
            {
                m_TSTR++;
                m_TPIG -= pigmentsneeded;
                m_levelGained++;
                m_strengthLessButton.interactable = true;
                break;
            }
            case 3:
            {
                m_TDEX++;
                m_TPIG -= pigmentsneeded;
                m_levelGained++;
                m_dexterityLessButton.interactable = true;
                break;
            }
        }
    }

    public void DecreaseStat(int _id)
    {
        if (m_levelGained >= 1)
        {
            switch (_id)
            {
                case 0:
                {
                    m_TVIT--;
                    m_TPIG += pigmentsneeded;
                    m_levelGained--;
                    break;
                }
                case 1:
                {
                    m_TCON--;
                    m_TPIG += pigmentsneeded;
                    m_levelGained--;
                    break;
                }
                case 2:
                {
                    m_TSTR--;
                    m_TPIG += pigmentsneeded;
                    m_levelGained--;
                    break;
                }
                case 3:
                {
                    m_TDEX--;
                    m_TPIG += pigmentsneeded;
                    m_levelGained--;
                    break;
                }
            }
        }
    }

    public void Cancel()
    {
        UIManager.Instance.m_LevelingPanel.SetActive(false);
        UIManager.Instance.m_CheckPointMenuPanel.SetActive(true);
    }

    public void Confirm()
    {
        m_playerStats.m_Level = m_TLEV;
        m_playerStats.m_Vitality = m_TVIT;
        m_playerStats.m_Constitution = m_TCON;
        m_playerStats.m_Strength = m_TSTR;
        m_playerStats.m_Dexterity = m_TDEX;
        m_playerStats.m_Pigments = m_TPIG;
        UIManager.Instance.m_LevelingPanel.SetActive(false);
        UIManager.Instance.m_CheckPointMenuPanel.SetActive(true);
    }
}