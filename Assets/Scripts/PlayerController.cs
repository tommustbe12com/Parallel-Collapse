using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;          // Camera attached to the player
    public Vector3 thirdPersonOffset;    // Third-person camera offset (behind the player)
    public Vector3 firstPersonOffset;    // First-person camera offset (in front of the player)
    private bool isFirstPerson = true;    // Default to first-person view
    private float mouseX;                 // X-axis mouse input for rotation
    private float mouseY;                 // Y-axis mouse input for rotation
    private float rotationX = 0f;         // Current vertical rotation of the camera (pitch)
    private float rotationY = 0f;         // Current horizontal rotation of the player (yaw)
    public float sensitivity = 2f;        // Mouse sensitivity for look movement
    public float moveSpeed = 5f;          // Movement speed for WASD
    public float jumpHeight = 2f;         // Height of the jump (adjust this for realism)
    public float gravity = -9.81f;       // Gravity force, can be adjusted

    private Rigidbody rb;                 // Rigidbody for the player
    private bool isGrounded;              // Check if the player is on the ground
    private float groundCheckDistance = 0.3f; // Distance for ground check

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get Rigidbody component
        rb.freezeRotation = true;       // Prevent automatic rotation from the Rigidbody

        // Lock the cursor and hide it for FPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Toggle between first-person and third-person on pressing 'O'
        if (Input.GetKeyDown(KeyCode.O))
        {
            isFirstPerson = !isFirstPerson;
        }

        // Get mouse movement for rotation
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate camera vertically and horizontally (pitch and yaw)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -60f, 60f); // Prevent the camera from flipping

        rotationY += mouseX;

        // Apply rotations: Camera pitch (up/down) and player yaw (left/right)
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);  // Camera pitch (up/down)
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);  // Player yaw (left/right)

        // Switch camera position between first-person and third-person
        if (isFirstPerson)
        {
            playerCamera.transform.position = transform.position + firstPersonOffset;
            playerCamera.transform.rotation = transform.rotation;
        }
        else
        {
            Vector3 thirdPersonPosition = transform.position - thirdPersonOffset;
            thirdPersonPosition = transform.position - (transform.forward * thirdPersonOffset.z) + (transform.up * thirdPersonOffset.y);
            playerCamera.transform.position = thirdPersonPosition;
            playerCamera.transform.LookAt(transform.position); // Always look at the player
        }

        // Handle movement input (WASD)
        float moveForward = Input.GetAxis("Vertical");  // W/S (forward/backward)
        float moveSide = Input.GetAxis("Horizontal");   // A/D (left/right)

        // Calculate the direction the player should move in (relative to the player's rotation)
        Vector3 moveDirection = (transform.forward * moveForward) + (transform.right * moveSide);
        moveDirection.y = rb.velocity.y; // Keep the vertical velocity intact for jumping and gravity

        // Apply movement to the Rigidbody's velocity
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        // Jumping logic: Check if player presses space and is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jumpHeight * -2f * gravity), rb.velocity.z);
        }

        // Check if the player is grounded (just below the player's position)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
