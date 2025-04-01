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
    public GameObject clue2; //ref to clue 2, for stage 5 convergence gate
    public MorseCodeTransform morseCodeTransform; // ref to morse code transform script

    void Update() {
        // if (Input.GetKeyDown(KeyCode.B)) {
        //     answerSystem.ShowAnswerUI();
        // }
        // cheat code to debug, not needed now since i solved that.
        if (Input.GetKeyDown(KeyCode.C)) {
            textDisplayManager.AddMessageToQueue("Controls: WASD/Arrow Keys to move. Space to pick up/drop items. H for hints. O to change perspective. E to switch dimensions.");
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
            textDisplayManager.AddMessageToQueue("Welcome to the game!\n" +
                "First, you need to activate the mirror." +
                "Move with W A S D, or arrow keys." + 
                "You can pick up items by pressing Space and change perspective with O.");
            textDisplayManager.AddMessageToQueue("You can also press H for hints if you are stuck.");
            textDisplayManager.AddMessageToQueue("Find 2 mirror shards, one in each dimension, to activate the mirror.");
            textDisplayManager.AddMessageToQueue("Pressing C will tell you controls again.");
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
            mirrorRune.SetActive(false);
            clue.gameObject.SetActive(false);
            clue2.gameObject.SetActive(true);
        }
        if(stage == 7)
        {
            textDisplayManager.AddMessageToQueue("The convergence gate has a lock with a 5 letter code on it.\nScattered around the convergence gate are clues... can you piece them together?");
        }
        if(stage == 8)
        {
            morseCodeTransform.EndPart();
        }
    }
}
