using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayManager : MonoBehaviour
{
    public TMP_Text uiText; // Reference to the TextMesh Pro UI Text component.
    public float typingSpeed = 0.05f; // Speed of the text typing effect.
    public float fadeOutDuration = 1f; // Duration for the fade-out effect.
    private Coroutine currentCoroutine; // Reference to the currently running typing coroutine.

    private Queue<string> messageQueue = new Queue<string>(); // Queue to store messages to display.

    void Start()
    {
        uiText.text = ""; // Ensure the text starts empty.
        uiText.alpha = 1; // Ensure the text starts fully visible.
    }

    // Method to add a message to the queue
    public void AddMessageToQueue(string message)
    {
        messageQueue.Enqueue(message);

        // If no message is currently being typed, start the process of displaying messages.
        if (currentCoroutine == null)
        {
            StartCoroutine(DisplayNextMessage());
        }
    }

    // Coroutine to handle displaying the next message in the queue.
    private IEnumerator DisplayNextMessage()
    {
        // While there are messages to show
        while (messageQueue.Count > 0)
        {
            string message = messageQueue.Dequeue();

            // Display the message with the typing effect
            currentCoroutine = StartCoroutine(TypeText(message));

            // Wait for the current message to finish typing before proceeding
            yield return currentCoroutine;

            // Wait for 3 seconds before starting to fade
            yield return new WaitForSeconds(3f);

            // Fade out the current text
            yield return StartCoroutine(FadeOutText());

            // After fading out, reset the text to be fully opaque again
            uiText.alpha = 1f;  // Ensure it's visible before typing the next message.
        }

        // Once all messages are processed, reset the UI text to be blank
        uiText.text = "";
        currentCoroutine = null; // Reset currentCoroutine when done.
    }

    // Coroutine to type out the text with a delay for each character.
    private IEnumerator TypeText(string text)
    {
        uiText.text = ""; // Clear any previous text.

        // Type out each character one by one
        foreach (char letter in text)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Wait before showing the next letter.
        }
    }

    // Coroutine to fade out the text.
    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        float startingAlpha = uiText.alpha;

        // Gradually reduce the alpha value to zero (fade out).
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            uiText.alpha = Mathf.Lerp(startingAlpha, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        // Ensure the text is fully faded out.
        uiText.alpha = 0f;
    }
}
