using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Animator))]
public abstract class AEnemyController : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent m_Agent = null;
    [HideInInspector] public Healthbar m_Healthbar = null;
    [HideInInspector] public Animator m_anim = null;
    [HideInInspector] public PlayerController m_playerController = null;

    public Vector3 OriginalPosition { get; set; }
    public Quaternion OriginalRotation { get; set; }
    public float OriginalFOVAngle { get; set; }
    public float OriginalFOVDistance { get; set; }

    [SerializeField, Tooltip("Maximum Healthpoints.")]
    public float m_maxHealthPoints;

    [SerializeField, Tooltip("Current Healthpoints.")]
    public float m_currentHealthPoints;
    
    [SerializeField, Tooltip("Time to pass until resetting."), Range(1f, 20f)]
    public float m_ResetDelay = 1f;
    
    [Header("FOV and Range Parameters:")]
    [SerializeField, Tooltip("Field of View Distance."), Range(1f, 100f)]
    public float m_FOVDistance = 1f;

    [SerializeField, Tooltip("Field of View Angle."), Range(0f, 90f)]
    public float m_FOVAngle = 1f; 
    
    [SerializeField, Tooltip("Distance at which the GameObject is able to Attack."),Range(1f, 100f)]
    public float m_AttackDistance = 1f;
    
    public ABaseState m_activeState;
    public EnemyIdleState m_idleState;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
        m_Healthbar = GetComponentInChildren<Healthbar>();
        m_playerController = FindObjectOfType<PlayerController>();
        
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
        OriginalFOVAngle = m_FOVAngle;
        OriginalFOVDistance = m_FOVDistance;
    }

    public virtual void Start()
    {
        GameManager.Instance.m_Enemies.Add(this.gameObject);
        m_Healthbar.GetMaxHealth(m_maxHealthPoints);
        
        m_idleState = new EnemyIdleState();
    }

    
    public virtual void Update()
    {
        if (m_activeState is object)
        {
            m_activeState.Update();

            if (m_activeState.IsFinished)
            {
                m_activeState.Exit();
                m_activeState = m_idleState;
                m_activeState.Enter();
            }

            foreach (var keyValuePair in m_activeState.Transitions)
            {
                if (keyValuePair.Key())
                {
                    m_activeState.Exit();
                    m_activeState = keyValuePair.Value;
                    if (!m_activeState.Enter())
                        // Debug.Log($"Konnte {m_activeState} nicht betretet!");
                        break;
                }
            }
        }
    }
    public bool PlayerInFOV()
    {
        Vector3 playerposition = GameManager.Instance.PlayerTransform.position;
        Vector3 origin = transform.position + new Vector3(0, 1, 0);
        Vector3 directionToPlayer = (playerposition + new Vector3(0, 1, 0)) -
                                    origin;

        if (Vector3.SignedAngle(directionToPlayer, transform.forward, Vector3.forward) <= m_FOVAngle &&
            Vector3.SignedAngle(directionToPlayer, transform.forward, Vector3.forward) >= -m_FOVAngle)
        {
            // Debug.Log("Player in FOV!");
            RaycastHit hit;
            if (Vector3.Distance(origin, playerposition) <= m_FOVDistance)
            {
                if (Physics.Raycast(origin, directionToPlayer, out hit, m_FOVDistance))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        // Debug.Log("Player hit!");
                        Debug.DrawRay(origin, directionToPlayer, Color.green,
                            5f);
                        return true;
                    }
                    else
                    {
                        // Debug.Log("Hit something else! ");
                        Debug.DrawRay(origin, directionToPlayer, Color.red, 5f);
                        return false;
                    }
                }
            }
        }

        return false;
    }

    public bool PlayerInRangeToAttack(float _distance)
    {
        Vector3 playerposition = GameManager.Instance.PlayerTransform.position;
        Vector3 origin = transform.position + new Vector3(0, 1, 0);
        Vector3 directionToPlayer = (playerposition + new Vector3(0, 1, 0)) - origin;

        RaycastHit hit;
        if (Vector3.Distance(origin, playerposition) <= _distance)
        {
            if (Physics.Raycast(origin, directionToPlayer, out hit, _distance))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.DrawRay(origin, directionToPlayer, Color.blue, 5f);
                    return true;
                }
                else
                {
                    // Debug.Log("Cant attack player! ");
                    Debug.DrawRay(origin, directionToPlayer, Color.white, 5f);
                    return false;
                }
            }
        }
        return false;
    }

    public void TakeDamage(float _damageAmount)
    {
        m_currentHealthPoints -= _damageAmount;
        m_Healthbar.GetCurrentHealth(m_currentHealthPoints);
        
        if (m_currentHealthPoints <= 0)
        {
            m_currentHealthPoints = 0;
            if (m_playerController.m_targetedEnemy == this.gameObject)
            {
                m_playerController.m_targetedEnemy = null;
            }
            GameManager.Instance.m_Enemies.Remove(this.gameObject);
            gameObject.SetActive(false);
        }
    }
    
    public virtual void OnDrawGizmos()
    {
        Vector3 origin = transform.position + new Vector3(0, 1, 0);

        Vector3 FovLine1 = Quaternion.AngleAxis(m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;
        Vector3 FovLine2 = Quaternion.AngleAxis(-m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;
        Vector3 FovLine3 = Quaternion.AngleAxis(m_FOVAngle, transform.right) * transform.forward * m_FOVDistance;
        Vector3 FovLine4 = Quaternion.AngleAxis(-m_FOVAngle, transform.right) * transform.forward * m_FOVDistance;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(origin, m_FOVDistance);
        Gizmos.DrawWireSphere(origin, m_AttackDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(origin, FovLine1);
        Gizmos.DrawRay(origin, FovLine2);
        Gizmos.DrawRay(origin, FovLine3);
        Gizmos.DrawRay(origin, FovLine4);
    }
}
