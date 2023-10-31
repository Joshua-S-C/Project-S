using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootHitboxScript : MonoBehaviour
{
    int groundObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            groundObjects++;
            transform.parent.GetComponent<PlayerScript>().SetGrounded(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            groundObjects--;
            if(groundObjects <= 0)
            {
                transform.parent.GetComponent<PlayerScript>().SetGrounded(false);
            }
            
        }
    }
}
