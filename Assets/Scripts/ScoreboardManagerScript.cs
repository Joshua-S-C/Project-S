using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManagerScript : MonoBehaviour
{
    public GameObject scoreCardPrefab;
    private List<ScoreCard> scoreCards;
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
        if(scoreCards.Count < 4)
        {
            ScoreCard newCard = new ScoreCard(Instantiate(scoreCardPrefab, transform), player);
            scoreCards.Add(newCard);
            if (scoreCards.Count == 1)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -160);
            }
            if (scoreCards.Count == 2)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, -160);
                scoreCards[1].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(60, -160);
            }
            if (scoreCards.Count == 3)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, -160);
                scoreCards[1].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -160);
                scoreCards[2].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(120, -160);
            }
            if (scoreCards.Count == 3)
            {
                scoreCards[0].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-180, -160);
                scoreCards[1].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(-60, -160);
                scoreCards[2].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(60, -160);
                scoreCards[3].GetScoreCard().GetComponent<RectTransform>().anchoredPosition = new Vector2(180, -160);
            }

        }
        
    }
    public void UpdateScoreCard(GameObject player,int lives)
    {
        ScoreCard card = GetScoreCard(player);
        if(card != null)
        {
            card.GetScoreCard().GetComponent<ScoreCardScript>().UpdateScoreCard(lives);
        }


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
}