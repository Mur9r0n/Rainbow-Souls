using UnityEngine;

public class BeeWalkState : ABaseState
{
    private float angle;
    private float distance;
    public override bool Enter()
    {
        angle = m_beeController.m_FOVAngle;
        distance = m_beeController.m_FOVDistance;
        
        m_beeController.m_Agent.isStopped = false;
        m_beeController.m_FOVAngle *= 1.5f;
        m_beeController.m_FOVDistance *= 1.25f;
        return base.Enter();
    }

    public override void Update()
    {
        m_beeController.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
    }

    public override void Exit()
    {
        m_beeController.m_FOVAngle = angle;
        m_beeController.m_FOVDistance = distance;
        base.Exit();
    }
}
