using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    #region Private Variables

    [SerializeField, Tooltip("Inventory of the Player")]
    private CharacterController m_controller = null;

    private Animator m_anim = null;
    private PlayerInputs m_inputs = null;
    private PlayerStats m_playerStats = null;
    private Transform m_mainCameraTransform = null;
    private UIObjects m_uiObjects;
    private InteractableObjects m_interactableObject = null;

    private float m_currentSpeed = 0f;
    private float m_speedSmoothVelocity = 0f;
    private float m_speedSmoothTime = 0.1f;
    private float m_rotationSpeed = 0.1f;
    public GameObject m_targetedEnemy = null;

    #endregion

    [SerializeField, Tooltip("FreeLookCamera to Follow the Player.")]
    private CinemachineFreeLook m_cinemachineFreeLook = null;

    [SerializeField, Tooltip("Enable Gravity?")]
    private bool m_useGravity = true;

    #region Stat Variables

    [SerializeField, Tooltip("Speed in which the Character moves.")]
    private Vector2 m_movementSpeed = new Vector2(5.0f, 10.0f);

    [Tooltip("Amount of Damage dealt to Enemies.")]
    public float m_DamageBase = 10f;

    private float m_attackDamage = 0f;

    #endregion

    #region Animator Variables

    private int m_lightAttack = Animator.StringToHash("Light_Attack");
    private int m_heavyAttack = Animator.StringToHash("Heavy_Attack");
    private int m_walking = Animator.StringToHash("Walking");

    #endregion

    //TEST
    bool isSprinting = false;

    private void Awake()
    {
        m_inputs = new PlayerInputs();
        m_playerStats = GetComponent<PlayerStats>();
        m_controller = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();
        m_uiObjects = FindObjectOfType<UIObjects>();
        m_mainCameraTransform = Camera.main.transform;

        #region Input Action

        m_inputs.Player.Dodge.performed += _ => Dodge();
        m_inputs.Player.LightAttack.performed += _ => LightAttack();
        m_inputs.Player.HeavyAttack.performed += _ => HeavyAttack();
        m_inputs.Player.TargetSystem.performed += _ => TargetSystem();
        m_inputs.Player.Sprint.performed += _ => Sprint();
        m_inputs.Player.Use.performed += _ => Use();
        m_inputs.Player.Interaction.performed += _ => Interaction();
        m_inputs.Player.SwitchItems.performed += _ => SwitchItems(m_inputs.Player.SwitchItems.ReadValue<float>());
        m_inputs.Player.OpenInventory.performed += _ => OpenInventory();
        m_inputs.Player.OpenMenu.performed += _ => OpenMenu();

        #endregion
    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        InventoryManager.Instance.Inventory.Clear();
        UIManager.Instance.UpdatePigmentCounter(m_playerStats.m_Pigments);
    }

    private void Update()
    {
        Movement(m_inputs.Player.Movement.ReadValue<Vector2>());
        if (m_useGravity) Gravity();
        LookForInteractables();

        //While Target selected camera looks at target
        // if (m_targetedEnemy ? m_cinemachineFreeLook.LookAt = m_targetedEnemy.transform : m_cinemachineFreeLook.LookAt = transform) ;
    }

    public void Movement(Vector2 _context)
    {
        if (!m_targetedEnemy || isSprinting)
        {
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_WaitTime = 0.5f;
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_RecenteringTime = 1.0f;
            if (_context != Vector2.zero)
            {
                m_anim.SetBool(m_walking, true);
                Vector2 movementInput = new Vector2(_context.x, _context.y);

                Vector3 forward = m_mainCameraTransform.forward;
                Vector3 right = m_mainCameraTransform.right;
                forward.y = 0;
                right.y = 0;

                forward.Normalize();
                right.Normalize();

                Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

                if (desiredMoveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),
                        m_rotationSpeed);
                }

                float targetSpeed = m_movementSpeed.x * movementInput.magnitude;
                if (isSprinting)
                    m_currentSpeed = m_movementSpeed.y;
                else
                    m_currentSpeed = Mathf.SmoothDamp(m_currentSpeed, targetSpeed, ref m_speedSmoothVelocity,
                        m_speedSmoothTime);

                m_controller.Move(desiredMoveDirection * m_currentSpeed * Time.deltaTime);


                if (!m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled)
                {
                    m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true;
                }
            }
            else
            {
                m_anim.SetBool(m_walking, false);
                m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = false;
            }
        }
        else if (m_targetedEnemy)
        {
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_WaitTime = 0;
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_RecenteringTime = 0.1f;
            Vector3 desiredMoveDirection = (m_targetedEnemy.transform.position - transform.position).normalized;

            if (desiredMoveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),
                    1f);
            }

            if (_context != Vector2.zero)
            {
                m_anim.SetBool(m_walking, true);
                Vector2 movementInput = new Vector2(_context.x, _context.y);

                float targetSpeed = m_movementSpeed.x * movementInput.magnitude;

                if (isSprinting)
                {
                    m_currentSpeed = m_movementSpeed.y;
                }

                else
                {
                    m_currentSpeed = Mathf.SmoothDamp(m_currentSpeed, targetSpeed, ref m_speedSmoothVelocity,
                        m_speedSmoothTime);
                }

                Vector3 movement = transform.forward * movementInput.y + transform.right * movementInput.x;
                m_controller.Move(movement * Time.deltaTime * m_currentSpeed);

                if (!m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled)
                {
                    m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true;
                }
            }
            else
            {
                m_anim.SetBool(m_walking, false);
                m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = false;
            }
        }
    }

    private void Gravity()
    {
        Vector3 gravityVector = Vector3.zero;

        if (!m_controller.isGrounded)
        {
            gravityVector.y += Physics.gravity.y;
        }

        m_controller.Move(gravityVector * Time.deltaTime);
    }

    public void Dodge()
    {
        if (m_playerStats.m_CurrentStaminaPoints >= 20f)
        {
            if (m_inputs.Player.Movement.ReadValue<Vector2>() != Vector2.zero)
            {
                Vector2 movementInput = new Vector2(m_inputs.Player.Movement.ReadValue<Vector2>().x, m_inputs.Player.Movement.ReadValue<Vector2>().y);
                Vector3 desiredMoveDirection =
                    (m_mainCameraTransform.forward * movementInput.y + m_mainCameraTransform.right * movementInput.x).normalized;

                m_controller.Move(desiredMoveDirection * 4f);
            }
            else
            {
                m_controller.Move(transform.forward * -4f);
            }

            m_playerStats.m_CurrentStaminaPoints -= 20f;
        }

        Debug.Log("Dodge");
    }

    //TODO Can consume Stamina while Attacking -> Loss of Stamina with additional Attack
    public void LightAttack()
    {
        if (m_playerStats.m_CurrentStaminaPoints >= 30f)
        {
            m_playerStats.m_CurrentStaminaPoints -= 30f;
            m_anim.SetTrigger(m_lightAttack);
            m_attackDamage = m_DamageBase;
            Debug.Log("Light Attack");
        }
    }

    public void HeavyAttack()
    {
        m_anim.SetTrigger(m_heavyAttack);
        m_attackDamage = m_DamageBase * 1.5f;
        Debug.Log("Heavy Attack");
    }

    public void TargetSystem()
    {
        if (!m_targetedEnemy)
        {
            foreach (GameObject possibleTarget in GameManager.Instance.m_Enemies)
            {
                if (m_targetedEnemy != null)
                {
                    if (Vector3.Distance(possibleTarget.transform.position, transform.position) <
                        Vector3.Distance(m_targetedEnemy.transform.position, transform.position))
                    {
                        m_targetedEnemy = possibleTarget;
                    }
                }
                else
                {
                    m_targetedEnemy = possibleTarget;
                }
            }

            // m_cinemachineFreeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
        }
        else if (m_targetedEnemy)
        {
            m_targetedEnemy = null;
            // m_cinemachineFreeLook.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        }

        Debug.Log("Target : " + m_targetedEnemy);
    }

    public void Sprint()
    {
        Debug.Log("Sprint");

        isSprinting = !isSprinting;
    }

    public void Use()
    {
        Debug.Log("Use");
    }

    public void Interaction()
    {
        if (m_interactableObject != null)
        {
            m_interactableObject.Use();
            m_interactableObject = null;
        }
    }

    public void SwitchItems(float _context)
    {
        Debug.Log("Switch Items");
    }

    public void OpenInventory()
    {
        m_uiObjects.Show(m_uiObjects.m_InventoryPanel, !m_uiObjects.m_InventoryPanel.activeSelf);
        Debug.Log("Open Inventory");
    }

    public void OpenMenu()
    {
        Debug.Log("Open Menu");
    }

    #region OnEnable & OnDisable

    private void OnEnable()
    {
        m_inputs.Enable();
    }

    private void OnDisable()
    {
        m_inputs.Disable();
    }

    #endregion

    private void LookForInteractables()
    {
        //TODO: UI for Chest, Door, NPC
        if (InteractManager.Instance.interactables.Count > 0)
        {
            foreach (InteractableObjects _object in InteractManager.Instance.interactables)
            {
                if (Vector3.Distance(transform.position, _object.transform.position) <= 2.5f)
                {
                    m_interactableObject = _object;
                }
            }
        }

        if (m_interactableObject != null)
        {
            if (Vector3.Distance(transform.position, m_interactableObject.gameObject.transform.position) > 2.5f)
            {
                m_interactableObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        string hit = other.gameObject.tag;

        switch (hit)
        {
            case "Mushroom":
                if (other.gameObject.GetComponent<MushroomController>())
                {
                    other.gameObject.GetComponent<MushroomController>().TakeDamage((int) m_attackDamage);
                }

                break;
            case "Bee":
                if (other.gameObject.GetComponent<BeeController>())
                {
                    other.gameObject.GetComponent<BeeController>().TakeDamage((int) m_attackDamage);
                }

                break;
            case "Sheep":
                if (other.gameObject.GetComponent<SheepController>())
                {
                    other.gameObject.GetComponent<SheepController>().TakeDamage((int) m_attackDamage);
                }

                break;
        }
    }

    public void TakeDamage(float _damageAmount)
    {
        m_playerStats.m_CurrentHealthPoints -= _damageAmount;
        UIManager.Instance.UpdateHealthBar(m_playerStats.m_MaxHealthPoints, m_playerStats.m_CurrentHealthPoints);
        Debug.Log(m_playerStats.m_CurrentHealthPoints);

        if (m_playerStats.m_CurrentHealthPoints <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Damage"))
        {
            m_playerStats.m_CurrentHealthPoints -= 10;
        }

        if (GUI.Button(new Rect(10, 150, 150, 100), "Save"))
        {
            DataManager.Instance.SavePlayer(m_playerStats);
            Debug.Log("Saved");
        }

        if (GUI.Button(new Rect(10, 300, 150, 100), "Load"))
        {
            PlayerData temp = DataManager.Instance.LoadPlayer();

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

            Debug.Log(temppos);
        }
    }
}