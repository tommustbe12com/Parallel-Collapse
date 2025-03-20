using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    public float floatStrength = 0.5f;  // How high the object floats
    public float floatSpeed = 2f;      // Speed of floating
    public float rotationSpeed = 30f;  // Rotation speed

    private Vector3 startPosition;

    private void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    private void Update()
    {
        // Make the object float up and down using a sine wave
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = new Vector3(startPosition.x, startPosition.y + newY, startPosition.z);

        // Rotate the object gently
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
