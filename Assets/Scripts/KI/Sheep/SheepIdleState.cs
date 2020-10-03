using UnityEngine;

public class SheepIdleState : ABaseState
{
    public override bool Enter()
    {
        //TODO Adjustments todo
        m_controller.m_FOVAngle = m_controller.OriginalFOVAngle;
        m_controller.m_FOVDistance = m_controller.OriginalFOVDistance;
        Debug.Log("Hier wäre die Update! SheepIdleState");
        return base.Enter();
    }

    public override void Update()
    {

    }
}