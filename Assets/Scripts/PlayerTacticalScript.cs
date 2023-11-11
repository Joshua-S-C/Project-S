using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTacticalScript : MonoBehaviour
{
    public GameObject tacticalPrefab;
    private GameObject thrownTactical;
    private int maxTacticalCount;
    private int currentTacticalCount;
    private float tacticalCooldown;
    private float tacticalCooldownTimer;
    private bool cooldownStarted;
    // Start is called before the first frame update
    void Start()
    {
        if(tacticalPrefab != null)
        {
            maxTacticalCount = tacticalPrefab.GetComponent<TacticalScript>().GetTacticalCount();
            currentTacticalCount = maxTacticalCount;
            tacticalCooldown = tacticalPrefab.GetComponent<TacticalScript>().GetTacticalCooldown();
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardTacticalDisplay(gameObject,currentTacticalCount);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        TacticalRegen();
    }
    private void TacticalRegen()
    {
        if(cooldownStarted)
        {
            tacticalCooldownTimer -= Time.deltaTime;
            if(tacticalCooldownTimer <= 0)
            {
                cooldownStarted = false;
                currentTacticalCount++;
                GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardTacticalDisplay(gameObject, currentTacticalCount);
                if (currentTacticalCount < maxTacticalCount)
                {
                    StartCooldown();
                }
            }
        }
    }
    private void StartCooldown()
    {
        if (!cooldownStarted)
        {
            tacticalCooldownTimer = tacticalCooldown;
            cooldownStarted = true;
        }
    }
    
    public void ThrowTactical()
    {
        if(currentTacticalCount > 0)
        {
            currentTacticalCount--;
            GameObject thrownTactical = Instantiate(tacticalPrefab, GameObject.Find("ShotThings").transform);
            thrownTactical.GetComponent<TacticalScript>().SetThrowPlayer(gameObject);
            thrownTactical.transform.position = transform.position;
            thrownTactical.GetComponent<Rigidbody2D>().velocity = transform.Find("WeaponOrigin").right * thrownTactical.GetComponent<TacticalScript>().GetThrowSpeed();
            StartCooldown();
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardTacticalDisplay(gameObject, currentTacticalCount);
        }
        
    }
    public void ThrowTacticalPressed(InputAction.CallbackContext context)
    {
        if (context.performed && tacticalPrefab != null)
        {
            if (thrownTactical != null)
            {
                thrownTactical.GetComponent<TacticalScript>().TriggerTactical();
            }
            ThrowTactical();
        }
    }
}
