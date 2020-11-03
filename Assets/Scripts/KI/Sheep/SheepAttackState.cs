using UnityEngine;

public class SheepAttackState : ABaseState
{
    public override bool Enter()
    {
        m_controller.m_Agent.isStopped = true;
        // Debug.Log("Hier wäre die Update! SheepAttackState");
        return base.Enter();
    }
    
    public override void Update()
    {
        Debug.Log("Attack Player!");
        m_controller.transform.LookAt(GameManager.Instance.PlayerTransform.position);
    }
}