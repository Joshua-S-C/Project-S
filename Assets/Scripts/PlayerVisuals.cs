using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;
    
    // Temp vars to view in editor
    public float angle = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateAnimatorParams();
        UpdatePlayerDirection();
    }

    /// <summary>
    /// Updates all parameters of the Animator.
    /// </summary>
    private void UpdateAnimatorParams()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("Jumping", rb.velocity.y > 0);
        animator.SetBool("Falling", rb.velocity.y < -0);
        animator.SetBool("Dashing", gameObject.GetComponent<DashScript>().GetIsDashing());
    }

    /// <summary>
    /// Flips sprite based on where player is aiming
    /// </summary>
    private void UpdatePlayerDirection()
    {
        Vector2 aimDirection = gameObject.GetComponent<PlayerScript>().GetAimDirection();
        angle = Mathf.Atan2(aimDirection.x, aimDirection.y);
        sr.flipY = angle > 180;
    }

}
