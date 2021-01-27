using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerMovement m_playerMovement = null;
    private PlayerCombat m_playerCombat = null;
    private PlayerInputs m_playerInputs = null;
    private PlayerStats m_playerStats = null;
    private CharacterController m_controller = null;
    private InteractManager m_interactManager;
    private AInteractables m_aInteractable;
    private UIManager m_uiManager;

    void Awake()
    {
        m_playerInputs = new PlayerInputs();
        m_playerStats = GetComponent<PlayerStats>();
        m_playerMovement = GetComponent<PlayerMovement>();
        m_playerCombat = GetComponent<PlayerCombat>();
        m_controller = GetComponent<CharacterController>();

        m_playerInputs.Player.Use.performed += _ => Use();
        m_playerInputs.Player.Interaction.performed += _ => Interact();
        m_playerInputs.Player.SwitchItems.performed += _ => SwitchItems(m_playerInputs.Player.SwitchItems.ReadValue<float>());
        m_playerInputs.Player.OpenInventory.performed += _ => OpenInventory();
        m_playerInputs.Player.OpenMenu.performed += _ => OpenMenu();
    }

    void Start()
    {
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
        if (m_interactManager.m_interactables != null)
        {
            if (m_interactManager.m_interactables.Count > 0)
            {
                m_aInteractable = m_interactManager.LookForClosestInteraction();
                m_aInteractable.Interact();
                UIManager.Instance.HideInteractionTooltip();
            }
            else if (m_interactManager.m_interactables.Count == 0)
            {
                Debug.Log("Interactable List is Empty");
            }
        }
        else
        {
            Debug.Log("Interactable list is NULL");
        }
        
    }

    public void SwitchItems(float _context)
    {
        Debug.Log("Switch Items");
    }

    public void OpenInventory()
    {
        FreezeUnfreezePlayer();
        m_uiManager.ShowInventory();
        m_uiManager.ShowEquipment();
        m_uiManager.UpdateSlotsUI();
    }

    public void OpenMenu()
    {
        Debug.Log("Open Menu");
    }

    public void FreezeUnfreezePlayer()
    {
        if (m_playerMovement.enabled)
        {
            m_playerMovement.enabled = false;
            m_playerCombat.enabled = false;
            m_playerMovement.m_cinemachineFreeLook.enabled = false;
        }
        else
        {
            m_playerMovement.enabled = true;            
            m_playerCombat.enabled = true;
            m_playerMovement.m_cinemachineFreeLook.enabled = true;
        }
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
            DataManager.Instance.SaveWorld(1);
            DataManager.Instance.SaveInventory(1);
            Debug.Log("Saved 1");
        }
        
        if (GUI.Button(new Rect(10, 180, 150, 25), "Save 2"))
        {
            DataManager.Instance.SavePlayer(m_playerStats, 2);
            DataManager.Instance.SaveWorld(2);
            DataManager.Instance.SaveInventory(2);

            Debug.Log("Saved 2");
        }
        
        if (GUI.Button(new Rect(10, 210, 150, 25), "Save 3"))
        {
            DataManager.Instance.SavePlayer(m_playerStats, 3);
            DataManager.Instance.SaveWorld(3);
            DataManager.Instance.SaveInventory(3);
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
            
            m_playerStats.m_PhysicalDefense = temp.m_PhysicalDefense;
            m_playerStats.m_BleedingResistance = temp.m_BleedingResistance;
            m_playerStats.m_PoisonResistance = temp.m_PoisonResistance;

            m_playerStats.m_BaseDamage = temp.m_BaseDamage;

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

            m_playerStats.m_PhysicalDefense = temp.m_PhysicalDefense;
            m_playerStats.m_BleedingResistance = temp.m_BleedingResistance;
            m_playerStats.m_PoisonResistance = temp.m_PoisonResistance;

            m_playerStats.m_BaseDamage = temp.m_BaseDamage;

            Vector3 temppos = new Vector3(temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]);
            m_controller.enabled = false;
            transform.position = temppos;
            m_controller.enabled = true;

            UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,m_playerStats.m_CurrentHealthPoints,m_playerStats.m_MaxStaminaPoints,
                m_playerStats.m_CurrentStaminaPoints,m_playerStats.m_Pigments);
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
            
            m_playerStats.m_PhysicalDefense = temp.m_PhysicalDefense;
            m_playerStats.m_BleedingResistance = temp.m_BleedingResistance;
            m_playerStats.m_PoisonResistance = temp.m_PoisonResistance;

            m_playerStats.m_BaseDamage = temp.m_BaseDamage;

            Vector3 temppos = new Vector3(temp.m_Position[0], temp.m_Position[1], temp.m_Position[2]);
            m_controller.enabled = false;
            transform.position = temppos;
            m_controller.enabled = true;

            UIManager.Instance.RefreshUI(m_playerStats.m_MaxHealthPoints,m_playerStats.m_CurrentHealthPoints,m_playerStats.m_MaxStaminaPoints,
                m_playerStats.m_CurrentStaminaPoints,m_playerStats.m_Pigments);
            
            Debug.Log(temppos);
        }

        if (GUI.Button(new Rect(10, 420, 150, 50), "Main Menu"))
        {
            SceneManager.LoadScene(0);
        }
        
        if (GUI.Button(new Rect(10, 480, 150, 50), "Exit"))
        {
            Application.Quit();
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
