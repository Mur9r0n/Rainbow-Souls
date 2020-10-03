using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour
{
    public NavMeshAgent m_Agent;
    public Vector3? TargetPosition { get; set; }
    public Vector3 OriginalPosition { get; set; }
    public Quaternion OriginalRotation { get; set; }
    public float OriginalFOVAngle { get; set; }
    public float OriginalFOVDistance { get; set; }
    
    [SerializeField, Tooltip("Maximum Healthpoints.")]
    private float m_maxHealthPoints;

    [SerializeField, Tooltip("Current Healthpoints.")]
    private float m_currentHealthPoints;

    [SerializeField, Tooltip("Distance at with the GameObject is able to Attack.")]
    private float m_attackDistance;

    [SerializeField, Tooltip("Field of View Distance."), Range(1f, 100f)]
    public float m_FOVDistance = 1f;

    [SerializeField, Tooltip("Field of View Angle."), Range(0f, 90f)]
    public float m_FOVAngle = 1f; 
    
    private ABaseState m_activeState;
    private SheepIdleState m_idleState;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
        OriginalFOVAngle = m_FOVAngle;
        OriginalFOVDistance = m_FOVDistance;

        m_idleState = new SheepIdleState();
        SheepAttackState m_attackState = new SheepAttackState();
        SheepResetState m_resetState = new SheepResetState();
        SheepWalkState m_walkState = new SheepWalkState();

        m_idleState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRange(), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRange(), m_walkState
            ));

        m_attackState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRange(), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRange(), m_resetState
            ));

        m_resetState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_resetState.IsFinished, m_idleState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRange(), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRange(), m_walkState
            ));

        m_walkState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRange(), m_resetState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRange(), m_attackState
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

    private bool PlayerInFOV()
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

    private bool PlayerInRange()
    {
        Vector3 playerposition = GameManager.Instance.PlayerTransform.position;
        Vector3 origin = transform.position + new Vector3(0, 1, 0);

        if (Vector3.Distance(playerposition, origin) <= m_attackDistance)
        {
            return true;
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        Vector3 FovLine1 = Quaternion.AngleAxis(m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;
        Vector3 FovLine2 = Quaternion.AngleAxis(-m_FOVAngle, transform.up) * transform.forward * m_FOVDistance;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, m_FOVDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, FovLine1);
        Gizmos.DrawRay(transform.position, FovLine2);
    }
}