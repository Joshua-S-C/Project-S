using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCardScript : MonoBehaviour
{
    private List<GameObject> lifeBubbles;
    private GameObject deathMask;
    private GameObject DashCooldownDisplay;
    private bool playerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        GetLifeBubbles();
        deathMask = transform.Find("DeathMask").gameObject;
        DashCooldownDisplay = transform.Find("PlayerDashCooldown").gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScoreCardLives(int lives)
    {
       
        if(lives >= 0)
        {
            Destroy(lifeBubbles[lifeBubbles.Count - 1]);
            lifeBubbles.RemoveAt(lifeBubbles.Count - 1);
            if(lives == 0)
            {
                deathMask.SetActive(true);
                playerAlive = false;
            }
        }
        
    }
    public void UpdateScoreCardCooldowns(float dashDisplayRatio)
    {
        DashCooldownDisplay.GetComponent<DashCooldownDisplayScript>().UpdateDisplay(dashDisplayRatio);
    }
    public bool GetIsAlive()
    {
        return playerAlive;
    }
    private void GetLifeBubbles()
    {
        lifeBubbles = new List<GameObject>();
        GameObject bubbleParent = transform.Find("LifeBubbles").gameObject;
        lifeBubbles.Add(bubbleParent.transform.GetChild(0).gameObject);
        lifeBubbles.Add(bubbleParent.transform.GetChild(1).gameObject);
        lifeBubbles.Add(bubbleParent.transform.GetChild(2).gameObject);
    }
}