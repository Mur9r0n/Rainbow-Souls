using UnityEngine;

public class BeeWalkState : ABaseState
{
    private float angle;
    private float distance;
    public override bool Enter()
    {
        angle = m_controller.m_FOVAngle;
        distance = m_controller.m_FOVDistance;
        
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
        m_controller.m_FOVAngle = angle;
        m_controller.m_FOVDistance = distance;
        base.Exit();
    }
}
