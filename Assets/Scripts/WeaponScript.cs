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

    public float reloadTime;
    private float reloadTimer;
    private bool isReloading;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.root.gameObject;
        currentAmmo = maxAmmo;
    }
    private void OnEnable()
    {
        
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
    private void ReloadTimerTick()
    {
        if(isReloading)
        {

            reloadTimer -= Time.deltaTime;
            float ratio = reloadTimer / reloadTime;
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, ratio);
            if (reloadTimer <= 0)
            {
                isReloading = false;
                currentAmmo = maxAmmo;
            }
        }
    }
    private bool CheckCanShoot()
    {
        if(isFireDelayTimer && !isReloading)
        {
            return false;
        }
        return true;
    }
    public void Shoot(GameObject player)
    {
        if(CheckCanShoot())
        {
            currentAmmo--;
            if(currentAmmo <= 0)
            {
                isReloading = true;
            }
            float ratio = currentAmmo / maxAmmo;
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardAmmoDisplay(player, (int)currentAmmo, (int)maxAmmo, ratio);
            StartFireDelayTimer();
            GameObject firedObject = Instantiate(ammo,GameObject.Find("ShotThings").transform);
            firedObject.transform.position = transform.position;
            firedObject.transform.rotation = transform.rotation;
            firedObject.GetComponent<Rigidbody2D>().velocity = firedObject.transform.right * fireVelocity;
            firedObject.GetComponent<AmmoScript>().AddSelfPlayer(player);

        }
    }
}
