using UnityEngine;

public class SheepWalkState : ABaseState
{
    public override bool Enter()
    {
        m_controller.m_Agent.isStopped = false;
        //TODO Adjustments todo
        m_controller.m_FOVAngle = 55f;
        m_controller.m_FOVDistance = 15f;
        Debug.Log("Hier wäre die Update! SheepWalkState");
        return base.Enter();
    }

    public override void Update()
    {
        m_controller.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
    }
}