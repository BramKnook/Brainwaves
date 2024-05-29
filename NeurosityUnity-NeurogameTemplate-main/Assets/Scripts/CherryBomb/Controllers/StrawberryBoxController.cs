using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class StrawberryBoxController : MonoBehaviour
{
    public Vector3 moveDistance = new Vector3(-1.5f, 0f, 0f);
    public float moveSpeed = 4f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private StrawberryBoxControls controls;

    void Awake()
    {
        controls = new StrawberryBoxControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Gameplay.MoveBox.performed += ctx => MoveBox();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + moveDistance;
    }

    private void MoveBox()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveBoxCoroutine());
        }
    }

    private IEnumerator MoveBoxCoroutine()
    {
        isMoving = true;

        // Move to the target position
        yield return MoveToPosition(targetPosition);

        // Wait for a moment if needed (optional)
        yield return new WaitForSeconds(0.8f);

        // Move back to the original position
        yield return MoveToPosition(originalPosition);

        isMoving = false;
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }
}