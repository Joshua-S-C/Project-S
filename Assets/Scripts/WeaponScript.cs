using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject ammo;
    public float maxAmmo;
    private float currentAmmo;
    public float fireDelay;
    public float fireVelocity;
    private bool isFireDelayTimer;
    private float fireDelayTimer;

    public float bulletCount;
    public bool burst;
    public float burstShotDelay;
    public bool spread;
    public float spreadArc;

    public float reloadTime;
    private float reloadTimer;
    private bool isReloading;

    private bool isCurrenWeapon;
    // Start is called before the first frame update
    void Awake()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ReloadTimerTick();
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
    public void SwitchToWeapon()
    {
        isCurrenWeapon = true;
        GameObject player = transform.root.gameObject;
        GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, GetCurrentRatio());
        GetComponent<SpriteRenderer>().enabled = true;
    }
    public void SwitchOffWeapon()
    {
        isCurrenWeapon = false;
        GetComponent<SpriteRenderer>().enabled = false;
        
    }
    private float GetCurrentRatio()
    {
        float ratio = 0;
        if (isReloading)
        {
            ratio = reloadTimer / reloadTime;
        }
        else
        {
            ratio = currentAmmo / maxAmmo;
        }
        return ratio;
    }
    private void ReloadTimerTick()
    {
        if(isReloading)
        {
            GameObject player = transform.root.gameObject;
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                isReloading = false;
                currentAmmo = maxAmmo;
            }
            if(isCurrenWeapon)
            {
                GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, GetCurrentRatio());
            }
            
        }
    }
    private void StartReloading()
    {
        reloadTimer = 0;
        isReloading = true;
    }
    private bool CheckCanShoot()
    {
        if(isFireDelayTimer || isReloading)
        {
            return false;
        }
        return true;
    }
    public void Use(GameObject player)
    {
        Shoot(player);
    }
    private void Shoot(GameObject player)
    {
        if(CheckCanShoot())
        {
            currentAmmo--;
            if(currentAmmo <= 0)
            {
                StartReloading();
            }
            float ratio = currentAmmo / maxAmmo;
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, ratio);
            StartFireDelayTimer();

            if (burst)
            {

            }
            else if(spread)
            {
                for(int i = 1;i < bulletCount + 1;i++)
                {
                    GameObject firedObject = Instantiate(ammo, GameObject.Find("ShotThings").transform);
                    firedObject.transform.position = transform.position;
                    firedObject.transform.rotation = transform.rotation;
                    float angle = ((spreadArc / bulletCount) * i) - (spreadArc / 2) - ((spreadArc / bulletCount) / 2);
                    firedObject.transform.Rotate(0, 0,angle);
                    firedObject.GetComponent<Rigidbody2D>().velocity = firedObject.transform.right * fireVelocity;
                    firedObject.GetComponent<AmmoScript>().AddSelfPlayer(player);
                }
                
            }
            else
            {
                GameObject firedObject = Instantiate(ammo, GameObject.Find("ShotThings").transform);
                firedObject.transform.position = transform.position;
                firedObject.transform.rotation = transform.rotation;
                firedObject.GetComponent<Rigidbody2D>().velocity = firedObject.transform.right * fireVelocity;
                firedObject.GetComponent<AmmoScript>().AddSelfPlayer(player);
            }
            

        }
    }
}
