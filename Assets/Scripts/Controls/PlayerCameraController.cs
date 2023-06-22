using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using Inputs;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("camera")]
    [SerializeField] float senseY;
    [SerializeField] float senseX;
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private Inputs.Controls controls;
    private Inputs.Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Inputs.Controls();
        }
    }
    public override void OnStartAuthority()
    {
        virtualCamera.gameObject.SetActive(true);
        enabled = true; 

        Controls.Player.MouseLook.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }
    [ClientCallback]
    private void OnEnable() => Controls.Enable();
    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;
        virtualCamera.transform.Rotate(-lookAxis.y * senseY * deltaTime, 0f, 0f);
        playerTransform.Rotate(0f, lookAxis.x * senseX * deltaTime, 0f);
    }
}
