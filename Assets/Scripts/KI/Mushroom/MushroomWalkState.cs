using UnityEngine;

public class MushroomWalkState : ABaseState
{
    private float angle;
    private float distance;
    public override bool Enter()
    {
        angle = m_mushroomController.m_FOVAngle;
        distance = m_mushroomController.m_FOVDistance;

        m_mushroomController.m_Agent.isStopped = false;
        m_mushroomController.m_FOVAngle *= 1.5f;
        m_mushroomController.m_FOVDistance *= 1.25f;
        return base.Enter();
    }

    public override void Update()
    {
        m_mushroomController.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
    }

    public override void Exit()
    {
        m_mushroomController.m_FOVAngle = angle;
        m_mushroomController.m_FOVDistance = distance;
        base.Exit();
    }
}
