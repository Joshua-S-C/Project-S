using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DashCooldownDisplayScript : MonoBehaviour
{
    private GameObject cooldownMask;
    float originalHeight;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Setup()
    {
        cooldownMask = transform.Find("CooldownMask").gameObject;
        originalHeight = cooldownMask.GetComponent<RectTransform>().sizeDelta.y;
        cooldownMask.GetComponent<RectTransform>().sizeDelta = new Vector2(cooldownMask.GetComponent<RectTransform>().sizeDelta.x, 0);
    }
    public void UpdateDisplay(float ratio)
    {
        
        RectTransform myTransform = cooldownMask.GetComponent<RectTransform>();
        myTransform.sizeDelta = new Vector2(myTransform.sizeDelta.x, originalHeight * ratio);
        Debug.Log(originalHeight * ratio);
    }
}
