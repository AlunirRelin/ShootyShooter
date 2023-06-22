using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Mirror;
using Inputs;

public class PlayerMovementController : NetworkBehaviour
{
    public CinemachineVirtualCamera cvc;
    public float moveSpeed;
    public float jumpHeight;
    public float groundDrag;
    public float airDragMult;
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public InputAction controls;

    Vector2 direction = new Vector2(0,0);
    Vector3 movement = Vector3.zero;
    Rigidbody rb;
    public override void OnStartAuthority()
    {
        enabled = true;
        controls.Enable();
    }
    [Client]
    private void Start()
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        if (!isOwned) { return; }
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        direction = controls.ReadValue<Vector2>();
        movement = new Vector3(direction.x, 0, direction.y);
        grounded =  Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }
    void FixedUpdate()
    {
        if (!isOwned) { return;}
        if (grounded)
        {
            rb.AddRelativeForce(movement * moveSpeed);
        }
        else
        {
            rb.AddRelativeForce(movement * (moveSpeed * airDragMult));
        }
        if (Input.GetKey(KeyCode.Space) & grounded)
        {
            rb.AddForce(Vector3.up * jumpHeight);
        }
    }
}