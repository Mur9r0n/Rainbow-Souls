using UnityEngine;

public class MushroomAttackState : ABaseState
{
    private MushroomController m_mushroomController = null;
    private Animator m_anim = null;
    private int m_attack = Animator.StringToHash("Melee Attack");
    private Vector3 m_transformPosition;
    private Vector3 m_playerTransformPosition;

    private float m_cloudTimer = 0;
    private float m_attackTimer = 0;

    public override bool Enter()
    {
        m_mushroomController = m_controller.GetComponent<MushroomController>();
        m_anim = m_controller.GetComponent<Animator>();
        return base.Enter();
    }

    public override void Update()
    {
        m_transformPosition = m_controller.transform.position;
        m_playerTransformPosition = GameManager.Instance.PlayerTransform.position;

        if (m_cloudTimer > 0)
        {
            m_cloudTimer -= Time.deltaTime;
        }

        if (m_attackTimer > 0)
        {
            m_attackTimer -= Time.deltaTime;
        }

        m_controller.transform.LookAt(m_playerTransformPosition);

        if (Vector3.Distance(m_playerTransformPosition, m_transformPosition) <= m_mushroomController.m_PoisonDistance &&
            Vector3.Distance(m_playerTransformPosition, m_transformPosition) > m_controller.m_AttackDistance)
        {
            m_controller.m_Agent.isStopped = false;
            m_controller.m_Agent.SetDestination(m_playerTransformPosition + new Vector3(0, 1, 0));
            if (m_cloudTimer <= 0)
            {
                MonoBehaviour.Instantiate(m_mushroomController.m_PoisonCloudPrefab, m_transformPosition, Quaternion.identity);
                m_cloudTimer = m_mushroomController.m_PoisonCloudDelay;
            }
        }

        if (Vector3.Distance(m_playerTransformPosition, m_transformPosition) <= m_controller.m_AttackDistance)
        {
            m_controller.m_Agent.isStopped = true;

            if (m_attackTimer <= 0)
            {
                    m_anim.SetTrigger(m_attack);
                    m_attackTimer = m_controller.m_AttackDelay;
            }
        }
    }
}