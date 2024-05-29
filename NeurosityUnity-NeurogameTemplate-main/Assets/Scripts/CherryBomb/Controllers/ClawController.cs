using Notion.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 0.8f;
    public float kinesisValueThreshold = 0.8f;
    public float kinesisValue;
    public float unchangedThresholdTime = 2f; // Time in seconds to consider value as unchanged

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMovingDown = false;
    private bool isMovingUp = false;
    [SerializeField]
    public ConveyorBeltController conveyorBeltController;
    private NotionInterfacer deviceInterface;

    private float lastKinesisValue;
    private float unchangedTime = 0f;

    void OnEnable()
    {

        deviceInterface = FindObjectOfType<NotionInterfacer>();
    }



    void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z);
        lastKinesisValue = -1f; // Initial invalid value to ensure it gets updated
    }

    void Update()
    {
       if (deviceInterface == null || !deviceInterface.IsOnline())
       {
            return;
       }

        kinesisValue = deviceInterface.kinesisScore;
        Debug.Log(kinesisValue);

        // Check if the kinesis value has changed
        if (kinesisValue == lastKinesisValue)
        {
            unchangedTime += Time.deltaTime;
        }
        else
        {
            unchangedTime = 0f;
            lastKinesisValue = kinesisValue;
        }

        // Move the claw up if the kinesis value is unchanged for a specific time
        if (unchangedTime >= unchangedThresholdTime && !isMovingUp)
        {
            isMovingDown = false;
            isMovingUp = true;
        }

        if (kinesisValue >= kinesisValueThreshold && !isMovingDown && !isMovingUp)
        {
            isMovingDown = true;
        }
        else if (kinesisValue < kinesisValueThreshold && !isMovingUp && !isMovingDown)
        {
            isMovingUp = true;
        }

        if (isMovingDown)
        {
            MoveClawDown();
        }
        else if (isMovingUp)
        {
            MoveClawUp();
        }
    }

    void MoveClawDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMovingDown = false;
        }
    }

    void MoveClawUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

        if (transform.position == originalPosition)
        {
            isMovingUp = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Cherry"))
        {
            // Subtract 1 point for removing a cherry
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(-1);
            }
        }

        // Destroy the object whether it's a cherry or a bomb
        Destroy(collision.gameObject);
        conveyorBeltController.onBelt.Remove(collision.gameObject);
    }
}
