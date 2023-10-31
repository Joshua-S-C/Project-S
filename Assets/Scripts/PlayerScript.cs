using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D RB;
    private Vector2 MovementInput;
    private bool isGrounded;
    public float jumpHeight;
    public float accelerationSpeed;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerProfileManagerScript.AddProfile(GetComponent<PlayerInput>().devices[0]);
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Movement();
    }
    public void SetGrounded(bool newState)
    {
        isGrounded = newState;
    }
    private void Jump()
    {
        if(CheckCanJump())
        {
            RB.velocity += (Vector2)transform.up * jumpHeight;
        }
        
    }
    private bool CheckCanJump()
    {
        if(isGrounded)
        {
            return true;
        }
        return false;
    }
    private void Movement()
    {
        //moves player based on buttons pressed
        RB.velocity += (Vector2)transform.right * accelerationSpeed * MovementInput * Time.deltaTime;

        //checks if player speed is higher than the stick axis
        if((MovementInput.x > 0 && RB.velocity.x > maxSpeed * MovementInput.x) || (MovementInput.x < 0 && RB.velocity.x < maxSpeed * MovementInput.x))
        {
            RB.velocity = new Vector2(maxSpeed * MovementInput.x, RB.velocity.y);
        }
    }

    //methods performed when input is pressed
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
