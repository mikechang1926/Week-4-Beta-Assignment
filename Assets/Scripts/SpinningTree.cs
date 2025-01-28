using UnityEngine;

public class SpinningTree : MonoBehaviour
{
    public float spinSpeed = 50f;
    public float speedChangeRate = 2f;
    private float currentSpinSpeed = 0f; // The current speed of rotation
    private bool reversing = false; // Whether the spin direction is reversing
    public float maxHoverHeight = 5f; // Maximum hover height
    public float hoverSpeed = 2f; // Speed of the up-and-down oscillation
    private float currentHoverHeight = 0f; // The current height based on spin speed
    private float hoverOscillationAmplitude = 0.2f; // Amplitude of the up-and-down motion
    private float hoverBaseHeight = 0f; // The starting height of the object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpinSpeed = spinSpeed;
        hoverBaseHeight = transform.position.y; // Record the starting height of the object

    }

    // Update is called once per frame
    void Update()
    {
        // Handle input for speeding up or slowing down
        HandleInput();

        // Rotate the object
        RotateObject();
        // Update hover position
        UpdateHover();
    }
    private void HandleInput()
    {
        // Left mouse button: Increase speed
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            currentSpinSpeed += speedChangeRate;
            reversing = false; // Ensure direction stays as-is
        }

        // Right mouse button: Decrease speed
        if (Input.GetMouseButtonDown(1)) // 1 is the right mouse button
        {
            currentSpinSpeed -= speedChangeRate;
            if (currentSpinSpeed <= 0f)
            {
                reversing = true; // Trigger reversal if speed goes below or equal to 0
            }
            Debug.Log("Current spin speed: " + currentSpinSpeed);
        }
    }
    private void RotateObject()
    {
        // Apply rotation
        float rotationAmount = currentSpinSpeed * Time.deltaTime; // Reverse rotation if necessary
        transform.Rotate(Vector3.up, rotationAmount); // Rotate around the Y axis

    }
    private void UpdateHover()
    {
        if (currentSpinSpeed <= 1000f) { return; } // Don't hover if the object is not spinning)
        // Calculate the height based on spin speed, clamped to maxHoverHeight
        currentHoverHeight = Mathf.Lerp(0, maxHoverHeight, (currentSpinSpeed - 1000f) / (spinSpeed * 100f)); // Normalize spin speed
        currentHoverHeight = Mathf.Clamp(currentHoverHeight, 0, maxHoverHeight);

        // Add an oscillating motion using Mathf.Sin
        float oscillation = Mathf.Sin(Time.time * hoverSpeed) * hoverOscillationAmplitude;

        // Update the object's position (only the Y value changes)
        Vector3 newPosition = transform.position;
        newPosition.y = hoverBaseHeight + currentHoverHeight + oscillation;
        transform.position = newPosition;
    }

}
