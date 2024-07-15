using UnityEngine;
using TMPro;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;
    [SerializeField]
    public int pointsToNextLevel = 3;
    [SerializeField]
    public float speedIncreasePerLevel = 3f;
    [SerializeField]
    public TMP_Text levelText;
    [SerializeField]
    public TMP_Text levelUpText;
    [SerializeField]
    public float pauseDuration = 2f;  

    private ConveyorBeltController conveyorBeltController;

    void Start()
    {
        ScoreManager.OnScoreReached += CheckLevelUp;
        conveyorBeltController = FindObjectOfType<ConveyorBeltController>();
        UpdateLevelText();
        levelUpText.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        ScoreManager.OnScoreReached -= CheckLevelUp;
    }

    void CheckLevelUp(int currentScore)
    {
        if (currentScore >= pointsToNextLevel * currentLevel)
        {
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        currentLevel++;
        Debug.Log("Level Up! Current Level: " + currentLevel);

        levelUpText.gameObject.SetActive(true);

        yield return new WaitForSeconds(pauseDuration);

        levelUpText.gameObject.SetActive(false);

        // Increase difficulty of the game here
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
