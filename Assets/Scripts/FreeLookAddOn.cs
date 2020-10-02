//--------------------------------------------------------------------------------------//
// Workaround for Cinemachine, to work with new Input System 1.0.0
//https://medium.com/@smontambault64/unity-cinemachine-with-new-input-system-b608e13997c7
// Credits: Samuel Montambault
//--------------------------------------------------------------------------------------//

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookAddOn : MonoBehaviour
{
    [Range(0f, 10f)] public float LookSpeed = 1f;
    public bool InvertY = false;
    private CinemachineFreeLook _freeLookComponent;

    public void Start()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("Camera " + context.ReadValue<Vector2>());

        //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
        Vector2 lookMovement = context.ReadValue<Vector2>().normalized;
        lookMovement.y = InvertY ? -lookMovement.y : lookMovement.y;

        // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        lookMovement.x = lookMovement.x * 180f;

        //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        _freeLookComponent.m_XAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
        _freeLookComponent.m_YAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;
    }
}
