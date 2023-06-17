using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractionPromptUI : MonoBehaviour
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        var rotaion = _camera.transform.rotation;
        transform.LookAt(transform.position + rotaion * Vector3.forward, rotaion * Vector3.up);
    }
}
