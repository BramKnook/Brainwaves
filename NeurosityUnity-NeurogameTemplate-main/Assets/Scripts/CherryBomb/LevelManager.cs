using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    [SerializeField]
    public int pointsToNextLevel = 3;
    [SerializeField]
    public float speedIncreasePerLevel = 3f; // Amount to increase speed per level
    [SerializeField]
    public TMP_Text levelText;

    private ConveyorBeltController conveyorBeltController;

    void Start()
    {
        ScoreManager.OnScoreReached += CheckLevelUp;
        conveyorBeltController = FindObjectOfType<ConveyorBeltController>();
        UpdateLevelText();
    }

    void OnDestroy()
    {
        ScoreManager.OnScoreReached -= CheckLevelUp;
    }

    void CheckLevelUp(int currentScore)
    {
        if (currentScore >= pointsToNextLevel * currentLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        // Increase difficulty here
        Debug.Log("Level Up! Current Level: " + currentLevel);

        if (conveyorBeltController != null)
        {
            conveyorBeltController.speed += speedIncreasePerLevel;
            Debug.Log("New Conveyor Belt Speed: " + conveyorBeltController.speed);
        }
        UpdateLevelText();
        ScoreManager.instance.ResetScore();
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel;
        }
    }
}
