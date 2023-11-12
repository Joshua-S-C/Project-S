using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalScript : MonoBehaviour
{
    private bool canHitSelfPlayer = false;
    private float selfPlayerHitDelay = 0.3f;
    private GameObject selfPlayer;
    public float throwDelay;
    public float throwSpeed;
    public bool explodable;
    public bool explodeOnImpact;
    public bool stickOnImpact;
    public float stickExplodeDelay;
    private float stickExplodeDelayTimer;
    private bool isStuck;
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
    public bool destroyOnContact;
    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = explosionDelay;
    }

    // Update is called once per frame
    void Update()
    {
        CheckOffMap();
        ExplosionTimerTick();
        HitDelayTimerTick();
        StickExplodeDelayTimerTick();
    }
    private void StickExplodeDelayTimerTick()
    {
        if(isStuck)
        {
            stickExplodeDelayTimer -= Time.deltaTime;
            if(stickExplodeDelayTimer <= 0)
            {
                Explode();
            }
        }
    }
    private void CheckOffMap()
    {
        if (transform.position.magnitude > 40)
        {
            Destroy(gameObject);
        }
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
        isStuck = true;
        stickExplodeDelayTimer = stickExplodeDelay;
        transform.rotation = Quaternion.identity;
        transform.SetParent(newObject.transform, true);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
        

    }
    private void ImpactKnockback(GameObject newObject)
    {
        newObject.GetComponent<PlayerScript>().PlayerHit();
        newObject.GetComponent<PlayerScript>().DisableMovement(0.25f);
        newObject.GetComponent<PlayerScript>().DealKnockback((Vector2)transform.right * impactKnockback);
        if(!explodable)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if (!stickOnImpact)
            {
                ImpactKnockback(collision.gameObject);
            }
            if (explodable && explodeOnImpact)
            {
                Explode();
            }
            

        }
        if(stickOnImpact && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground"))
        {
            StickToObject(collision.gameObject);
        }
        if(destroyOnContact && (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground"))
        {
            Destroy(gameObject);
        }
    }
}
