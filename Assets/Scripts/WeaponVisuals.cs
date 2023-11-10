using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponVisuals : MonoBehaviour
{
    public Transform attachedPlayer;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool rotated;
    //public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        attachedPlayer = transform.root; // Can also be a GameObject and do transform.parent.parent.gameobject
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
        Vector2 aimDirection = attachedPlayer.GetComponent<PlayerScript>().GetAimDirection();
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y);
        sr.flipY = angle < 0;
    }
}
