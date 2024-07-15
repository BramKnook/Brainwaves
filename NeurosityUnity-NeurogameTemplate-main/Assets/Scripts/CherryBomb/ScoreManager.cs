using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    [SerializeField]
    public TMP_Text scoreText;


    public delegate void ScoreReached(int currentScore);

    public static event ScoreReached OnScoreReached;

    // Audio fields
    [SerializeField]
    private AudioClip positiveSound;
    [SerializeField]
    private AudioClip negativeSound;
    [SerializeField]
    private AudioClip levelUpSound;
    private AudioSource audioSource;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        int previousScore = score;
        score += value;
        UpdateScoreText();
        OnScoreReached?.Invoke(score);

        // Play sound based on score change
        if (value > 0)
        {
            PlaySound(positiveSound);
        }
        else if (value < 0)
        {
            PlaySound(negativeSound);
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
        PlaySound(levelUpSound);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score.ToString();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

