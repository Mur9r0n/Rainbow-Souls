﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //Maybe
    private PlayerInputs m_playerInputs = null;
    private PlayerStats m_playerStats = null;
    private CharacterController m_controller = null;
    private InteractManager m_interactManager;
    private Interactables m_interactable;
    private UIManager m_uiManager;

    void Awake()
    {
        m_playerInputs = new PlayerInputs();
        m_playerStats = GetComponent<PlayerStats>();
        m_controller = GetComponent<CharacterController>();

        m_playerInputs.Player.Use.performed += _ => Use();
        m_playerInputs.Player.Interaction.performed += _ => Interact();
        m_playerInputs.Player.SwitchItems.performed += _ => SwitchItems(m_playerInputs.Player.SwitchItems.ReadValue<float>());
        m_playerInputs.Player.OpenInventory.performed += _ => OpenInventory();
        m_playerInputs.Player.OpenMenu.performed += _ => OpenMenu();
    }

    void Start()
    {
        InventoryManager.Instance.Inventory.Clear();
        UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,
                                        m_playerStats.m_CurrentHealthPoints,
                                        m_playerStats.m_MaxStaminaPoints,
                                        m_playerStats.m_CurrentStaminaPoints,
                                        m_playerStats.m_Pigments);
        
        m_interactManager = InteractManager.Instance;
        m_uiManager = UIManager.Instance;
    }
    
    public void Use()
    {
        Debug.Log("Use");
    }
    
    public void Interact()
    {
        if (m_interactManager.m_interactables.Count > 0)
        {
            m_interactable = m_interactManager.LookForClosestInteraction();
            m_interactable.Interact();
            UIManager.Instance.HideInteractionTooltip();
        }
        else if (m_interactManager.m_interactables.Count == 0)
        {
            Debug.Log("Interactable List is Empty");
        }
    }

    public void SwitchItems(float _context)
    {
        Debug.Log("Switch Items");
    }

    public void OpenInventory()
    {
        m_uiManager.ShowInventory();
        m_uiManager.UpdateSlotsUI();
    }

    public void OpenMenu()
    {
        Debug.Log("Open Menu");
    }
    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Damage"))
        {
            m_playerStats.m_CurrentHealthPoints -= 10;
        }

        if (GUI.Button(new Rect(10, 150, 150, 25), "Save 1"))
        {
            DataManager.Instance.SavePlayer(m_playerStats, 1);
            Debug.Log("Saved 1");
        }
        
        if (GUI.Button(new Rect(10, 180, 150, 25), "Save 2"))
        {
            DataManager.Instance.SavePlayer(m_playerStats, 2);
            Debug.Log("Saved 2");
        }
        
        if (GUI.Button(new Rect(10, 210, 150, 25), "Save 3"))
        {
            DataManager.Instance.SavePlayer(m_playerStats, 3);
            Debug.Log("Saved 3");
        }

        if (GUI.Button(new Rect(10, 300, 150, 25), "Load 1"))
        {
            PlayerData temp = DataManager.Instance.LoadPlayer(1);

            m_playerStats.m_MaxHealthPoints = temp.m_MaxHealth;
            m_playerStats.m_CurrentHealthPoints = temp.m_CurrentHealth;

            m_playerStats.m_MaxStaminaPoints = temp.m_MaxStamina;
            m_playerStats.m_CurrentStaminaPoints = temp.m_CurrentStamina;

            m_playerStats.m_Level = temp.m_Level;
            m_playerStats.m_Pigments = temp.m_Pigments;

            m_playerStats.m_Vitality = temp.m_Vitality;
            m_playerStats.m_Constitution = temp.m_Constitution;
            m_playerStats.m_Strength = temp.m_Strength;
            m_playerStats.m_Dexterity = temp.m_Dexterity;

            Vector3 temppos = new Vector3(temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]);
            m_controller.enabled = false;
            transform.position = temppos;
            m_controller.enabled = true;

            UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,m_playerStats.m_CurrentHealthPoints,m_playerStats.m_MaxStaminaPoints,
                m_playerStats.m_CurrentStaminaPoints,m_playerStats.m_Pigments);
        }
        
        if (GUI.Button(new Rect(10, 330, 150, 25), "Load 2"))
        {
            PlayerData temp = DataManager.Instance.LoadPlayer(2);

            m_playerStats.m_MaxHealthPoints = temp.m_MaxHealth;
            m_playerStats.m_CurrentHealthPoints = temp.m_CurrentHealth;

            m_playerStats.m_MaxStaminaPoints = temp.m_MaxStamina;
            m_playerStats.m_CurrentStaminaPoints = temp.m_CurrentStamina;

            m_playerStats.m_Level = temp.m_Level;
            m_playerStats.m_Pigments = temp.m_Pigments;

            m_playerStats.m_Vitality = temp.m_Vitality;
            m_playerStats.m_Constitution = temp.m_Constitution;
            m_playerStats.m_Strength = temp.m_Strength;
            m_playerStats.m_Dexterity = temp.m_Dexterity;

            Vector3 temppos = new Vector3(temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]);
            m_controller.enabled = false;
            transform.position = temppos;
            m_controller.enabled = true;

            UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,m_playerStats.m_CurrentHealthPoints,m_playerStats.m_MaxStaminaPoints,
                m_playerStats.m_CurrentStaminaPoints,m_playerStats.m_Pigments);
            
            Debug.Log(temppos);
        }
        
        if (GUI.Button(new Rect(10, 360, 150, 25), "Load 3"))
        {
            PlayerData temp = DataManager.Instance.LoadPlayer(3);

            m_playerStats.m_MaxHealthPoints = temp.m_MaxHealth;
            m_playerStats.m_CurrentHealthPoints = temp.m_CurrentHealth;

            m_playerStats.m_MaxStaminaPoints = temp.m_MaxStamina;
            m_playerStats.m_CurrentStaminaPoints = temp.m_CurrentStamina;

            m_playerStats.m_Level = temp.m_Level;
            m_playerStats.m_Pigments = temp.m_Pigments;

            m_playerStats.m_Vitality = temp.m_Vitality;
            m_playerStats.m_Constitution = temp.m_Constitution;
            m_playerStats.m_Strength = temp.m_Strength;
            m_playerStats.m_Dexterity = temp.m_Dexterity;

            Vector3 temppos = new Vector3(temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]);
            m_controller.enabled = false;
            transform.position = temppos;
            m_controller.enabled = true;

            UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,m_playerStats.m_CurrentHealthPoints,m_playerStats.m_MaxStaminaPoints,
                m_playerStats.m_CurrentStaminaPoints,m_playerStats.m_Pigments);
            
            Debug.Log(temppos);
        }
    }
    
    private void OnEnable()
    {
        m_playerInputs.Enable();
    }

    private void OnDisable()
    {
        m_playerInputs.Disable();
    }
}
