using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleScript : MonoBehaviour
{
    public GameObject doubleJumpParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoubleJumpParticle()
    {
        GameObject particle = Instantiate(doubleJumpParticle,GameObject.Find("Particles").transform);
        particle.transform.position = transform.position;
    }
}
