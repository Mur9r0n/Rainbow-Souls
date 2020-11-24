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
    public GameObject m_InteractionPanel;
    public GameObject m_InventoryPanel;
    public GameObject m_ItemGridParent;
    public InventorySlot[] m_inventorySlots;
    public GameObject m_EquipmentPanel;
    public EquipmentSlot[] m_equipmentSlots;

    private void Start()
    {
        m_inventorySlots = m_ItemGridParent.GetComponentsInChildren<InventorySlot>();
        m_equipmentSlots = m_EquipmentPanel.GetComponentsInChildren<EquipmentSlot>();
    }

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
    
    public void UpdateSlotsUI()
    {
        for (int i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            m_inventorySlots[i].m_icon.sprite = InventoryManager.Instance.Inventory[i].Icon;
        }
        
        for (int i = 0; i < InventoryManager.Instance.Equipment.Count; i++)
        {
            m_equipmentSlots[i].m_Icon.sprite = InventoryManager.Instance.Equipment[i].m_icon;
        }
    }
    
    public void ShowInventory()
    {
        if (m_InventoryPanel.gameObject.activeSelf)
        {
            m_InventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            m_InventoryPanel.gameObject.SetActive(true);
        }
    }

    public void ShowInteractionTooltip(string _text)
    {
        m_InteractionPanel.SetActive(true);
        m_InteractionPanel.GetComponentInChildren<TextMeshProUGUI>().text = _text;
    }

    public void HideInteractionTooltip() {m_InteractionPanel.SetActive(false);}
    
    
    public void RefreshUI(float _maxHealth, float _currentHealth,float _maxStamina, float _currentStamina, int _pigmentAmount)
    {
        UpdateHealthBar(_maxHealth, _currentHealth);
        UpdateStaminaBar(_maxStamina,_currentStamina);
        UpdatePigmentCounter(_pigmentAmount);
        UpdateSlotsUI();
    }
}
