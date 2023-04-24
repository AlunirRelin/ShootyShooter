using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [HideInInspector]
    public float mouseX = 0;
    [HideInInspector]
    public float mouseY = 0;
    // sensitividade do mouse
    float sensitivity = 100;
    Cinemachine.CinemachineVirtualCamera virtualCamera;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }
    void Update()
    {
        LookInput();
    }
    public void LookInput()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        mouseY = Mathf.Clamp(mouseY, -80f, 80f);
    }
}
