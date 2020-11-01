using UnityEngine;

public class SheepWalkState : ABaseState
{
    private float angle;
    private float distance;
    public override bool Enter()
    {
        angle = m_sheepController.m_FOVAngle;
        distance = m_sheepController.m_FOVDistance;

        m_sheepController.m_Agent.isStopped = false;
        m_sheepController.m_FOVAngle *= 1.5f;
        m_sheepController.m_FOVDistance *= 1.25f;
        return base.Enter();
    }

    public override void Update()
    {
        m_sheepController.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
    }

    public override void Exit()
    {
        m_sheepController.m_FOVAngle = angle;
        m_sheepController.m_FOVDistance = distance;
        base.Exit();
    }
}