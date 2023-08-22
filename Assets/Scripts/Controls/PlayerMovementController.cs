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
    public float airControlMult;
    private Vector3 tempVelocity;
    private Vector3 tempVelocity2;
    public int ticksPerSecond = 60;
    public float rotationAmount;
    private bool airControlable;


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
<<<<<<< Updated upstream

        if (Input.GetKey(KeyCode.Space) & grounded)
        {
            Debug.Log("X = " +rb.velocity.x +" z = "+ rb.velocity.z);
            rb.AddForce(Vector3.up * jumpHeight);
            rb.velocity = Vector3.Scale(rb.velocity, new(0.7f, 1, 0.7f));
            rb.AddRelativeForce(Vector3.forward * (Mathf.Abs(rb.velocity.x) +Mathf.Abs(rb.velocity.z))*airControlMult);
=======
        if(Input.GetKey(KeyCode.W) & !grounded)
        {
            airControlable = true;
        }
        else
        {
            airControlable = false;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        
=======
        if (Input.GetKey(KeyCode.Space) & grounded)
        {
            Debug.Log("X = " +rb.velocity.x +" z = "+ rb.velocity.z);
            
            rb.AddForce(Vector3.up * jumpHeight);
        }
    }
    private IEnumerator airControl()
    {
        WaitForSeconds Wait = new WaitForSeconds(1f/ ticksPerSecond);
        if (airControlable) {
            tempVelocity = new(rb.velocity.x, 0, rb.velocity.z);
            tempVelocity2 = rb.transform.forward * tempVelocity.magnitude;
            rb.velocity = new(((rb.velocity.x * (airControlMult - 1)) + tempVelocity2.x) / airControlMult, rb.velocity.y, ((rb.velocity.z * (airControlMult - 1)) + tempVelocity2.z) / airControlMult);
        }
        yield return null;
>>>>>>> Stashed changes
    }
}