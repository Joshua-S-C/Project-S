using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private List<GameObject> explodeableObjects;
    private float knockback;
    private static float yDivideAmount = 3;
    public float stunDuration;
    public string explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        explodeableObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExplodeKnockback()
    {
        for (int i = 0; i < explodeableObjects.Count; i++)
        {

            Vector2 distance = explodeableObjects[i].transform.position - transform.position;
            Vector2 direction = distance.normalized;
            direction = new Vector2(direction.x, direction.y / yDivideAmount);
            explodeableObjects[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, explodeableObjects[i].GetComponent<Rigidbody2D>().velocity.y);
            explodeableObjects[i].GetComponent<Rigidbody2D>().velocity += direction * knockback;
            explodeableObjects[i].GetComponent<PlayerScript>().PlayerHit();
            explodeableObjects[i].GetComponent<PlayerScript>().DisableMovement(stunDuration);
            if(knockback == 0)
            {
                explodeableObjects[i].GetComponent<PlayerScript>().StopMovement();
            }
            
        }
    }
    private void ExplodeStun()
    {
        for (int i = 0; i < explodeableObjects.Count; i++)
        {

            explodeableObjects[i].GetComponent<PlayerScript>().DisableMovement(stunDuration);
            explodeableObjects[i].GetComponent<PlayerScript>().PlayerHit();
        }
    }
    public void Explode()
    {
        
        if (explosionEffect == "knockback")
        {
            ExplodeKnockback();
        }
        else if(explosionEffect == "stun")
        {
            ExplodeStun();
        }
    }
    public void SetExplosionKnockback(float newKnockback)
    {
        knockback = newKnockback;
    }
    public void SetStunDuration(float newStunDuration)
    {
        stunDuration = newStunDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            explodeableObjects.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            explodeableObjects.Remove(collision.gameObject);
        }
    }
}
