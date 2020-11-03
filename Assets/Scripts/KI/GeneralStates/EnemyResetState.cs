using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResetState : ABaseState
{
    private Quaternion? m_tempRot = null;
    private float m_timer = 0;
    public override bool Enter()
    {
        m_controller.m_Agent.isStopped = false;
        m_controller.m_Agent.SetDestination(m_controller.OriginalPosition);
        return base.Enter();
    }
    
    public override void Update()
    {
        if (Vector3.Distance(m_controller.transform.position, m_controller.OriginalPosition) <= .2f)
        {
            m_timer += Time.deltaTime;
            if (m_timer > 1)
            {
                m_controller.transform.rotation = m_controller.OriginalRotation;
                IsFinished = true;
            }
            if (m_tempRot == null)
            {
                m_tempRot = m_controller.transform.rotation;
            }
            m_controller.m_Agent.enabled = false;
            m_controller.transform.rotation = Quaternion.Lerp((Quaternion)m_tempRot,m_controller.OriginalRotation,m_timer);
            m_controller.m_Agent.enabled = true;
        }
    }
    
    public override void Exit()
    {
        m_timer = 0;
        m_controller.m_FOVAngle = m_controller.OriginalFOVAngle;
        m_controller.m_FOVDistance = m_controller.OriginalFOVDistance;
        base.Exit();
    }
}
