using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimatorParams();
        UpdatePlayerFlip();
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
    private void UpdatePlayerFlip()
    {
        Vector2 aimDirection = gameObject.GetComponent<PlayerScript>().GetAimDirection();
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y);
        sr.flipX = angle < 0;
    }

}
