using UnityEngine;

public class BeeAttackState : ABaseState
{
    public override bool Enter()
    {
        m_beeController.m_Agent.isStopped = true;
        Debug.Log("Hier wäre die Update! BeeAttackState");
        return base.Enter();
    }
    
    public override void Update()
    {
        Debug.Log("Attack Player!");
        m_beeController.transform.LookAt(GameManager.Instance.PlayerTransform.position);
    }
}
