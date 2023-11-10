using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject ammo;
    private GameObject player;
    public float maxAmmo;
    private float currentAmmo;
    public float fireDelay;
    public float fireVelocity;
    private bool isFireDelayTimer;
    private float fireDelayTimer;
    public float knockback;
    public float explosionKnockback;

    public int bulletCount;
    private int currentBulletCount;
    public bool burst;
    public float burstShotDelay;
    private float burstShotTimer;
    private bool isBurst;
    public bool spread;
    public float spreadArc;
    public bool auto;

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
        BurstTimer();
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
        if(player != null)
        {
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, GetCurrentRatio());
        }
        GetComponent<SpriteRenderer>().enabled = true;
        StartFireDelayTimer();
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
    private bool CheckAutoShoot(bool pressedDown)
    {
        if(!pressedDown)
        {
            if(auto)
            {
                return true;
            }
            return false;
        }
        return true;
    }
    private void BurstTimer()
    {
        if(isBurst)
        {
            
            burstShotTimer -= Time.deltaTime;
            if(burstShotTimer <= 0)
            {
                
                if (currentBulletCount <= 0)
                {
                    isBurst = false;
                    StartFireDelayTimer();
                    if (currentAmmo <= 0)
                    {
                        StartReloading();
                    }
                }
                else
                {
                    ShootBullet();
                    burstShotTimer = burstShotDelay;
                }
            }    
        }
    }
    private void StartBurst()
    {
        isBurst = true;
        currentBulletCount = bulletCount;
        burstShotTimer = burstShotDelay;
        ShootBullet();
        
    }
    private void ShootBullet()
    {
        if(isBurst)
        {
            currentBulletCount--;
        }
        GameObject firedObject = Instantiate(ammo, GameObject.Find("ShotThings").transform);
        firedObject.GetComponent<AmmoScript>().SetKnockback(knockback, explosionKnockback);
        firedObject.transform.position = transform.position;
        firedObject.transform.rotation = transform.rotation;
        firedObject.GetComponent<Rigidbody2D>().velocity = firedObject.transform.right * fireVelocity;
        firedObject.GetComponent<AmmoScript>().AddSelfPlayer(player);
        currentAmmo--;
        float ratio = currentAmmo / maxAmmo;
        GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, ratio);
    }
    public void Use(GameObject player,bool pressedDown)
    {
        Shoot(player,pressedDown);
    }

    private void Shoot(GameObject currentPlayer,bool pressedDown)
    {
        player = currentPlayer;
        if(CheckCanShoot() && CheckAutoShoot(pressedDown))
        {
            

            
            

            if (burst)
            {
                StartBurst();
            }
            else if(spread)
            {
                for(int i = 1;i < bulletCount + 1;i++)
                {
                    GameObject firedObject = Instantiate(ammo, GameObject.Find("ShotThings").transform);
                    firedObject.GetComponent<AmmoScript>().SetKnockback(knockback,explosionKnockback);
                    firedObject.transform.position = transform.position;
                    firedObject.transform.rotation = transform.rotation;
                    float angle = ((spreadArc / bulletCount) * i) - (spreadArc / 2) - ((spreadArc / bulletCount) / 2);
                    firedObject.transform.Rotate(0, 0,angle);
                    firedObject.GetComponent<Rigidbody2D>().velocity = firedObject.transform.right * fireVelocity;
                    firedObject.GetComponent<AmmoScript>().AddSelfPlayer(player);
                }
                StartFireDelayTimer();
            }
            else
            {
                ShootBullet();
                StartFireDelayTimer();
            }
            if (currentAmmo <= 0)
            {
                StartReloading();
            }

        }
    }
}
