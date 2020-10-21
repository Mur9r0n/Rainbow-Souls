using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MushroomController : MonoBehaviour
{
    public NavMeshAgent m_Agent = null;
    private Animator m_anim = null;
    public Vector3 OriginalPosition { get; set; }
    public Quaternion OriginalRotation { get; set; }
    public float OriginalFOVAngle { get; set; }
    public float OriginalFOVDistance { get; set; }

    public Healthbar m_Healthbar;

    private PlayerController m_playerController;
    
    [SerializeField, Tooltip("Maximum Healthpoints.")]
    private float m_maxHealthPoints;

    [SerializeField, Tooltip("Current Healthpoints.")]
    private float m_currentHealthPoints;

    [SerializeField, Tooltip("Time to pass until resetting."), Range(1f, 20f)]
    public float m_ResetDelay = 1f;

    [Header("FOV and Range Parameters:")] [SerializeField, Tooltip("Field of View Distance."), Range(1f, 100f)]
    public float m_FOVDistance = 1f;

    [SerializeField, Tooltip("Field of View Angle."), Range(0f, 90f)]
    public float m_FOVAngle = 1f;

    [SerializeField, Tooltip("Distance at which the GameObject is able to Attack."), Range(1f, 100f)]
    public float m_AttackDistance = 2f;

    [SerializeField, Tooltip("Distance at which the GameObject is able to Attack."), Range(1f, 100f)]
    public float m_PoisonDistance = 1f;

    [SerializeField] public GameObject m_PoisonCloudPrefab;

    private ABaseState m_activeState;
    private MushroomIdleState m_idleState;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
        OriginalFOVAngle = m_FOVAngle;
        OriginalFOVDistance = m_FOVDistance;
        m_Healthbar = GetComponentInChildren<Healthbar>();
        m_playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        GameManager.Instance.m_Enemies.Add(this.gameObject);
        m_Healthbar.GetMaxHealth(m_maxHealthPoints);

        m_idleState = new MushroomIdleState();
        MushroomAttackState m_attackState = new MushroomAttackState();
        MushroomResetState m_resetState = new MushroomResetState();
        MushroomWalkState m_walkState = new MushroomWalkState();
        MushroomSearchState m_searchState = new MushroomSearchState();

        m_idleState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(), m_walkState
            ));

        m_attackState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(), m_searchState
            ));

        m_resetState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_resetState.IsFinished, m_idleState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(), m_walkState
            ));

        m_walkState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(), m_searchState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(), m_attackState
            ));
        m_searchState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_searchState.m_Playerfound, m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !m_searchState.m_Playerfound && m_searchState.m_Timer <= 0, m_resetState
            ));

        m_activeState = m_idleState;
    }

    private void Update()
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
        // Debug.Log(Vector3.SignedAngle(dir, transform.forward, Vector3.forward));

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

    private bool PlayerInRangeToAttack()
    {
        Vector3 playerposition = GameManager.Instance.PlayerTransform.position;
        Vector3 origin = transform.position + new Vector3(0, 1, 0);
        Vector3 directionToPlayer = (playerposition + new Vector3(0, 1, 0)) - origin;

        RaycastHit hit;
        if (Vector3.Distance(origin, playerposition) <= m_PoisonDistance)
        {
            if (Physics.Raycast(origin, directionToPlayer, out hit, m_PoisonDistance))
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>())
                other.gameObject.GetComponent<PlayerController>().TakeDamage(50);
        }
    }

    public void TakeDamage(int _damageAmount)
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
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + new Vector3(0, 1, 0);

        Vector3 FovLine1 = Quaternion.AngleAxis(m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;
        Vector3 FovLine2 = Quaternion.AngleAxis(-m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;
        Vector3 FovLine3 = Quaternion.AngleAxis(m_FOVAngle, transform.right) * transform.forward * m_FOVDistance;
        Vector3 FovLine4 = Quaternion.AngleAxis(-m_FOVAngle, transform.right) * transform.forward * m_FOVDistance;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(origin, m_FOVDistance);
        Gizmos.DrawWireSphere(origin, m_AttackDistance);
        Gizmos.DrawWireSphere(origin, m_PoisonDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(origin, FovLine1);
        Gizmos.DrawRay(origin, FovLine2);
        Gizmos.DrawRay(origin, FovLine3);
        Gizmos.DrawRay(origin, FovLine4);
    }
}