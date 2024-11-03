using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum Axis { X, Y, Z } // This allows Axis choosing
    public Axis moveAxis = Axis.X; 
    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    public float delayTime = 2f; 

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;

    void Start()
    {
        startPosition = transform.position;

    
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

           
            movingToTarget = !movingToTarget;

            
            yield return new WaitForSeconds(delayTime);
        }
    }
}