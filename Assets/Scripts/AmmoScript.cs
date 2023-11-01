using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    private bool canHit = false;
    private float hitDelay = 0.01f;
    public bool explodable;
    public float knockback;
    private List<GameObject> hitableObjects;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        hitableObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HitDelayTimerTick();
    }
    private void HitDelayTimerTick()
    {
        if(!canHit)
        {
            hitDelay -= Time.deltaTime;
            if(hitDelay <= 0)
            {
                canHit = true;
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }
    private void HitObject(GameObject newObject)
    {
        if(explodable)
        {
            transform.GetChild(0).GetComponent<ExplosionScript>().Explode();
        }
        if(newObject.tag == "Player")
        {
            newObject.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.right * knockback;
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            HitObject(collision.gameObject);
            
            
        }
    }
}
