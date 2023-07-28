using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField]
    private GunBase gun;
    public InputManager inputManager;
    Cinemachine.CinemachineVirtualCamera virtualCamera;
    Vector3 lookRot = new Vector3(0, 0, 0);
    public Rigidbody rb;
    void Start()
    {
        inputManager = InputManager.Instance;
        virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }
    void Update()
    {
        if (!isOwned) { return; }
        lookRot = new Vector3(inputManager.mouseX, inputManager.mouseY, 0);
        virtualCamera.transform.rotation = Quaternion.LookRotation(lookRot);
    }
}
