using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreboardManagerScript : MonoBehaviour
{
    public GameObject scoreCardPrefab;
    private List<ScoreCard> scoreCards;
    public float scoreCardYPosition;
    private int playersAlive;
    // Start is called before the first frame update
    void Start()
    {
        scoreCards = new List<ScoreCard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private ScoreCard GetScoreCard(GameObject player)
    {
        for(int i = 0; i < scoreCards.Count; i++)
        {
            if (scoreCards[i].GetPlayer() == player)
            {
                return scoreCards[i];
            }
        }
        return null;
    }
    public void AddScoreCard(GameObject player)
    {
        playersAlive++;
        if(scoreCards.Count < 4)
        {
            ScoreCard newCard = new ScoreCard(Instantiate(scoreCardPrefab, transform), player);
            scoreCards.Add(newCard);
            if (scoreCards.Count == 1)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, scoreCardYPosition);
            }
            if (scoreCards.Count == 2)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, scoreCardYPosition);
                scoreCards[1].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(60, scoreCardYPosition);
            }
            if (scoreCards.Count == 3)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, scoreCardYPosition);
                scoreCards[1].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, scoreCardYPosition);
                scoreCards[2].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(120, scoreCardYPosition);
            }
            if (scoreCards.Count == 3)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-180, scoreCardYPosition);
                scoreCards[1].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, scoreCardYPosition);
                scoreCards[2].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(60, scoreCardYPosition);
                scoreCards[3].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(180, scoreCardYPosition);
            }

        }
        
    }
    public void UpdateScoreCard(GameObject player,int lives)
    {
        ScoreCard card = GetScoreCard(player);
        if(card != null)
        {
            card.GetScoreCard().GetComponent<ScoreCardScript>().UpdateScoreCard(lives);
            if(lives == 0)
            {
                playersAlive--;
                if(playersAlive == 1)
                {
                    GameObject alivePlayer = GetFirstAlivePlayer();
                    EndScreenScript.EndScreen(Instantiate(alivePlayer),alivePlayer.GetComponent<PlayerInput>().playerIndex);
                }
            }
        }


    }
    private GameObject GetFirstAlivePlayer()
    {
        for(int i = 0;i < scoreCards.Count;i++)
        {
            if (scoreCards[i].GetIsAlive())
            {
                return scoreCards[i].GetPlayer();
            }
        }
        return null;
    }
}



public class ScoreCard
{
    private GameObject scoreCard;
    private GameObject player;

    public ScoreCard(GameObject scoreCard, GameObject player)
    {
        this.scoreCard = scoreCard;
        this.player = player;
    }
    public GameObject GetScoreCard()
    {
        return scoreCard;
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    public bool GetIsAlive()
    {
        return scoreCard.GetComponent<ScoreCardScript>().GetIsAlive();
    }
}