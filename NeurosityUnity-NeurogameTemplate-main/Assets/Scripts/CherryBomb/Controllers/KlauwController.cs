using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KlauwController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveDistance = 2f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMovingDown = false;
    private bool isMovingUp = false;

    private ClawControls controls;
    [SerializeField]
    public ConveyorBeltController conveyorBeltController;

    void Awake()
    {
        controls = new ClawControls();
        controls.Claw.MoveClaw.performed += ctx => MoveClaw();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z);
    }

    void Update()
    {
        if (isMovingDown)
        {
            MoveClawDown();
        }
        else if (isMovingUp)
        {
            MoveClawUp();
        }
    }

    void MoveClaw()
    {
        if (!isMovingDown && !isMovingUp)
        {
            isMovingDown = true;
        }
    }

    void MoveClawDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMovingDown = false;
            isMovingUp = true;
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
