using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOriginScript : MonoBehaviour
{
    private GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootWeapon(GameObject player)
    {
        weapon.GetComponent<WeaponScript>().Shoot(player);
    }    
}
