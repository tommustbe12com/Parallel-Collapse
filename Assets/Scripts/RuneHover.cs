using UnityEngine;

public class RuneHover : MonoBehaviour
{
    public float hoverSpeed = 2f;  // How fast it moves
    public float hoverAmount = 0.1f; // How much it moves up and down
    public float rotationSpeed = 30f; // Rotation speed

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Make it float up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Slowly rotate
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
