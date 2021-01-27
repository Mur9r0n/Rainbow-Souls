using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameData : MonoBehaviour
{
    public static GlobalGameData Instance { get; private set; }

    public bool m_DataFromFile = false;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        #endregion

        SceneManager.sceneLoaded += TriggerStart;
    }
    
    [Header("Player Data from File")] public PlayerData m_PlayerData;
    [Header("World Data from File")] public WorldData m_WorldData;
    [Header("Inventory Data from File")] public InventoryData m_InventoryData;
    
    void Start()
    {
        if (m_DataFromFile && SceneManager.GetActiveScene().buildIndex != 0)
        {
            PlayerStats m_playerStats = FindObjectOfType<PlayerStats>();
            CharacterController m_controller = FindObjectOfType<CharacterController>();
            
            m_playerStats.m_MaxHealthPoints = m_PlayerData.m_MaxHealth;
            m_playerStats.m_CurrentHealthPoints = m_PlayerData.m_CurrentHealth;

            m_playerStats.m_MaxStaminaPoints = m_PlayerData.m_MaxStamina;
            m_playerStats.m_CurrentStaminaPoints = m_PlayerData.m_CurrentStamina;

            m_playerStats.m_Level = m_PlayerData.m_Level;
            m_playerStats.m_Pigments = m_PlayerData.m_Pigments;

            m_playerStats.m_Vitality = m_PlayerData.m_Vitality;
            m_playerStats.m_Constitution = m_PlayerData.m_Constitution;
            m_playerStats.m_Strength = m_PlayerData.m_Strength;
            m_playerStats.m_Dexterity = m_PlayerData.m_Dexterity;

            Vector3 temppos = new Vector3(m_PlayerData.m_Position[0], m_PlayerData.m_Position[1], m_PlayerData.m_Position[2]);
            m_controller.enabled = false;
            m_controller.transform.position = temppos;
            m_controller.enabled = true;

            UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,m_playerStats.m_CurrentHealthPoints,m_playerStats.m_MaxStaminaPoints,
                m_playerStats.m_CurrentStaminaPoints,m_playerStats.m_Pigments);

            GameManager.Instance.m_FallenEnemies = m_WorldData.m_FallenEnemies;
            GameManager.Instance.m_LootedItems = m_WorldData.m_LootedItems;
            GameManager.Instance.m_OpenedChests = m_WorldData.m_OpenedChests;
            GameManager.Instance.m_OpenedDoors = m_WorldData.m_OpenedDoors;
            GameManager.Instance.m_ActivatedCheckPoints = m_WorldData.m_ActivatedCheckPoints;
        }
        
        Debug.Log("Start");
    }

    public void SavePlayerDataGlobalFromFile(int _saveSlot)
    {
        PlayerData temp = DataManager.Instance.LoadPlayer(_saveSlot);

        m_PlayerData.m_SceneIndex = temp.m_SceneIndex;
        
        m_PlayerData.m_MaxHealth = temp.m_MaxHealth;
        m_PlayerData.m_CurrentHealth = temp.m_CurrentHealth;

        m_PlayerData.m_MaxStamina = temp.m_MaxStamina;
        m_PlayerData.m_CurrentStamina = temp.m_CurrentStamina;

        m_PlayerData.m_Level = temp.m_Level;
        m_PlayerData.m_Pigments = temp.m_Pigments;

        m_PlayerData.m_Vitality = temp.m_Vitality;
        m_PlayerData.m_Constitution = temp.m_Constitution;
        m_PlayerData.m_Strength = temp.m_Strength;
        m_PlayerData.m_Dexterity = temp.m_Dexterity;
        
        m_PlayerData.m_PhysicalDefense = temp.m_PhysicalDefense;
        m_PlayerData.m_BleedingResistance = temp.m_BleedingResistance;
        m_PlayerData.m_PoisonResistance = temp.m_PoisonResistance;

        m_PlayerData.m_BaseDamage = temp.m_BaseDamage;

        m_PlayerData.m_Position = new float[] {temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]};

        m_DataFromFile = true;
    }
    
    public void SaveWorldDataGlobalFromFile(int _saveSlot)
    {
        WorldData temp = DataManager.Instance.LoadWorld(_saveSlot);

        m_WorldData.m_FallenEnemies = temp.m_FallenEnemies;
        m_WorldData.m_LootedItems = temp.m_LootedItems;
        m_WorldData.m_OpenedChests = temp.m_OpenedChests;
        m_WorldData.m_ActivatedCheckPoints = temp.m_ActivatedCheckPoints;
        m_WorldData.m_OpenedDoors = temp.m_OpenedDoors;
        
        m_DataFromFile = true;
    }

    public void SaveInventoryDataGlobalFromFile(int _saveSlot)
    {
        InventoryData temp = DataManager.Instance.LoadInventory(_saveSlot);
        
        m_InventoryData.m_InventoryItems = temp.m_InventoryItems;
        m_InventoryData.m_EquipmentItems = temp.m_EquipmentItems;
        
        m_DataFromFile = true;
    }

    public void SavePlayerDataGlobalFromGame(PlayerStats _playerStats)
    {
        m_PlayerData.m_SceneIndex = SceneManager.GetActiveScene().buildIndex;

        m_PlayerData.m_MaxHealth = _playerStats.m_MaxHealthPoints;
        m_PlayerData.m_CurrentHealth = _playerStats.m_CurrentHealthPoints;
        m_PlayerData.m_MaxStamina = _playerStats.m_MaxStaminaPoints;
        m_PlayerData.m_CurrentStamina = _playerStats.m_CurrentStaminaPoints;
        m_PlayerData.m_Level = _playerStats.m_Level;
        m_PlayerData.m_Pigments = _playerStats.m_Pigments;
        m_PlayerData.m_Vitality = _playerStats.m_Vitality;
        m_PlayerData.m_Constitution = _playerStats.m_Constitution;
        m_PlayerData.m_Strength = _playerStats.m_Strength;
        m_PlayerData.m_Dexterity = _playerStats.m_Dexterity;
        m_PlayerData.m_PhysicalDefense = _playerStats.m_PhysicalDefense;
        m_PlayerData.m_BleedingResistance = _playerStats.m_BleedingResistance;
        m_PlayerData.m_PoisonResistance = _playerStats.m_PoisonResistance;
        m_PlayerData.m_BaseDamage = _playerStats.m_BaseDamage;
        
        m_PlayerData.m_Position = new float[] {0,0,0};
        
        m_DataFromFile = true;
    }

    public void SaveInventoryDataGlobalFromGame()
    {
        for (int i = 0; i < InventoryManager.Instance.Inventory.Length; i++)
        {
            m_InventoryData.m_InventoryItems[i] = InventoryManager.Instance.Inventory[i].GetID();
        }
        for (int i = 0; i < InventoryManager.Instance.Equipment.Length; i++)
        {
            m_InventoryData.m_EquipmentItems[i] = InventoryManager.Instance.Equipment[i].GetID();
        }
        
        m_DataFromFile = true;
    }

    private void TriggerStart(Scene _newScene, LoadSceneMode _mode)
    {
        Start();
    }
}
