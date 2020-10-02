using UnityEngine;

public class SheepAttackState : ABaseState
{
    public override void Update()
    {
        Debug.Log(m_controller);
        // Debug.Log("Hier wäre die Update! SheepAttackState");
    }
    // public override bool Enter()
    // {
    //     m_controller.m_Agent.SetDestination(GameManager.Instance.PlayerTransform.position);
    //     return base.Enter();
    // }
}