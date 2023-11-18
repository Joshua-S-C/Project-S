using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughPlayersTextScript : MonoBehaviour
{
    public float fadeTime;
    private float fadeTimer;
    // Start is called before the first frame update
    void Start()
    {
        RestartFadeTimer();
    }

    // Update is called once per frame
    void Update()
    {
        FadeTimerTick();
    }
    public void RestartFadeTimer()
    {
        fadeTimer = fadeTime;
    }
    private void FadeTimerTick()
    {
        fadeTimer -= Time.deltaTime;
        Color currentColor = GetComponent<TextMeshProUGUI>().color;
        GetComponent<TextMeshProUGUI>().color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeTimer / fadeTime);
        if (fadeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
