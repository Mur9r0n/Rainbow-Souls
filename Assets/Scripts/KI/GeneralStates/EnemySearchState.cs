using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchState : ABaseState
{
    public float m_Timer;
    public bool m_Playerfound;
    
    public override bool Enter()
    {
        m_Timer = m_controller.m_ResetDelay;
        m_controller.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
        return base.Enter();
    }
    
    public override void Update()
    {
        m_Timer -= Time.deltaTime;

        if (m_Timer >= 0f)
        {
            SearchForPlayer();
        }
        else
        {
            m_Playerfound = false;
        }
    }

    public override void Exit()
    {
        m_Playerfound = false;
        base.Exit();
    }

    public void SearchForPlayer()
    {
        if (m_controller.PlayerInFOV())
        {
            m_Playerfound = true;
        }
    }
}
