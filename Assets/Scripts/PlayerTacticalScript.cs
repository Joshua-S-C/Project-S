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
    private float throwDelay;
    private float throwDelayTimer;
    private bool isThrowDelay;

    public List<GameObject> tacticalList;
    // Start is called before the first frame update
    void Start()
    {
        RandomTactical();
        if(tacticalPrefab != null)
        {
            maxTacticalCount = tacticalPrefab.GetComponent<TacticalScript>().GetTacticalCount();
            currentTacticalCount = maxTacticalCount;
            tacticalCooldown = tacticalPrefab.GetComponent<TacticalScript>().GetTacticalCooldown();
            throwDelay = tacticalPrefab.GetComponent<TacticalScript>().GetThrowDelay();
            GameObject.Find("ScoreboardManager").GetComponent<ScoreboardManagerScript>().UpdateScoreCardTacticalDisplay(gameObject,currentTacticalCount);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        TacticalRegen();
        ThrowDelayTimerTick();
    }
    private void RandomTactical()
    {
        tacticalPrefab = tacticalList[Random.Range(0, tacticalList.Count)];
    }
    private void ThrowDelayTimerTick()
    {
        if(isThrowDelay)
        {
            throwDelayTimer -= Time.deltaTime;
            if(throwDelayTimer <= 0)
            {
                isThrowDelay = false;
            }
        }
    }
    private void StartThrowDelay()
    {
        throwDelayTimer = throwDelay;
        isThrowDelay = true;
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
        if (currentTacticalCount > 0 && !isThrowDelay)
        {
            StartThrowDelay();
            currentTacticalCount--;
            thrownTactical = Instantiate(tacticalPrefab, GameObject.Find("ShotThings").transform);
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
            if (thrownTactical != null && thrownTactical.GetComponent<TacticalScript>().GetTriggerable())
            {
                thrownTactical.GetComponent<TacticalScript>().TriggerTactical();
            }
            else
            {
                ThrowTactical();
            }
            
            
        }
    }
}
