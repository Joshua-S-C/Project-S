using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalDisplayScript : MonoBehaviour
{
    public List<GameObject> tacticalBubbles;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTacticalDisplay(int tacticalCount)
    {
        for(int i = 0;i < tacticalCount;i++)
        {
            tacticalBubbles[i].SetActive(true);
        }
        for(int i = tacticalCount;i < tacticalBubbles.Count;i++)
        {
            tacticalBubbles[i].SetActive(false);
        }
    }
}
