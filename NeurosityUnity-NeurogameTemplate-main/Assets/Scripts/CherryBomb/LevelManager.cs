using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int pointsToNextLevel = 10;
    [SerializeField]
    public float speedIncreasePerLevel = 1f; // Amount to increase speed per level

    private ConveyorBeltController conveyorBeltController;

    void Start()
    {
        ScoreManager.OnScoreReached += CheckLevelUp;
        conveyorBeltController = FindObjectOfType<ConveyorBeltController>();
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

        ScoreManager.instance.ResetScore();
    }
}
