using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private List<GameObject> objectsInRadius;
    private List<GameObject> hitObjects;
    private GameObject myPlayer;
    private Animator animator;

    //hitdelay determines how long after swing begins that sword does knockback
    public float hitDelay;
    private float hitDelayTimer;
    private bool isHitDelay;

    //hitwindow is how long during the swing the sword can deal knockback
    public float hitWindow;
    private float hitWindowTimer;
    private bool isHitWindow;

    public float swingDelay;
    private float swingDelayTimer;
    private bool isSwingDelay;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        objectsInRadius = new List<GameObject>();
        hitObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HitWindowTimerTick();
        HitDelayTimerTick();
        SwingDelayTimerTick();
    }
    public void Swing(GameObject player)
    {
        
        if (!isSwingDelay)
        {
            animator.SetTrigger("Swing");
            myPlayer = player;
            
            StartSwingDelayTimer();
            StartHitDelayTimer();
        }
        
    }
    private void StartSwingDelayTimer()
    {
        isSwingDelay = true;
        swingDelayTimer = swingDelay;
    }
    private void SwingDelayTimerTick()
    {
        if(isSwingDelay)
        {
            swingDelayTimer -= Time.deltaTime;
            if(swingDelayTimer <= 0)
            {
                isSwingDelay = false;
                hitObjects = new List<GameObject>();
            }
        }
    }
    private void StartHitDelayTimer()
    {
        hitDelayTimer = hitDelay;
        isHitDelay = true;
    }
    private void HitDelayTimerTick()
    {
        if(isHitDelay)
        {
            hitDelayTimer -= Time.deltaTime;
            if(hitDelayTimer <= 0)
            {
                isHitDelay = false;
                StartHitWindowTimer();
            }
        }
    }
    private void StartHitWindowTimer()
    {
        HitObjectsInRadius();
        hitWindowTimer = hitWindow;
        isHitWindow = true;
    }
    private void HitWindowTimerTick()
    {
        if(isHitWindow)
        {
            hitWindowTimer -= Time.deltaTime;
            if(hitWindowTimer <= 0)
            {
                isHitWindow = false;
            }
        }
    }
    private void HitObjectsInRadius()
    {
        for (int i = 0;i < objectsInRadius.Count;i++)
        {
            HitObject(objectsInRadius[i]);
        }
    }
    private void HitObject(GameObject newObject)
    {
        if(!hitObjects.Contains(newObject))
        {
            hitObjects.Add(newObject);
            newObject.GetComponent<PlayerScript>().PlayerHit();
            newObject.GetComponent<PlayerScript>().DisableMovement(0.25f);
            newObject.GetComponent<PlayerScript>().DealKnockback(transform.parent.right * GetComponent<WeaponScript>().GetKnockback());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != myPlayer)
        {
            if(isHitWindow)
            {
                HitObject(collision.gameObject);
            }
            else
            {
                objectsInRadius.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject != myPlayer)
        {
            objectsInRadius.Remove(collision.gameObject);
        }
    }
}
