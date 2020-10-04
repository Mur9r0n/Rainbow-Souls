using System.IO.IsolatedStorage;
using UnityEngine;

public class SheepResetState : ABaseState
{
    public override bool Enter()
    {
        m_controller.m_Agent.isStopped = false;
        m_controller.m_Agent.SetDestination(m_controller.OriginalPosition);
        Debug.Log("Hier wäre die Update! SheepResetState");
        return base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(m_controller.transform.position, m_controller.OriginalPosition) <= .2f)
        {
            m_controller.m_Agent.enabled = false;
            m_controller.transform.rotation = m_controller.OriginalRotation;
            m_controller.m_Agent.enabled = true;
            IsFinished = true;
        }
    }

    public override void Exit()
    {
        m_controller.m_FOVAngle = m_controller.OriginalFOVAngle;
        m_controller.m_FOVDistance = m_controller.OriginalFOVDistance;
        base.Exit();
    }
}