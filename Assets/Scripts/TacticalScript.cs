using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalScript : MonoBehaviour
{
    private bool canHitSelfPlayer = false;
    private float selfPlayerHitDelay = 0.1f;
    private GameObject selfPlayer;
    public float throwDelay;
    public float throwSpeed;
    public bool explodable;
    public bool explodeOnImpact;
    public bool stickOnImpact;
    public bool triggerable;
    public float explosionKnockback;
    //how long after throw the tactical explodes
    public float explosionDelay;
    private float explosionTimer;
    public bool explosionTimerEnabled;
    public int tacticalCount;
    public float tacticalCooldown;
    public float impactKnockback;
    public float stunDuration;
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
                if(GetComponent<Collider2D>() != null)
                {
                    Physics2D.IgnoreCollision(selfPlayer.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                }
                
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
        transform.GetChild(0).GetComponent<ExplosionScript>().SetStunDuration(stunDuration);
        transform.GetChild(0).GetComponent<ExplosionScript>().Explode();
        Destroy(gameObject);
    }
    public void TriggerTactical()
    {

            if(explodable)
            {
                Explode();
            }
        
    }
    public float GetThrowSpeed()
    {
        return throwSpeed;
    }
    public float GetThrowDelay()
    {
        return throwDelay;
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
    public bool GetTriggerable()
    {
        return triggerable;
    }
    private void StickToObject(GameObject newObject)
    {
        transform.SetParent(newObject.transform, true);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
        

    }
    private void ImpactKnockback(GameObject newObject)
    {
        newObject.GetComponent<PlayerScript>().PlayerHit();
        newObject.GetComponent<PlayerScript>().DisableMovement(0.25f);
        newObject.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.right * impactKnockback;
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ImpactKnockback(collision.gameObject);
            if (explodable && explodeOnImpact)
            {
                Explode();
            }
            
        }
        if(stickOnImpact && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground"))
        {
            StickToObject(collision.gameObject);
        }
    }
}
