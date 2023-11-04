using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerProfileManagerScript.AddProfile(GetComponent<PlayerInput>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
