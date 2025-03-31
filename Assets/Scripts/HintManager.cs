using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public TextDisplayManager textDisplayManager; // ref to text display manager.
    
    void Update()
    {
        // Check if the player presses 'H'
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowHint();
        }
    }

    void ShowHint()
    {
        string hintMessage = GetHintMessage(GameController.Instance.currentStage);
        textDisplayManager.AddMessageToQueue(hintMessage);
    }

    string GetHintMessage(int stage)
    {
        switch (stage)
        {
            case 1:
                return "Try to find mirror shards. One mirror shard is in each world hidden.";
            case 2:
                return "Change dimensions with E and look for the mirror shard there.";
            case 4:
                return "Caesar cipher is used with a shift of +3. Look it up if you're confused.";
            case 5:
                return "Some things can be seen in the mirror that can't be seen in person.";
            case 6:
                return "Morse code messages in a certain order...";
            case 8:
                return "The end is near...";
            default:
                return "No hint available.";
        }
    }
}
