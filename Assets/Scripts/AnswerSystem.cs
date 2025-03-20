using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerSystem : MonoBehaviour
{
    public TMP_InputField answerInput; // Assign in Inspector
    public TextMeshProUGUI feedbackText; // Assign in Inspector
    public Button submitButton; // Assign in Inspector
    public string correctAnswer = "TIME"; // Change this to the actual answer
    public PlayerController playerControls; // ref for disabling player controls while typing
    public DimensionChanger dimensionChanger; // ref for dimension changer script
    public HintManager hintManager; // ref for hint manager script

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

        if (playerInput == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            ProceedToNextStage(); // Call function to continue the game
        }
        else
        {
            feedbackText.text = "Wrong answer. Try again.";
            feedbackText.color = Color.red;
        }
    }

    void ProceedToNextStage()
    {
        GameController.Instance.ChangeStage(5); // move to the next stage if correct
        HideAnswerUI(); // hide the answer UI after correct answer
    }
}
