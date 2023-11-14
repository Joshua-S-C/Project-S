using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public GameObject winnerText;
    private static GameObject winnerPlayerPrefab;
    private static int winnerPlayerIndex;
    public float endScreenDuration;
    private float endScreenTimer;
    // Start is called before the first frame update
    void Start()
    {

        InitializeEndScreen();
    }

    // Update is called once per frame
    void Update()
    {
        EndScreenTimerTick();
    }
    private void InitializeEndScreen()
    {
        endScreenTimer = endScreenDuration;
        PlayerProfile myProfile = PlayerProfileManagerScript.GetProfile(winnerPlayerIndex);
        winnerText.GetComponent<TextMeshProUGUI>().text = myProfile.GetName() + " Wins!";
        //GameObject player = Instantiate(winnerPlayerPrefab);
        //player.transform.localScale = new Vector2(10, 10);
    }
    private void EndScreenTimerTick()
    {
        endScreenTimer -= Time.deltaTime;
        if(endScreenTimer <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    public static void EndScreen(int index)
    {
        winnerPlayerIndex = index;
        SceneManager.LoadScene("EndScreen");
    }
}
