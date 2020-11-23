using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputs m_playerInputs = null;
    private CharacterController m_controller = null;
    private Animator m_anim = null;
    private PlayerStats m_playerStats = null;
    private Transform m_mainCameraTransform = null;

    private float m_currentSpeed = 0f, 
                  m_speedSmoothVelocity = 0f, 
                  m_speedSmoothTime = 0.1f, 
                  m_rotationSpeed = 0.1f;

    private bool m_useGravity = true;
    
    
    private int m_walking = Animator.StringToHash("Walking");
    
    public CinemachineFreeLook m_cinemachineFreeLook = null;

    void Awake()
    {
        m_playerInputs = new PlayerInputs();
        m_controller = GetComponent<CharacterController>();
        m_playerStats = GetComponent<PlayerStats>();
        m_mainCameraTransform = Camera.main.transform;
        m_anim = GetComponent<Animator>();
        
        m_playerInputs.Player.Sprint.performed += _ => Sprint();
    }

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    
    void Update()
    {
        Debug.Log(m_playerInputs.Player.Movement.ReadValue<Vector2>());
        Movement(m_playerInputs.Player.Movement.ReadValue<Vector2>());
        if (m_useGravity) Gravity();
    }

    public void Movement(Vector2 _context)
    {
        if (!m_playerStats.m_targetEnemy || m_playerStats.m_isSprinting)
        {
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_WaitTime = 0.5f;
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_RecenteringTime = 1.0f;
            if (_context != Vector2.zero)
            {
                m_anim.SetBool(m_walking, true);
                Vector2 movementInput = new Vector2(_context.x, _context.y);

                Vector3 forward = m_mainCameraTransform.forward;
                Vector3 right = m_mainCameraTransform.right;
                forward.y = 0;
                right.y = 0;

                forward.Normalize();
                right.Normalize();

                Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

                if (desiredMoveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),
                        m_rotationSpeed);
                }

                float targetSpeed = m_playerStats.m_MovementSpeed.x * movementInput.magnitude;
                if (m_playerStats.m_isSprinting)
                    m_currentSpeed = m_playerStats.m_MovementSpeed.y;
                else
                    m_currentSpeed = Mathf.SmoothDamp(m_currentSpeed, targetSpeed, ref m_speedSmoothVelocity,
                        m_speedSmoothTime);

                m_controller.Move(desiredMoveDirection * m_currentSpeed * Time.deltaTime);


                if (!m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled)
                {
                    m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true;
                }
            }
            else
            {
                m_anim.SetBool(m_walking, false);
                m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = false;
            }
        }
        else if (m_playerStats.m_targetEnemy)
        {
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_WaitTime = 0;
            m_cinemachineFreeLook.m_RecenterToTargetHeading.m_RecenteringTime = 0.1f;
            Vector3 desiredMoveDirection = (m_playerStats.m_targetEnemy.transform.position - transform.position).normalized;

            if (desiredMoveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection),
                    1f);
            }

            if (_context != Vector2.zero)
            {
                m_anim.SetBool(m_walking, true);
                Vector2 movementInput = new Vector2(_context.x, _context.y);

                float targetSpeed = m_playerStats.m_MovementSpeed.x * movementInput.magnitude;

                if (m_playerStats.m_isSprinting)
                {
                    m_currentSpeed = m_playerStats.m_MovementSpeed.y;
                }

                else
                {
                    m_currentSpeed = Mathf.SmoothDamp(m_currentSpeed, targetSpeed, ref m_speedSmoothVelocity,
                        m_speedSmoothTime);
                }

                Vector3 movement = transform.forward * movementInput.y + transform.right * movementInput.x;
                m_controller.Move(movement * Time.deltaTime * m_currentSpeed);

                if (!m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled)
                {
                    m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true;
                }
            }
            else
            {
                m_anim.SetBool(m_walking, false);
                m_cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = false;
            }
        }
    }
    
    private void Gravity()
    {
        Vector3 gravityVector = Vector3.zero;

        if (!m_controller.isGrounded)
        {
            gravityVector.y += Physics.gravity.y;
        }

        m_controller.Move(gravityVector * Time.deltaTime);
    }
    
    public void Sprint()
    {
        Debug.Log("Sprint");

        m_playerStats.m_isSprinting = !m_playerStats.m_isSprinting;
    }
    
    private void OnEnable()
    {
        m_playerInputs.Enable();
    }

    private void OnDisable()
    {
        m_playerInputs.Disable();
    }
}
