using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private DashScript myDashScript;

    private GameObject weaponOrigin;
    private Rigidbody2D RB;
    private Vector2 MovementInput;
    private bool isGrounded;
    private bool canDoubleJump;
    private int jumpCount;
    private Vector2 aimDirection;
    private Vector2 lastAimDirection;
    public float jumpHeight;
    public float doubleJumpHeight;
    public float groundAccelerationSpeed;
    public float airAccelerationSpeed;
    public float maxSpeed;
    public float groundDeccelerationSpeed;
    public float airDeccelerationSpeed;

    private bool usingWeaponPressed;
    private bool usingWeaponPressedDown;
    
    public int lives;
    private Vector2 respawnPosition;

    private bool movementDisabled;
    private bool isMovementDisabledTimer;
    private float movementDisabledTimer;

    public Vector2 bounds;

    private GameObject platformList;
    private bool isIgnorePlatforms;
    private bool isIgnorePlatformsTimer;
    private float ignorePlatformsDuration = 0.5f;
    private float ignorePlatformsTimer;

    // Start is called before the first frame update
    void Start()
    {

        platformList = GameObject.Find("OneWayPlatforms");
        respawnPosition = new Vector2(0,10);
        RB = GetComponent<Rigidbody2D>();
        weaponOrigin = transform.Find("WeaponOrigin").gameObject;
        PlayerProfileManagerScript.AddProfile(GetComponent<PlayerInput>());

        myDashScript = GetComponent<DashScript>();
    }
     
    // Update is called once per frame
    void Update()
    {
        IgnoreOneWayPlatformsTimerTick();
        CheckUseWeaponPressedKeyboard();
        CalculateAimDirection();
        MovementDisabledTimerTick();
        CheckOffScreen();

        UseWeapon();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void UseWeapon()
    {
        if(usingWeaponPressed)
        {
            
            weaponOrigin.GetComponent<WeaponOriginScript>().UseWeapon(gameObject,usingWeaponPressedDown);
            usingWeaponPressedDown = false;
        }
    }    
    private void Respawn()
    {
        
         transform.position = respawnPosition;
         RB.velocity = Vector2.zero;
        
        
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void CheckDead()
    {
        GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardLives(gameObject, lives);
        if (lives <= 0)
        {
            Die();
        }
        else
        {
            Respawn();
        }
    }
    private void CheckOffScreen()
    {
        if(Mathf.Abs(transform.position.x) > bounds.x || Mathf.Abs(transform.position.y) > bounds.y)
        {
            lives--;
            CheckDead();
        }
    }
    private bool CheckCanMove()
    {
        if(!movementDisabled)
        {
            return true;
        }
        return false;
    }
    public void SetGrounded(bool newState)
    {
        isGrounded = newState;
        canDoubleJump = true;
    }
    public void DisableMovement(float duration)
    {
        movementDisabled = true;
        isMovementDisabledTimer = true;
        movementDisabledTimer = duration;
    }
    public void DisableMovement()
    {
        movementDisabled = true;
    }
    public void EnableMovement()
    {
        movementDisabled = false;
    }
    public void PlayerHit()
    {
        myDashScript.StopDash();
    }
    private void MovementDisabledTimerTick()
    {
        if(isMovementDisabledTimer)
        {
            movementDisabledTimer -= Time.deltaTime;
            if(movementDisabledTimer <= 0)
            {
                movementDisabled = false;
                isMovementDisabledTimer = false;
            }
        }
    }
    private void Jump()
    {
        if(isGrounded)
        {
            RB.velocity += (Vector2)transform.up * jumpHeight;
        }
        else
        {
            if(canDoubleJump)
            {
                canDoubleJump = false;
                if(RB.velocity.y > 0)
                {
                    RB.velocity = new Vector2(RB.velocity.x, doubleJumpHeight + RB.velocity.y);
                }
                else
                {
                    RB.velocity = new Vector2(RB.velocity.x, doubleJumpHeight);
                }
                
                GetComponent<PlayerParticleScript>().DoubleJumpParticle();
            }
        }
        
    }
    
    private void Movement()
    {
        if(MovementInput == Vector2.zero && !myDashScript.GetIsDashing())
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

            float hypotheticalXVelocity = RB.velocity.x + (accelerationAmount * MovementInput.x);

            
            

            //checks if player speed is higher than the stick axis
            if ((MovementInput.x > 0 && hypotheticalXVelocity > maxSpeed * MovementInput.x) || (MovementInput.x < 0 && hypotheticalXVelocity < maxSpeed * MovementInput.x))
            {
                if((MovementInput.x > 0 && RB.velocity.x < maxSpeed * MovementInput.x) || (MovementInput.x < 0 && RB.velocity.x > maxSpeed * MovementInput.x))
                {
                    RB.velocity = new Vector2(maxSpeed * MovementInput.x, RB.velocity.y);
                }
                
            }
            if ((MovementInput.x > 0 && RB.velocity.x > maxSpeed * MovementInput.x) || (MovementInput.x < 0 && RB.velocity.x < maxSpeed * MovementInput.x))
            {
                if (CheckCanMove() && (Mathf.Sign(MovementInput.x) != Mathf.Sign(RB.velocity.x)))
                {
                    RB.velocity += (Vector2)transform.right * accelerationAmount * MovementInput;
                }
            }
            else
            {
                //moves player based on buttons pressed
                if (CheckCanMove())
                {
                    RB.velocity += (Vector2)transform.right * accelerationAmount * MovementInput;
                }
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
    private void IgnoreOneWayPlatforms(bool ignore)
    {
        isIgnorePlatforms = ignore;
        for (int i = 0;i < platformList.transform.childCount;i++)
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), platformList.transform.GetChild(i).GetComponent<Collider2D>(),ignore);
        }
    }
    private void IgnoreOneWayPlatformsTimerTick()
    {
        if(isIgnorePlatformsTimer)
        {
            ignorePlatformsTimer -= Time.deltaTime;
            if(ignorePlatformsTimer <= 0)
            {
                isIgnorePlatforms = false;
                IgnoreOneWayPlatforms(false);
            }
        }
    }  
    private void StartIgnoreOneWayPlatformsTimer()
    {
        if(isIgnorePlatforms)
        {
            isIgnorePlatformsTimer = true;
            ignorePlatformsTimer = ignorePlatformsDuration;
        }
        
    }



    //methods performed when input is pressed
    public void MovePressed(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        if(MovementInput.y < -0.9)
        {
            IgnoreOneWayPlatforms(true);
            
        }
        else
        {
            StartIgnoreOneWayPlatformsTimer();
        }
        
        
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
    public void DashPressed(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            myDashScript.Dash(MovementInput);
        }
    }
    public void UseWeaponPressed(InputAction.CallbackContext context)
    {
        if(context.performed && weaponOrigin != null)
        {
            usingWeaponPressedDown = true;
            usingWeaponPressed = true;
        }
        else if(context.canceled && weaponOrigin != null)
        {
            usingWeaponPressed = false;
        }
    }
    public void SwitchWeaponPressed(InputAction.CallbackContext context)
    {
        if (context.performed && weaponOrigin != null)
        {
            weaponOrigin.GetComponent<WeaponOriginScript>().SwitchWeapon();
        }
    }
    private void CheckUseWeaponPressedKeyboard()
    {
        
        if (GetComponent<PlayerInput>().devices[0].description.deviceClass == "Keyboard")
        {
            if (Input.GetMouseButtonDown(0) && weaponOrigin != null)
            {
                usingWeaponPressedDown = true;
            }
            if (Input.GetMouseButton(0) && weaponOrigin != null)
            {
                usingWeaponPressed = true;
            }
            else
            {
                usingWeaponPressed = false;
            }
        }
        
    }



}
