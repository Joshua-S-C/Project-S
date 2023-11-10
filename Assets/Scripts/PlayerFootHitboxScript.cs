using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootHitboxScript : MonoBehaviour
{
    private int groundObjects;
    private bool istouchingGround;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
    }
    private void CheckGrounded()
    {
        if(istouchingGround && transform.parent.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            transform.parent.GetComponent<PlayerScript>().SetGrounded(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            istouchingGround = true;
            groundObjects++;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            groundObjects--;
            if(groundObjects <= 0)
            {
                istouchingGround = false;
                transform.parent.GetComponent<PlayerScript>().SetGrounded(false);
            }
            
        }
    }
}
