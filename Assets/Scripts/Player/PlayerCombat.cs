using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputs m_playerInputs = null;
    private CharacterController m_controller = null;
    private Transform m_mainCameraTransform = null;
    private Animator m_anim = null;

    private int m_lightAttack = Animator.StringToHash("Light_Attack");
    private int m_heavyAttack = Animator.StringToHash("Heavy_Attack");

    public PlayerStats m_playerStats = null;
    
    void Awake()
    {
        m_playerInputs = new PlayerInputs();
        m_controller = GetComponent<CharacterController>();
        m_playerStats = GetComponent<PlayerStats>();
        m_mainCameraTransform = Camera.main.transform;
        m_anim = GetComponent<Animator>();
        
        m_playerInputs.Player.Dodge.performed += _ => Dodge();
        m_playerInputs.Player.LightAttack.performed += _ => LightAttack();
        m_playerInputs.Player.HeavyAttack.performed += _ => HeavyAttack();
        m_playerInputs.Player.TargetSystem.performed += _ => TargetEnemy();
    }

    private void Update()
    {
        //TODO : Player muss stierwen wann en rooffällt
        // if (transform.position.y <= 27f)
        // {
        //     Die();
        // }
    }

    public void Dodge()
    {
        if (m_playerStats.m_CurrentStaminaPoints >= 20f)
        {
            if (m_playerInputs.Player.Movement.ReadValue<Vector2>() != Vector2.zero)
            {
                Vector2 movementInput = new Vector2(m_playerInputs.Player.Movement.ReadValue<Vector2>().x, m_playerInputs.Player.Movement.ReadValue<Vector2>().y);
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
            m_playerStats.m_ActualDamage = m_playerStats.m_BaseDamage;
            Debug.Log("Light Attack");
        }
    }
    
    public void HeavyAttack()
    {
        if (m_playerStats.m_CurrentStaminaPoints >= 50f)
        {
            m_playerStats.m_CurrentStaminaPoints -= 50f;
            m_anim.SetTrigger(m_heavyAttack);
            m_playerStats.m_ActualDamage = m_playerStats.m_BaseDamage * 2.0f;
            Debug.Log("Heavy Attack");
        }
    }
    
    public void TargetEnemy()
    {
        if (!m_playerStats.m_targetEnemy)
        {
            foreach (GameObject possibleTarget in GameManager.Instance.m_Enemies)
            {
                if (m_playerStats.m_targetEnemy != null)
                {
                    if (Vector3.Distance(possibleTarget.transform.position, transform.position) <
                        Vector3.Distance(m_playerStats.m_targetEnemy.transform.position, transform.position))
                    {
                        m_playerStats.m_targetEnemy = possibleTarget;
                    }
                }
                else
                {
                    m_playerStats.m_targetEnemy = possibleTarget;
                }
            }

            // m_cinemachineFreeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
        }
        else if (m_playerStats.m_targetEnemy)
        {
            m_playerStats.m_targetEnemy = null;
            // m_cinemachineFreeLook.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
        }

        Debug.Log("Target : " + m_playerStats.m_targetEnemy);
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
                    other.gameObject.GetComponent<MushroomController>().TakeDamage((int) m_playerStats.m_ActualDamage);
                }

                break;
            case "Bee":
                if (other.gameObject.GetComponent<BeeController>())
                {
                    other.gameObject.GetComponent<BeeController>().TakeDamage((int) m_playerStats.m_ActualDamage);
                }

                break;
            case "Sheep":
                if (other.gameObject.GetComponent<SheepController>())
                {
                    other.gameObject.GetComponent<SheepController>().TakeDamage((int) m_playerStats.m_ActualDamage);
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
            Die();
        }
    }

    //TODO
    public void Die()
    {
        //Die and respawn at checkpoint
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
