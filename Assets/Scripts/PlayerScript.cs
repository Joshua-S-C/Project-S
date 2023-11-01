using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private GameObject weaponOrigin;
    private Rigidbody2D RB;
    private Vector2 MovementInput;
    private bool isGrounded;
    private Vector2 aimDirection;
    private Vector2 lastAimDirection;
    public float jumpHeight;
    public float groundAccelerationSpeed;
    public float airAccelerationSpeed;
    public float maxSpeed;
    public float groundDeccelerationSpeed;
    public float airDeccelerationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        weaponOrigin = transform.Find("WeaponOrigin").gameObject;
        PlayerProfileManagerScript.AddProfile(GetComponent<PlayerInput>().devices[0]);
    }
     
    // Update is called once per frame
    void Update()
    {
        CheckShootPressedKeyboard();
        CalculateAimDirection();
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
        if(MovementInput == Vector2.zero)
        {
            //deccelerates player when no movement is done
            Decceleration();
        }
        else
        {
            float accelerationAmount;

            if(isGrounded)
            {
                accelerationAmount = groundAccelerationSpeed * Time.deltaTime;
            }
            else
            {
                accelerationAmount = airAccelerationSpeed * Time.deltaTime;
            }

            //moves player based on buttons pressed
            RB.velocity += (Vector2)transform.right * accelerationAmount * MovementInput;

            //checks if player speed is higher than the stick axis
            if ((MovementInput.x > 0 && RB.velocity.x > maxSpeed * MovementInput.x) || (MovementInput.x < 0 && RB.velocity.x < maxSpeed * MovementInput.x))
            {
                RB.velocity = new Vector2(maxSpeed * MovementInput.x, RB.velocity.y);
            }
        }

        
    }
    private void Decceleration()
    {
        float deccelerationAmount;
        float sign = Mathf.Sign(RB.velocity.x);
        if(isGrounded)
        {
            deccelerationAmount = groundDeccelerationSpeed * Time.deltaTime;
        }
        else
        {
            deccelerationAmount = airDeccelerationSpeed * Time.deltaTime;
        }
        if(deccelerationAmount > (Mathf.Abs(RB.velocity.x)))
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
        }
        else
        {
            RB.velocity = new Vector2((Mathf.Abs(RB.velocity.x) - deccelerationAmount) * sign, RB.velocity.y);
        }
        
    }
    private void CalculateAimDirection()
    {
        if (GetComponent<PlayerInput>().devices[0].description.deviceClass == "Keyboard")
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)weaponOrigin.transform.position).normalized;
            aimDirection = direction;
        }

        UpdateAim();
    }    
    private void UpdateAim()
    {
        if(aimDirection == Vector2.zero)
        {
            weaponOrigin.transform.right = lastAimDirection;
        }
        else
        {
            weaponOrigin.transform.right = aimDirection;
            lastAimDirection = aimDirection;
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
    public void AimPressed(InputAction.CallbackContext context)
    {
        aimDirection = context.ReadValue<Vector2>();
    }
    public void ShootPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            transform.Find("WeaponOrigin").GetComponent<WeaponOriginScript>().ShootWeapon();
        }
    }
    private void CheckShootPressedKeyboard()
    {
        
        if (GetComponent<PlayerInput>().devices[0].description.deviceClass == "Keyboard")
        {
            if(Input.GetMouseButtonDown(0))
            {
                transform.Find("WeaponOrigin").GetComponent<WeaponOriginScript>().ShootWeapon();
            }
        }
        
    }
}
