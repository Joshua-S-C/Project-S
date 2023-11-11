using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalScript : MonoBehaviour
{
    private bool canHitSelfPlayer = false;
    private float selfPlayerHitDelay = 0.1f;
    private GameObject selfPlayer;
    public float throwSpeed;
    public bool explodable;
    public bool explodeOnImpact;
    public bool triggerable;
    public float explosionKnockback;
    //how long after throw the tactical explodes
    public float explosionDelay;
    private float explosionTimer;
    public bool explosionTimerEnabled;
    public int tacticalCount;
    public float tacticalCooldown;
    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = explosionDelay;
    }

    // Update is called once per frame
    void Update()
    {
        ExplosionTimerTick();
        HitDelayTimerTick();
    }
    private void HitDelayTimerTick()
    {
        if (!canHitSelfPlayer)
        {
            selfPlayerHitDelay -= Time.deltaTime;
            if (selfPlayerHitDelay <= 0)
            {
                canHitSelfPlayer = true;
                Physics2D.IgnoreCollision(selfPlayer.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
        }
    }
    private void ExplosionTimerTick()
    {
        if(explosionTimerEnabled && explodable)
        {
            explosionTimer -= Time.deltaTime;
            if(explosionTimer <= 0)
            {
                Explode();
            }
        }
    }
    private void Explode()
    {
        transform.GetChild(0).GetComponent<ExplosionScript>().SetExplosionKnockback(explosionKnockback);
        transform.GetChild(0).GetComponent<ExplosionScript>().Explode();
        Destroy(gameObject);
    }
    public void TriggerTactical()
    {
        if(triggerable)
        {
            if(explodable)
            {
                Explode();
            }
        }
    }
    public float GetThrowSpeed()
    {
        return throwSpeed;
    }
    public void SetThrowPlayer(GameObject player)
    {
        selfPlayer = player;
        Physics2D.IgnoreCollision(selfPlayer.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
    }
    public int GetTacticalCount()
    {
        return tacticalCount;
    }
    public float GetTacticalCooldown()
    {
        return tacticalCooldown;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (explodable && explodeOnImpact)
            {
                Explode();
            }
        }
    }
}
