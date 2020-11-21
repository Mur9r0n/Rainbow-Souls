using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : ABaseState
{
    private float m_angle;
    private float m_distance;
    public override bool Enter()
    {
        m_angle = m_controller.m_FOVAngle;
        m_distance = m_controller.m_FOVDistance;

        m_controller.m_Agent.isStopped = false;
        m_controller.m_FOVAngle *= 1.5f;
        m_controller.m_FOVDistance *= 1.25f;
        return base.Enter();
    }

    public override void Update()
    {
        m_controller.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
    }

    public override void Exit()
    {
        m_controller.m_FOVAngle = m_angle;
        m_controller.m_FOVDistance = m_distance;
        base.Exit();
    }
}
