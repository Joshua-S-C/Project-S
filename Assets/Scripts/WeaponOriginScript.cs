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

    public List<GameObject> weaponList;
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
        GetRandomWeapons();
        currentWeaponName = "Primary";
        primaryWeapon = Instantiate(primaryWeaponPrefab,transform);
        primaryWeapon.GetComponent<WeaponScript>().SwitchToWeapon();
        secondaryWeapon = Instantiate(secondaryWeaponPrefab,transform);
        secondaryWeapon.GetComponent<WeaponScript>().SwitchOffWeapon();
        currentWeapon = primaryWeapon;
    }
    private void GetRandomWeapons()
    {
        primaryWeaponPrefab = weaponList[Random.Range(0, weaponList.Count)];
    }
    public void UseWeapon(GameObject player,bool pressedDown)
    {
        //player is passed to prevent the bullet from hitting the player that fired it
        currentWeapon.GetComponent<WeaponScript>().Use(player,pressedDown);
    }    
    public void SwitchWeapon()
    {
        if(currentWeaponName == "Primary")
        {
            secondaryWeapon.GetComponent<WeaponScript>().SwitchToWeapon();
            primaryWeapon.GetComponent<WeaponScript>().SwitchOffWeapon();
            currentWeapon = secondaryWeapon;
            currentWeaponName = "Secondary";
        }
        else if (currentWeaponName == "Secondary")
        {
            primaryWeapon.GetComponent<WeaponScript>().SwitchToWeapon();
            secondaryWeapon.GetComponent<WeaponScript>().SwitchOffWeapon();
            currentWeapon = primaryWeapon;
            currentWeaponName = "Primary";
        }
    }
}
