using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponVisuals : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        UpdateWeaponFlip();
    }

    /// <summary>
    /// Flips sprite based on where player is aiming (it's the same as update )
    /// </summary>
    private void UpdateWeaponFlip()
    {
        Vector2 aimDirection = gameObject.GetComponent<PlayerScript>().GetAimDirection();
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y);
        sr.flipX = angle < 0;
    }
}
