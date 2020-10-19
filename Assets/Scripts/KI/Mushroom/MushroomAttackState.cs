using UnityEngine;

public class MushroomAttackState : ABaseState
{
    public override bool Enter()
    {
        m_mushroomController.m_Agent.isStopped = true;
        // Debug.Log("Hier wäre die Update! MushroomAttackState");
        return base.Enter();
    }
    
    public override void Update()
    {
        Debug.Log("Attack Player!");
        m_mushroomController.transform.LookAt(GameManager.Instance.PlayerTransform.position);
    }
}
