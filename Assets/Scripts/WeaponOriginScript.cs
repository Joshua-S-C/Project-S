using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOriginScript : MonoBehaviour
{
    public GameObject primaryWeaponPrefab;
    public GameObject secondaryWeaponPrefab;
    private GameObject primaryWeapon;
    private GameObject secondaryWeapon;
    private GameObject currentWeapon;
    private string currentWeaponName;
    // Start is called before the first frame update
    void Start()
    {
        SetupWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetupWeapons()
    {
        currentWeaponName = "Primary";
        primaryWeapon = Instantiate(primaryWeaponPrefab,transform);
        primaryWeapon.SetActive(true);
        secondaryWeapon = Instantiate(secondaryWeaponPrefab,transform);
        secondaryWeapon.SetActive(false);
        currentWeapon = primaryWeapon;
    }
    public void ShootWeapon(GameObject player)
    {
        //player is passed to prevent the bullet from hitting the player that fired it
        currentWeapon.GetComponent<WeaponScript>().Shoot(player);
    }    
    public void SwitchWeapon()
    {
        if(currentWeaponName == "Primary")
        {
            secondaryWeapon.SetActive(true);
            primaryWeapon.SetActive(false);
            currentWeapon = secondaryWeapon;
            currentWeaponName = "Secondary";
        }
        else if (currentWeaponName == "Secondary")
        {
            primaryWeapon.SetActive(true);
            secondaryWeapon.SetActive(false);
            currentWeapon = primaryWeapon;
            currentWeaponName = "Primary";
        }
    }
}
