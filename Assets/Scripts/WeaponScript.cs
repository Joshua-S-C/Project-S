using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject ammo;
    public float fireDelay;
    public float fireVelocity;
    private bool isFireDelayTimer;
    private float fireDelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireDelayTimerTick();
    }
    private void StartFireDelayTimer()
    {
        fireDelayTimer = fireDelay;
        isFireDelayTimer = true;
    }
    private void FireDelayTimerTick()
    {
        if(isFireDelayTimer)
        {
            fireDelayTimer -= Time.deltaTime;
            if(fireDelayTimer <= 0)
            {
                isFireDelayTimer = false;
            }
        }
    }
    private bool CheckCanShoot()
    {
        if(isFireDelayTimer)
        {
            return false;
        }
        return true;
    }
    public void Shoot()
    {
        if(CheckCanShoot())
        {
            isFireDelayTimer = true;
            GameObject firedObject = Instantiate(ammo,GameObject.Find("ShotThings").transform);
            firedObject.transform.position = transform.position;
            firedObject.transform.rotation = transform.rotation;
            firedObject.GetComponent<Rigidbody2D>().velocity = firedObject.transform.right * fireVelocity;
        }
    }
}
