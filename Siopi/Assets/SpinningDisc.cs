using UnityEngine;

public class SpinningDisc : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float bobSpeed = 1f;
    public float bobHeight = 0.5f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Spin the coin around the Z-axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Bob the coin up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}