using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BeeController : AEnemyController
{
    [SerializeField] public GameObject m_StingPrefab;
    public Transform m_StingTransform;
    
    private void Start()
    {
        base.Start();
        
        BeeAttackState m_attackState = new BeeAttackState();
        EnemyResetState m_resetState = new EnemyResetState();
        EnemyWalkState m_walkState = new EnemyWalkState();
        EnemySearchState m_searchState = new EnemySearchState();

        m_idleState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_AttackDistance), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_AttackDistance), m_walkState
            ));

        m_attackState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_AttackDistance), m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(m_AttackDistance), m_searchState
            ));

        m_resetState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_resetState.IsFinished, m_idleState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_AttackDistance), m_attackState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && !PlayerInRangeToAttack(m_AttackDistance), m_walkState
            ));

        m_walkState.Init(this, new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !PlayerInFOV() && !PlayerInRangeToAttack(m_AttackDistance), m_searchState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => PlayerInFOV() && PlayerInRangeToAttack(m_AttackDistance), m_attackState
            ));
        m_searchState.Init(this,new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => m_searchState.m_Playerfound, m_walkState
            ),
            new KeyValuePair<ABaseState.TransitionDelegate, ABaseState>
            (
                () => !m_searchState.m_Playerfound && m_searchState.m_Timer<=0, m_resetState
            ));

        m_activeState = m_idleState;
    }
}
