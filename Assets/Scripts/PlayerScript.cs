using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D RB;
    private Vector2 MovementInput;
    public float jumpHeight;
    public float accelerationSpeed;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

        Movement();
    }
    private void Jump()
    {
        RB.velocity += (Vector2)transform.up * jumpHeight;
    }
    private void Movement()
    {
        RB.velocity += (Vector2)transform.right * accelerationSpeed * MovementInput * Time.deltaTime;
        if((MovementInput.x > 0 && RB.velocity.x > maxSpeed * MovementInput.x) || (MovementInput.x < 0 && RB.velocity.x < maxSpeed * MovementInput.x))
        {
            RB.velocity = new Vector2(maxSpeed * MovementInput.x, RB.velocity.y);
        }
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }
    public void JumpPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Jump();
        }
    }
}
