using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    // Define a delegate type
    public delegate void ScoreReached(int currentScore);

    // Declare an event using the delegate
    public static event ScoreReached OnScoreReached;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score.ToString();
        }
    }

    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score.ToString();
        }
        OnScoreReached?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score.ToString();
        }
    }
}
