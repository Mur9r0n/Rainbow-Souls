using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour
{
    #region Components and Vectors

    public NavMeshAgent m_Agent;
    public Vector3? TargetPosition { get; set; }
    public Vector3 OriginalPosition { get; set; }
    public Quaternion OriginalRotation { get; set; }

    #endregion

    [SerializeField, Tooltip("Maximum Healthpoints.")]
    private float m_maxHealthPoints;

    [SerializeField, Tooltip("Current Healthpoints.")]
    private float m_currentHealthPoints;

    [SerializeField, Tooltip("Distance at with the GameObject is able to Attack.")]
    private float m_attackDistance;

    [SerializeField, Tooltip("Field of View Distance."), Range(1f, 100f)]
    private float m_fovDistance = 1f;

    [SerializeField, Tooltip("Field of View Angle."), Range(0f, 90f)]
    private float m_fovAngle = 1f;

    private ABaseState m_activeState;
    private SheepIdleState m_idleState;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;

        m_idleState = new SheepIdleState();
        SheepAttackState m_attackState = new SheepAttackState();
        // SheepResetState m_resetState = new SheepResetState();
        SheepWalkState m_walkState = new SheepWalkState();

        m_idleState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => Test(), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV(), m_attackState
            ));

        m_attackState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
        (
            () => Test(), m_idleState
        ));
        //
        // m_resetState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
        // (
        //     
        // ));
        //
        m_walkState.SheepInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
        (
            () => Test(), m_idleState
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

        if (Vector3.SignedAngle(directionToPlayer, transform.forward, Vector3.forward) <= m_fovAngle &&
            Vector3.SignedAngle(directionToPlayer, transform.forward, Vector3.forward) >= -m_fovAngle)
        {
            // Debug.Log("Player in FOV!");
            RaycastHit hit;
            if (Vector3.Distance(origin, playerposition) <= m_fovDistance)
            {
                if (Physics.Raycast(origin, directionToPlayer, out hit, m_fovDistance))
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
                        Debug.Log("Hit something else! " );
                        Debug.DrawRay(origin, directionToPlayer, Color.red, 5f);
                        return false;
                    }
                }
            }
        }

        return false;
    }

    private bool Test()
    {
        return m_currentHealthPoints < 10000000f;
    }

    private void OnDrawGizmos()
    {
        Vector3 FovLine1 = Quaternion.AngleAxis(m_fovAngle, transform.up) * transform.forward * m_fovDistance;
        Vector3 FovLine2 = Quaternion.AngleAxis(-m_fovAngle, transform.up) * transform.forward * m_fovDistance;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, m_fovDistance);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, FovLine1);
        Gizmos.DrawRay(transform.position, FovLine2);
    }
}