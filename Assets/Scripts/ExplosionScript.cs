using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private List<GameObject> explodeableObjects;
    public float knockback;
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
            explodeableObjects[i].GetComponent<Rigidbody2D>().velocity += direction * knockback;
            Debug.Log((direction));
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
