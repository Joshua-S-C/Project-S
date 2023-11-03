using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerScript : MonoBehaviour
{
    public GameObject scoreCardManager;
    public List<Color> playerColors = new List<Color>();
    public List<Vector2> playerStartPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerJoinedGame(GameObject player)
    {
        int index = player.GetComponent<PlayerInput>().playerIndex;
        player.GetComponent<SpriteRenderer>().color = playerColors[index];
        player.transform.position = playerStartPositions[index];
        scoreCardManager.GetComponent<ScoreboardManagerScript>().AddScoreCard(player);
    }
}
