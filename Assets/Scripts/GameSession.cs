using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        scoreText.text = score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int enemyKillPoint)
    {
        score += enemyKillPoint;
        scoreText.text = score.ToString();
    }

    public string GetScore()
    {
        return score.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
  

}
