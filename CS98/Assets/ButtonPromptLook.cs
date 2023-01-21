using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPromptLook : MonoBehaviour
{
    public Transform cameraTransform;
    public Canvas buttonCanvas;

    private void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("ButtonLookTransform").transform;
        buttonCanvas.worldCamera = Camera.main;
    }
    void Update()
    {
        transform.LookAt(cameraTransform);
    }
}
