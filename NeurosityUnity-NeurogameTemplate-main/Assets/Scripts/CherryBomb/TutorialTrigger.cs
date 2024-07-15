using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialMessage;
    private bool hasTriggered = false;
    private TutorialResume tutorialActions;

    private void Awake()
    {
        tutorialActions = new TutorialResume();
    }

    private void OnEnable()
    {
        tutorialActions.UI.Enable();
    }

    private void OnDisable()
    {
        tutorialActions.UI.Disable();
    }

    private void Start()
    {
        tutorialMessage.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the other object is a cherry, bomb, or strawberry
        if (!hasTriggered && (other.gameObject.CompareTag("Cherry") || other.gameObject.CompareTag("Bomb") || other.gameObject.CompareTag("Strawberry")))
        {
            hasTriggered = true;
            // Pause the game
            Time.timeScale = 0f;

            // Show the tutorial message
            ShowTutorialMessage();

            StartCoroutine(ResumeAndDestroy());
        }
    }

    void ShowTutorialMessage()
    {
        if (tutorialMessage != null)
        {
            tutorialMessage.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Tutorial message GameObject is not assigned.");
        }
    }

    IEnumerator ResumeAndDestroy()
    {
        // Wait for any key press
        bool anyKeyPressed = false;
        tutorialActions.UI.AnyKey.performed += ctx => anyKeyPressed = true;
        yield return new WaitUntil(() => anyKeyPressed);

        // Hide the tutorial message and resume the game
        HideTutorialMessage();

        // Destroy the trigger object so that the tutorial doesn't repeat
        Destroy(gameObject);
    }

    void HideTutorialMessage()
    {
        if (tutorialMessage != null)
        {
            tutorialMessage.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}



