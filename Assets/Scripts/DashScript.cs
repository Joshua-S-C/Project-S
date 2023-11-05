using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private Rigidbody2D RB;

    private bool canDash = true;
    private bool isDashing;
    public float dashDistance;
    public float dashSpeed;
    private float dashDistanceCovered;
    private Vector2 dashDirection;
    private Vector2 dashLastPosition;
    public float dashCooldown;
    private float dashCooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DashCooldownTimerTick();
    }
    private void FixedUpdate()
    {
        DashMovementTracker();
    }
    public void Dash(Vector2 input)
    {
        if(canDash && input != Vector2.zero)
        {
            dashDirection = (new Vector2(input.x, 0)).normalized;
            Debug.Log(dashDirection);
            dashDistanceCovered = 0;
            canDash = false;
            isDashing = true;
            GetComponent<PlayerScript>().DisableMovement();
            dashLastPosition = transform.position;
        }
        
    }
    private void DashMovementTracker()
    {
        if (isDashing)
        {
            RB.velocity = dashDirection * dashSpeed;
            dashDistanceCovered += ((Vector2)transform.position - dashLastPosition).magnitude;
            dashLastPosition = transform.position;
            Debug.Log(dashDistanceCovered);
            if (dashDistanceCovered > dashDistance)
            {
                isDashing = false;
                GetComponent<PlayerScript>().SetSpeedMovementInput();
                dashCooldownTimer = dashCooldown;
                GetComponent<PlayerScript>().EnableMovement();
            }
        }
    }
    private void DashCooldownTimerTick()
    {
        if (!canDash && !isDashing)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
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

}
