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
    public GameObject convergenceKey; // ref to convergence key gameObject

    void Update() {
        // if (Input.GetKeyDown(KeyCode.B)) {
        //     answerSystem.ShowAnswerUI();
        // }
        // cheat code to debug, not needed now since i solved that.
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
            textDisplayManager.AddMessageToQueue("Welcome to the game!\n" +
                "Your goal is to find the mirror shards and activate the mirror.\n" +
                "You can pick up items by pressing Space and change perspective with O.");
            textDisplayManager.AddMessageToQueue("You can also press H for hints if you are stuck.");
            textDisplayManager.AddMessageToQueue("Find 2 mirror shards, one in each dimension, to activate the mirror.");
        }
    }

    // Function to change stage (you can add your logic to change the stage).
    public void ChangeStage(int stage)
    {
        currentStage = stage;
        // Handle what happens when the stage changes.
        if(stage == 2)
        {
            textDisplayManager.AddMessageToQueue("Something seems off. You have access to another dimension. Press E to switch.");
            postProcessing.StartCorruption();
        }
        if(stage == 4)
        {
            textDisplayManager.AddMessageToQueue("The mirror is active. Remember that the corrupted world and this one are linked...");
            mirrorRune.SetActive(true);
            clue.gameObject.SetActive(true);
            textDisplayManager.AddMessageToQueue("A database entry is needed in the corrupted world to find access to the Convergence Gate. Find out what it is... and quick.");
        }
        if(stage == 5)
        {
            textDisplayManager.AddMessageToQueue("You received a key. Where is the convergence gate though?");
        }
    }
}
