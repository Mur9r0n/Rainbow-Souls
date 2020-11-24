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
    
    void Start()
    {
        if (m_DataFromFile)
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

        m_PlayerData.m_Position = new float[] {temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]};

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
        
        m_PlayerData.m_Position = new float[] {0,0,0};
        
        m_DataFromFile = true;
    }
    
    private void TriggerStart(Scene _newScene, LoadSceneMode _mode)
    {
        Start();
    }
}
