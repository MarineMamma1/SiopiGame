using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum Axis { X, Y, Z } // Choose the axis to move on
    public Axis moveAxis = Axis.X; // Default to X-axis
    public float moveDistance = 5f; // Distance to move in each direction
    public float moveSpeed = 2f; // Speed of the platform
    public float delayTime = 2f; // Delay time before reversing direction

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;

    void Start()
    {
        startPosition = transform.position;

        // Set target position based on chosen axis
        switch (moveAxis)
        {
            case Axis.X:
                targetPosition = startPosition + new Vector3(moveDistance, 0, 0);
                break;
            case Axis.Y:
                targetPosition = startPosition + new Vector3(0, moveDistance, 0);
                break;
            case Axis.Z:
                targetPosition = startPosition + new Vector3(0, 0, moveDistance);
                break;
        }

        // Start moving the platform
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            Vector3 destination = movingToTarget ? targetPosition : startPosition;
            while (Vector3.Distance(transform.position, destination) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Toggle direction
            movingToTarget = !movingToTarget;

            // Wait for the delay time
            yield return new WaitForSeconds(delayTime);
        }
    }
}