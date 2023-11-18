using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerScript : MonoBehaviour
{
    public GameObject playerJoinedMessage;
    private List<GameObject> playerJoinedMessages;
    public GameObject scoreCardManager;
    public List<Color> playerColors = new List<Color>();
    public List<Vector2> playerStartPositions = new List<Vector2>();
    public bool stage;
    // Start is called before the first frame update
    void Start()
    {
        playerJoinedMessages = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerJoinedGame(GameObject player)
    {
        if (stage)
        {
            int index = player.GetComponent<PlayerInput>().playerIndex;
            player.GetComponent<SpriteRenderer>().color = playerColors[index];
            player.transform.position = playerStartPositions[index];
            scoreCardManager.GetComponent<ScoreboardManagerScript>().AddScoreCard(player);
        }
        
    }
    public void NewPlayerJoinedGame(int index)
    {
        GameObject message = Instantiate(playerJoinedMessage,GameObject.Find("Canvas").transform);
        playerJoinedMessages.Add(message);
        GameObject messageText = message.transform.Find("PlayerJoinedText").gameObject;
        messageText.GetComponent<TextMeshProUGUI>().text = "Player " + (index + 1) + " Joined";
        FormatPlayerJoinedMessages();
    }
    private void FormatPlayerJoinedMessages()
    {
        for(int j = 0;j < playerJoinedMessages.Count;j++)
        {
            if (playerJoinedMessages[j] == null)
            {
                playerJoinedMessages.RemoveAt(j);
            }
        }
        for(int i = playerJoinedMessages.Count - 1;i >= 0; i--)
        {
            playerJoinedMessages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(340 - (((playerJoinedMessages.Count- 1) - i) * 80), 190);
        }
    }
}
