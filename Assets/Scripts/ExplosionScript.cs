using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private List<GameObject> explodeableObjects;
    public float knockback;
    public float yDivideAmount;
    public float playerMovementDisabledDuration;
    // Start is called before the first frame update
    void Start()
    {
        explodeableObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explode()
    {
        for(int i = 0; i < explodeableObjects.Count; i++)
        {

            Vector2 distance = explodeableObjects[i].transform.position - transform.position;
            Vector2 direction = distance.normalized;
            direction = new Vector2(direction.x, direction.y / yDivideAmount);
            //explodeableObjects[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, explodeableObjects[i].GetComponent<Rigidbody2D>().velocity.y);
            explodeableObjects[i].GetComponent<Rigidbody2D>().velocity += direction * knockback;
            explodeableObjects[i].GetComponent<PlayerScript>().DisableMovement(playerMovementDisabledDuration);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            explodeableObjects.Add(collision.gameObject);
        }
    }
}
