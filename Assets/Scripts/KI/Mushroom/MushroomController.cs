using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MushroomController : AEnemyController
{
    [SerializeField, Tooltip("Distance at which the GameObject is able to Attack."), Range(1f, 100f)]
    public float m_PoisonDistance = 1f;

    [SerializeField] private float m_attackDamage = 50f;
    [SerializeField] public GameObject m_PoisonCloudPrefab;

    private void Start()
    {
        base.Start();
        
        MushroomAttackState m_attackState = new MushroomAttackState();
        MushroomResetState m_resetState = new MushroomResetState();
        MushroomWalkState m_walkState = new MushroomWalkState();
        MushroomSearchState m_searchState = new MushroomSearchState();

        m_idleState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_PoisonDistance), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_walkState
            ));

        m_attackState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_searchState
            ));

        m_resetState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
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

        m_walkState.MushroomInit(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(m_PoisonDistance), m_searchState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_PoisonDistance), m_attackState
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>())
                other.gameObject.GetComponent<PlayerController>().TakeDamage(m_attackDamage);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,1,0), m_PoisonDistance);

    }
}