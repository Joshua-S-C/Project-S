using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private Rigidbody2D RB;

    private bool canDash = true;
    private bool isDashing;
    public float dashDuration;
    public float dashSpeed;
    private float dashTimer;
    private Vector2 dashDirection;
    private Vector2 dashLastPosition;
    public float dashCooldown;
    private float dashCooldownTimer;
    public float dashEndSpeed;
    private GameObject scoreBoardManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreBoardManager = GameObject.Find("ScoreboardManager");
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DashCooldownTimerTick();
    }
    private void FixedUpdate()
    {
        DashMovement();
    }
    public void Dash(Vector2 input)
    {
        if(canDash && input != Vector2.zero)
        {
            dashDirection = (new Vector2(input.x, input.y)).normalized;
            dashTimer = dashDuration;
            canDash = false;
            isDashing = true;
            GetComponent<PlayerScript>().DisableMovement();
            dashLastPosition = transform.position;
        }
        
    }
    private void DashMovement()
    {
        if (isDashing)
        {
            RB.velocity = dashDirection * dashSpeed;
            dashTimer -= Time.deltaTime;
            dashLastPosition = transform.position;
            scoreBoardManager.GetComponent<ScoreboardManagerScript>().UpdateScoreCardCooldowns(gameObject);
            if (dashTimer <= 0)
            {
                StopDash();
                RB.velocity = dashEndSpeed * dashDirection;
                
                
            }
        }
    }
    private void DashCooldownTimerTick()
    {
        if (!canDash && !isDashing)
        {
            scoreBoardManager.GetComponent<ScoreboardManagerScript>().UpdateScoreCardCooldowns(gameObject);
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
        }
    }
    public void StopDash()
    {
        if(isDashing)
        {
            isDashing = false;
            dashCooldownTimer = dashCooldown;
            GetComponent<PlayerScript>().EnableMovement();
        }
        
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
    public bool GetCanDash()
    {
        return canDash;
    }
    public float CalculateDashRatio()
    {
        float ratio = 0;
        if(isDashing)
        {
            ratio = dashTimer / dashDuration;
        }
        if(!canDash)
        {
            ratio = dashCooldownTimer / dashCooldown;
        }
        return ratio; 
    }

}
