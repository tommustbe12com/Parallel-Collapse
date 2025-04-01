using System.Collections;
using UnityEngine;
using TMPro;

public class MorseCodeTransform : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public AudioSource jumpscareSound;
    public GameObject jumpscareEffect;
    
    public float typeSpeed = 0.3f;
    public float translateSpeed = 0.7f;
    public float jumpscareDelay = 1.5f;

    private string morseCode = "-... . .... .. -. -.. / -.-- --- ..-";
    private string translation = "BEHIND YOU";

    public void EndPart()
    {
        StartCoroutine(TypeAndTranslate());
    }

    IEnumerator TypeAndTranslate()
    {
        textDisplay.text = "";

        string[] morseLetters = morseCode.Split(' ');
        char[] englishLetters = translation.ToCharArray();

        // Step 1: Type out Morse code
        for (int i = 0; i < morseLetters.Length; i++)
        {
            textDisplay.text += morseLetters[i] + " ";
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return new WaitForSeconds(1f); // Pause before translating

        // Step 2: Slowly replace each Morse character with the English letter
        string[] morseArray = textDisplay.text.Trim().Split(' '); // Split the displayed Morse code
        for (int i = 0; i < englishLetters.Length; i++)
        {
            morseArray[i] = englishLetters[i].ToString(); // Replace Morse with English letter
            textDisplay.text = string.Join(" ", morseArray); // Rebuild the text string
            yield return new WaitForSeconds(translateSpeed);
        }

        yield return new WaitForSeconds(jumpscareDelay);

        // Step 3: Jumpscare
        jumpscareSound.Play();
        jumpscareEffect.SetActive(true);
        ScreenShake();
        textDisplay.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        // Step 4: Fade to black and end
        StartCoroutine(FadeToBlack());
    }

    void ScreenShake()
    {
        // Example simple screen shake effect
        Camera.main.transform.position += new Vector3(0.1f, 0.1f, 0);
    }

    IEnumerator FadeToBlack()
    {
        CanvasGroup canvasGroup = FindObjectOfType<CanvasGroup>();
        if (canvasGroup)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / 2;
                yield return null;
            }
        }
    }
}
