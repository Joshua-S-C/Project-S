using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJoinedMessageScript : MonoBehaviour
{
    public float waitTime;
    public float decayTime;
    private bool isDecaying;
    private bool isWaiting;
    private float waitTimer;
    private float decayTimer;
    // Start is called before the first frame update
    void Start()
    {
        isWaiting = true;
        isDecaying = false;
        waitTimer = waitTime;
        decayTimer = decayTime;
    }

    // Update is called once per frame
    void Update()
    {
        Wait();
        Decay();
    }
    private void Wait()
    {
        if(isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if(waitTimer <= 0)
            {
                isDecaying = true;
                isWaiting = false;
            }
        }
    }
    private void Decay()
    {
        if(isDecaying)
        {
            Color currentColor;
            decayTimer -= Time.deltaTime;
            currentColor = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(currentColor.r, currentColor.g, currentColor.b, decayTimer / decayTime);
            currentColor = transform.Find("PlayerJoinedText").GetComponent<TextMeshProUGUI>().color;
            transform.Find("PlayerJoinedText").GetComponent<TextMeshProUGUI>().color = new Color(currentColor.r, currentColor.g, currentColor.b, decayTimer / decayTime);
            if(decayTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
