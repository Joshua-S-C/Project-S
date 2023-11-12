using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShieldScript : MonoBehaviour
{
    public GameObject shield;
    private bool isShielding;
    public float shieldDuration;
    private float currentShieldDuration;
    public float shieldRegenRate;
    public float shieldUseCooldown;
    private float ShieldUseCooldownTimer;
    private bool canShield = true;
    // Start is called before the first frame update
    void Start()
    {
        currentShieldDuration = shieldDuration;
        GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardShieldCooldown(transform.root.gameObject, CalculateShieldRatio());
    }

    // Update is called once per frame
    void Update()
    {
        RegenShield();
        DrainShield();
        ShieldUseCooldownTimerTick();
    }
    private void RegenShield()
    {
        if(currentShieldDuration < shieldDuration && !isShielding)
        {
            currentShieldDuration += Time.deltaTime * shieldRegenRate;
            if(currentShieldDuration > shieldDuration)
            {
                currentShieldDuration = shieldDuration;
            }
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardShieldCooldown(transform.root.gameObject,CalculateShieldRatio());
        }
    }
    private void DrainShield()
    {
        if(isShielding)
        {
            currentShieldDuration -= Time.deltaTime;

            if(currentShieldDuration < 0)
            {
                currentShieldDuration = 0;
                StopShielding();
            }
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardShieldCooldown(transform.root.gameObject,CalculateShieldRatio());
        }
    }
    private void StartShieldUseCooldownTimer()
    {
        canShield = false;
        ShieldUseCooldownTimer = shieldUseCooldown;
    }
    private void ShieldUseCooldownTimerTick()
    {
        if(!canShield)
        {
            Debug.Log(ShieldUseCooldownTimer);
            ShieldUseCooldownTimer -= Time.deltaTime;
            if(ShieldUseCooldownTimer <= 0)
            {
                canShield = true;
            }
        }
    }
    public void StartShielding()
    {
        if(currentShieldDuration > 0 && canShield)
        {
            transform.root.GetComponent<PlayerScript>().DisableEverything();
            isShielding = true;
            shield.SetActive(true);
        }
    }
    public void StopShielding()
    {
        if(isShielding)
        {
            transform.root.GetComponent<PlayerScript>().EnableEverything();
            isShielding = false;
            shield.SetActive(false);
            StartShieldUseCooldownTimer();
        }
        
    }
    private float CalculateShieldRatio()
    {
        float ratio = currentShieldDuration / shieldDuration;
        return ratio;
    }
    public bool GetIsShielding()
    {
        return isShielding;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "projectile" && isShielding)
        {
            Debug.Log("hit");
            Destroy(collision.gameObject);
        }
    }

}
