using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    [SerializeField, Tooltip("Transform of the camera")]
    private Transform p_cameraTransform;


    private void Start()
    {
        p_cameraTransform = FindObjectOfType<Camera>().transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + p_cameraTransform.forward);
    }
}
