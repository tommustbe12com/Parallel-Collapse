using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    public PostProcessVolume postProcessVolume; // Drag your PostProcessVolume here
    public float minFlickerDuration = 0.1f;  // Minimum flicker duration (in seconds) - Faster
    public float maxFlickerDuration = 0.3f;   // Maximum flicker duration (in seconds) - Faster
    public float maxFlickerTime = 1f;        // Maximum time for flickering (in seconds)
    
    private bool isFlickering = false;  // Whether the corruption effect is active
    private float flickerTimer = 0f;    // Timer to track how long we've been flickering
    private float currentFlickerDuration = 0f;  // Current flicker duration
    private float nextFlickerTime = 0f; // Time until next flicker toggle

    void Update()
    {
        if (isFlickering)
        {
            // Track the flicker timer
            flickerTimer += Time.deltaTime;

            // If the flicker time exceeds the max time, stop flickering
            if (flickerTimer > maxFlickerTime)
            {
                StopCorruption();
                return;
            }

            // Check if it's time for the next flicker
            if (Time.time >= nextFlickerTime)
            {
                // Randomly set the next flicker duration
                currentFlickerDuration = Random.Range(minFlickerDuration, maxFlickerDuration);

                // Switch the corruption effect (turn it on or off)
                ToggleCorruptionEffect();

                // Set the time for the next flicker to happen
                nextFlickerTime = Time.time + currentFlickerDuration;
            }
        }
    }

    // Call this method to start the corruption effect
    public void StartCorruption()
    {
        isFlickering = true;
        flickerTimer = 0f;  // Reset the flicker timer
        nextFlickerTime = Time.time + Random.Range(minFlickerDuration, maxFlickerDuration); // Randomize initial flicker time
        EnableCorruption();  // Initially turn on the effects
    }

    // Call this method to stop the corruption effect
    public void StopCorruption()
    {
        isFlickering = false;
        DisableCorruption();  // Turn off the effects
    }

    // Enable all post-processing effects
    private void EnableCorruption()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.enabled = true;  // Enable the entire post-processing volume
        }
    }

    // Disable all post-processing effects
    private void DisableCorruption()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.enabled = false;  // Disable the post-processing volume
        }
    }

    // Toggle the corruption effect (on or off)
    public void ToggleCorruptionEffect()
    {
        if (postProcessVolume != null)
        {
            // Toggle visibility of the post-process effect (random flicker on/off)
            postProcessVolume.enabled = !postProcessVolume.enabled;
        }
    }

    public void CorruptionEffect(bool enable)
    {
        if (enable)
        {
            EnableCorruption();
        }
        else
        {
            DisableCorruption();
        }
    }
}
