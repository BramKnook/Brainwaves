using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public TMP_Text scoreText;

    private void Awake()
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

    private void Start()
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
    }
}
