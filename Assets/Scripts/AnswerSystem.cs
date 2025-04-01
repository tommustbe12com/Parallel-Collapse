using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerSystem : MonoBehaviour
{
    public TMP_InputField answerInput; 
    public TextMeshProUGUI feedbackText; 
    public Button submitButton; 
    public string correctAnswer = "TIME"; 
    public string morseCorrectAnswer = "MERGE";
    public bool isMorse = false;
    public PlayerController playerControls; 
    public DimensionChanger dimensionChanger; 
    public HintManager hintManager;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && answerInput.gameObject.activeSelf) // Press Enter to submit
        {
            CheckAnswer();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && answerInput.gameObject.activeSelf) // Press Escape to cancel
        {
            HideAnswerUI();
        }
    }

    public void ShowAnswerUI()
    {
        answerInput.gameObject.SetActive(true);
        answerInput.text = ""; // clear previous input
        feedbackText.gameObject.SetActive(true);
        submitButton.gameObject.SetActive(true);
        playerControls.enabled = false;
        dimensionChanger.enabled = false;
        hintManager.enabled = false;
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Make cursor visible
    }

    public void HideAnswerUI()
    {
        answerInput.gameObject.SetActive(false);
        feedbackText.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);
        playerControls.enabled = true;
        dimensionChanger.enabled = true;
        hintManager.enabled = true;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
    }

    public void CheckAnswer()
    {
        string playerInput = answerInput.text.ToUpper(); // Convert to uppercase for case insensitivity

        if (isMorse == false) {
            if (playerInput == correctAnswer)
            {
                feedbackText.text = "Correct!";
                feedbackText.color = Color.green;
                ProceedToNextStage(); // Call function to continue the game
                isMorse = true; // uh yeah this could technically lead to a problem but -1000% likely since player probly won't try to redo the thing they already got right :}
            }
            else
            {
                feedbackText.text = "Wrong answer. Try again.";
                feedbackText.color = Color.red;
            }
        } else 
        {
            if (playerInput == morseCorrectAnswer)
            {
                feedbackText.text = "Correct!";
                feedbackText.color = Color.green;
                GameController.Instance.ChangeStage(8); // Call function to continue the game
                HideAnswerUI();
            }
            else
            {
                feedbackText.text = "Wrong answer. Try again.";
                feedbackText.color = Color.red;
            }
        }
    }

    void ProceedToNextStage()
    {
        GameController.Instance.ChangeStage(5); // move to the next stage if correct
        HideAnswerUI(); // hide the answer UI after correct answer
    }
}
