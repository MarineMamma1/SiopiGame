using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public List<Vector3> relativePositions = new List<Vector3>();
    public float duration = 2.0f;

    private Vector3 originalPosition;
    private Rigidbody2D rb;
    private Vector3 lastPosition;
    public Vector3 velocity;
    private bool isMoving = false; // Flag to control the movement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        originalPosition = transform.position;
        lastPosition = transform.position;
    }

    public void PerformAction()
    {
        if (!isMoving) // Ensure that it only starts moving if it is not already moving
        {
            StartCoroutine(MoveThroughPoints());
        }
    }

    IEnumerator MoveThroughPoints()
    {
        isMoving = true; // Set the moving flag to true
        List<Vector3> absolutePositions = new List<Vector3>();

        foreach (Vector3 pos in relativePositions)
        {
            absolutePositions.Add(originalPosition + pos);
        }

        // Move forward through the list of absolute positions
        foreach (Vector3 pos in absolutePositions)
        {
            yield return MoveOverTime(pos, duration);
        }

        // Move backward through the list
        for (int i = absolutePositions.Count - 1; i >= 0; i--)
        {
            yield return MoveOverTime(absolutePositions[i], duration);
        }

        // Return to the original position last
        yield return MoveOverTime(originalPosition, duration);

        isMoving = false; // Reset the moving flag
    }

    IEnumerator MoveOverTime(Vector3 target, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, target, time / duration);
            rb.MovePosition(newPosition);
            velocity = (newPosition - lastPosition) / Time.deltaTime;
            lastPosition = newPosition;
            time += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(target);
        velocity = (target - lastPosition) / Time.deltaTime;
        lastPosition = target;
    }
}
