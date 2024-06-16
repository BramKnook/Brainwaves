using Notion.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 0.8f;
    public float kinesisValueThreshold = 0.8f;
    public float kinesisValue;
    [SerializeField]
    public TMP_Text kinesisText;
    public float unchangedThresholdTime = 2f; 

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMovingDown = false;
    private bool isMovingUp = false;

    [SerializeField]
    public ConveyorBeltController conveyorBeltController;
    private NotionInterfacer deviceInterface;
    private MockDevice mockDevice;

    private float lastKinesisValue;
    private float unchangedTime = 0f;

// Enable the Neurosity Crown OR mock version
    void OnEnable()
    {
//      mockDevice = FindObjectOfType<MockDevice>();

        if (mockDevice == null)
        {
            deviceInterface = FindObjectOfType<NotionInterfacer>();
        }
    }

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z);
        lastKinesisValue = -1f; // Initial invalid value to ensure it gets updated
    }

    void Update()
    {
        CheckKinesisValue();
        UpdateKinesisText();
        MoveClaw();
    }

    void CheckKinesisValue()
    {
          if (deviceInterface == null || !deviceInterface.IsOnline())
          {
              return;
          }
          kinesisValue = deviceInterface.kinesisScore;

        // test code for prototype/mock device
        /*   if (mockDevice != null)
           {
               kinesisValue = mockDevice.GetValue();
           }
           else if (deviceInterface != null && deviceInterface.IsOnline())
           {
               kinesisValue = deviceInterface.kinesisScore;
           }
           else
           {
               return;
           }*/

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
    }

    void MoveClaw()
    {
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
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddScore(-1);
            }
        }
        Destroy(collision.gameObject);
        conveyorBeltController.onBelt.Remove(collision.gameObject);
    }

    void UpdateKinesisText()
    {
        if (kinesisText != null)
        {
            kinesisText.text = "Claw: " + kinesisValue;
        }
    }
}
