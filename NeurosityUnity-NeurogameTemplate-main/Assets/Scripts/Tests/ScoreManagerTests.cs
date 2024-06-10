using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.TestTools;

[TestFixture]
public class ScoreManagerTests
{
    private GameObject scoreManagerObject;
    private ScoreManager scoreManager;
    private TMP_Text scoreText;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject for the ScoreManager
        scoreManagerObject = new GameObject();
        scoreManager = scoreManagerObject.AddComponent<ScoreManager>();

        // Create a new GameObject for the TMP_Text
        var textObject = new GameObject();
        scoreText = textObject.AddComponent<TextMeshProUGUI>();

        // Assign the TMP_Text to the ScoreManager
        scoreManager.scoreText = scoreText;

        // Call Awake and Start methods manually
        scoreManager.Awake();
        scoreManager.Start();
    }

    [TearDown]
    public void TearDown()
    {
        // Destroy the created GameObjects after each test
        Object.DestroyImmediate(scoreManagerObject);
        Object.DestroyImmediate(scoreText.gameObject);
    }

    [Test]
    public void ScoreStartsAtZero()
    {
        // Assert that the initial score is zero
        Assert.Equals(0, scoreManager.score);
        Assert.Equals("Points: 0", scoreText.text);
    }

    [Test]
    public void AddScoreIncreasesScore()
    {
        // Add 10 to the score
        scoreManager.AddScore(10);

        // Assert that the score has increased
        Assert.Equals(10, scoreManager.score);
        Assert.Equals("Points: 10", scoreText.text);
    }

    [Test]
    public void AddScoreMultipleTimes()
    {
        // Add 5 to the score
        scoreManager.AddScore(5);
        // Add 15 to the score
        scoreManager.AddScore(15);

        // Assert that the score has increased correctly
        Assert.Equals(20, scoreManager.score);
        Assert.Equals("Points: 20", scoreText.text);
    }
}