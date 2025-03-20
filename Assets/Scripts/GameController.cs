using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int currentStage = 1; // Start at stage 1.
    public TextDisplayManager textDisplayManager; // ref to text display manager.
    public PostProcessing postProcessing; // ref to post-processing script.
    public GameObject mirrorRune; // ref to mirror rune object to be shown on stage 4
    public TextMeshPro clue; // ref to clue text.
    public AnswerSystem answerSystem; // ref to answer system script.

    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            answerSystem.ShowAnswerUI();
        }
    }

    void Awake()
    {
        // Ensure there is only one instance of GameController.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Don't destroy the object when loading new scenes.
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Start the game with Stage 1 and show the introductory text.
        if (currentStage == 1)
        {
            textDisplayManager.AddMessageToQueue("Welcome to a world of kindness and joy, but beware of its opposite.");
            textDisplayManager.AddMessageToQueue("Find the 2 mirror shards, one in each dimension.");
            textDisplayManager.AddMessageToQueue("Press spacebar to pick up items and O to change perspective.");
        }
    }

    // Function to change stage (you can add your logic to change the stage).
    public void ChangeStage(int stage)
    {
        currentStage = stage;
        // Handle what happens when the stage changes.
        if(stage == 2)
        {
            textDisplayManager.AddMessageToQueue("You found half of the mirror. Transfer to the\ncorrupted world to find the other half.");
            textDisplayManager.AddMessageToQueue("You now have the ability to transfer between\ndimensions. Press E to change dimensions.");
            postProcessing.StartCorruption();
        }
        if(stage == 4)
        {
            textDisplayManager.AddMessageToQueue("The mirror is active If you need a hint, press H.");
            mirrorRune.SetActive(true);
            clue.gameObject.SetActive(true);
        }
        if(stage == 5)
        {
            textDisplayManager.AddMessageToQueue("You received a key. I wonder where it goes?");
        }
    }
}
