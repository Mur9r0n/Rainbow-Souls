using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MushroomController : AEnemyController
{
    [Header("Poison Cloud Attack Parameters:")]
    [SerializeField, Tooltip("Distance at which the GameObject is able to Attack."), Range(1f, 100f)]
    public float m_PoisonDistance = 1f;

    [SerializeField, Tooltip("Time between PoisonCloud Spawn.")]
    public float m_PoisonCloudDelay = 10f;

    [SerializeField] private float m_attackDamage = 50f;
    [SerializeField] public GameObject m_PoisonCloudPrefab;

    private void Start()
    {
        base.Start();
        
        MushroomAttackState m_attackState = new MushroomAttackState();
        EnemyResetState m_resetState = new EnemyResetState();
        EnemyWalkState m_walkState = new EnemyWalkState();
        EnemySearchState m_searchState = new EnemySearchState();

        m_idleState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_PoisonDistance), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_walkState
            ));

        m_attackState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_searchState
            ));

        m_resetState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_resetState.IsFinished, m_idleState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_PoisonDistance), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_walkState
            ));

        m_walkState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_searchState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_PoisonDistance), m_attackState
            ));
        m_searchState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_searchState.m_Playerfound, m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !m_searchState.m_Playerfound && m_searchState.m_Timer <= 0, m_resetState
            ));

        m_activeState = m_idleState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerCombat>())
                other.gameObject.GetComponent<PlayerCombat>().TakeDamage(m_attackDamage);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,1,0), m_PoisonDistance);

    }
}