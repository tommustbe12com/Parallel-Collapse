using UnityEngine;

public class DatabaseTerminal : MonoBehaviour
{
    public AnswerSystem answerSystem; // Reference to the player movement script

    // Trigger when player enters collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure it's the player
        {
            answerSystem.ShowAnswerUI();
        }
    }

    // Trigger when player exits collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            answerSystem.HideAnswerUI();
        }
    }
}
