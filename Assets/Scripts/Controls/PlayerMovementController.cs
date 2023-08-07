using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    public CinemachineVirtualCamera cvc;
    public float moveSpeed;
    public float jumpHeight;
    public float groundDrag;
    public float airDragMult;
    public float playerHeight;
    public float velocityX;
    public float velocityZ;
    public float airControlMult;

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
        if (!isOwned) { return; }
        cvc = GetComponent<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!isOwned) { return; }
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
            Debug.Log("X = " +rb.velocity.x +" z = "+ rb.velocity.z);
            
            rb.AddForce(Vector3.up * jumpHeight);
        }
        /*if (Input.GetKey(KeyCode.W))
        {
            velocityX = rb.velocity.x;
            velocityZ = rb.velocity.z;
            rb.velocity = new(rb.velocity.x - (rb.velocity.x * airControlMult),rb.velocity.y,rb.velocity.z - (rb.velocity.z * airControlMult));
            rb.AddRelativeForce(Vector3.forward * ((velocityX + velocityZ)*airControlMult));
            rb.velocity = rb.velocity / airControlMult;
        }*/
    }
}