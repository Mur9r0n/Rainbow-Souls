using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SheepController : MonoBehaviour
{
    public NavMeshAgent m_Agent;
    public Vector3 OriginalPosition { get; set; }
    public Quaternion OriginalRotation { get; set; }
    public float OriginalFOVAngle { get; set; }
    public float OriginalFOVDistance { get; set; }

    public Healthbar m_Healthbar;
    
    [SerializeField, Tooltip("Maximum Healthpoints.")]
    private float m_maxHealthPoints;

    [SerializeField, Tooltip("Current Healthpoints.")]
    private float m_currentHealthPoints;

    [SerializeField, Tooltip("Time to pass until resetting."), Range(1f, 20f)]
    public float m_ResetDelay = 1f;

    [Header("FOV and Range Parameters:")]
    [SerializeField, Tooltip("Field of View Distance."), Range(1f, 100f)]
    public float m_FOVDistance = 1f;

    [SerializeField, Tooltip("Field of View Angle."), Range(0f, 90f)]
    public float m_FOVAngle = 1f; 
    
    [SerializeField, Tooltip("Distance at which the GameObject is able to Attack."),Range(1f, 100f)]
    public float m_AttackDistance = 1f;
    
    [SerializeField, Tooltip("Angle at which the GameObject is able to Attack."),Range(0f, 90f)]
    public float m_AttackAngle = 1f;
    
    private ABaseState m_activeState;
    private SheepIdleState m_idleState;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
        OriginalFOVAngle = m_FOVAngle;
        OriginalFOVDistance = m_FOVDistance;
        m_Healthbar = GetComponentInChildren<Healthbar>();

        m_idleState = new SheepIdleState();
        SheepAttackState m_attackState = new SheepAttackState();
        SheepResetState m_resetState = new SheepResetState();
        SheepWalkState m_walkState = new SheepWalkState();
        SheepSearchState m_searchState = new SheepSearchState();

        m_idleState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(), m_walkState
            ));

        m_attackState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(), m_searchState
            ));

        m_resetState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
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

        m_walkState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(), m_searchState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(), m_attackState
            ));
        m_searchState.SheepInit(this,new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_searchState.m_Playerfound, m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !m_searchState.m_Playerfound && m_searchState.m_Timer<=0, m_resetState
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
                        Debug.Log("Hit something else! ");
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
        Vector3 directionToPlayer = (playerposition + new Vector3(0, 1, 0)) -
                                    origin;
        // Debug.Log(Vector3.SignedAngle(dir, transform.forward, Vector3.forward));

        if (Vector3.SignedAngle(directionToPlayer, transform.forward, Vector3.forward) <= m_AttackAngle &&
            Vector3.SignedAngle(directionToPlayer, transform.forward, Vector3.forward) >= -m_AttackAngle)
        {
            // Debug.Log("Player in FOV!");
            RaycastHit hit;
            if (Vector3.Distance(origin, playerposition) <= m_AttackDistance)
            {
                if (Physics.Raycast(origin, directionToPlayer, out hit, m_AttackDistance))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        // Debug.Log("Able to Attack!");
                        Debug.DrawRay(origin, directionToPlayer, Color.blue,
                            5f);
                        return true;
                    }
                    else
                    {
                        Debug.Log("Cant attack player! ");
                        Debug.DrawRay(origin, directionToPlayer, Color.white, 5f);
                        return false;
                    }
                }
            }
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + new Vector3(0, 1, 0);

        Vector3 FovLine1 = Quaternion.AngleAxis(m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;
        Vector3 FovLine2 = Quaternion.AngleAxis(-m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;        
        Vector3 FovLine3 = Quaternion.AngleAxis(m_FOVAngle, transform.right) * transform.forward * m_FOVDistance;
        Vector3 FovLine4 = Quaternion.AngleAxis(-m_FOVAngle, transform.right) * transform.forward * m_FOVDistance;


        Vector3 AttackLine1 = Quaternion.AngleAxis(m_AttackAngle, transform.up) * transform.forward * m_AttackDistance;
        Vector3 AttackLine2 = Quaternion.AngleAxis(-m_AttackAngle, transform.up) * transform.forward * m_AttackDistance;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(origin, m_FOVDistance);
        Gizmos.DrawWireSphere(origin, m_AttackDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(origin, FovLine1);
        Gizmos.DrawRay(origin, FovLine2);
        Gizmos.DrawRay(origin, FovLine3);        
        Gizmos.DrawRay(origin, FovLine4);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, AttackLine1);
        Gizmos.DrawRay(origin, AttackLine2);

    }
}