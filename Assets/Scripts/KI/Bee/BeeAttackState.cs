using UnityEngine;

public class BeeAttackState : ABaseState
{
    private BeeController m_beeController;

    private float m_shotTimer = 0f;
    
    public override bool Enter()
    {
        m_beeController = m_controller.GetComponent<BeeController>();
        m_controller.m_Agent.isStopped = true;
        return base.Enter();
    }
    
    public override void Update()
    {
        if (m_shotTimer > 0)
        {
            m_shotTimer -= Time.deltaTime;
        }
        
        m_controller.transform.LookAt(GameManager.Instance.PlayerTransform.position);

        if (m_shotTimer <= 0)
        {
            m_shotTimer = m_controller.m_AttackDelay;
            MonoBehaviour.Instantiate(m_beeController.m_StingPrefab, m_beeController.m_StingTransform.position,Quaternion.identity);
        }
    }
}
