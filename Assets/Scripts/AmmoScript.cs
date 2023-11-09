using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    private bool canHit = false;
    private float selfPlayerHitDelay = 0.1f;
    private GameObject selfPlayer;
    public bool explodable;
    private float knockback;
    private List<GameObject> hitableObjects;
    // Start is called before the first frame update
    void Start()
    {
        
        hitableObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HitDelayTimerTick();
        CheckOffMap();
    }
    private void HitDelayTimerTick()
    {
        if(!canHit)
        {
            selfPlayerHitDelay -= Time.deltaTime;
            if(selfPlayerHitDelay <= 0)
            {
                canHit = true;
                Physics2D.IgnoreCollision(selfPlayer.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
            }
        }
    }
    private void CheckOffMap()
    {
        if(transform.position.magnitude > 40)
        {
            Destroy(gameObject);
        }
    }
    public void AddSelfPlayer(GameObject player)
    {
        selfPlayer = player;
        Physics2D.IgnoreCollision(selfPlayer.GetComponent<Collider2D>(),GetComponent<Collider2D>(),true);
    }
    private void HitObject(GameObject newObject)
    {
        if(explodable)
        {
            transform.GetChild(0).GetComponent<ExplosionScript>().Explode();
        }
        if(newObject.tag == "Player")
        {
            newObject.GetComponent<PlayerScript>().PlayerHit();
            newObject.GetComponent<PlayerScript>().DisableMovement(0.25f);
            newObject.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.right * knockback;
        }
        Destroy(gameObject);
    }
    public void SetKnockback(float newKnockback,float explosionKnockback)
    {
        if (explodable)
        {
            transform.Find("ExplosionHitbox").GetComponent<ExplosionScript>().SetExplosionKnockback(explosionKnockback);
        }
        
        knockback = newKnockback;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            HitObject(collision.gameObject);
            
            
        }
    }
}
