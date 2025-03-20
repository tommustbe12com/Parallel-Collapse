using System.Collections;
using UnityEngine;
using TMPro;

public class TextGlitchEffect : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float glitchDuration = 0.8f;  // Time for glitching
    public float glitchSpeed = 0.05f;   // How fast it flickers
    private string originalText;

    void Start()
    {
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        originalText = textMeshPro.text;
        StartCoroutine(GlitchText());
    }

    IEnumerator GlitchText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < glitchDuration)
        {
            textMeshPro.text = GenerateGlitchedText(originalText);
            elapsedTime += glitchSpeed;
            yield return new WaitForSeconds(glitchSpeed);
        }

        // Restore the original text smoothly
        StartCoroutine(RestoreText());
    }

    IEnumerator RestoreText()
    {
        char[] currentText = textMeshPro.text.ToCharArray();
        int length = originalText.Length;

        for (int i = 0; i < length; i++)
        {
            currentText[i] = originalText[i];
            textMeshPro.text = new string(currentText);
            yield return new WaitForSeconds(glitchSpeed * 0.75f);
        }
    }

    string GenerateGlitchedText(string text)
    {
        char[] glitchChars = { '#', '@', '%', '*', '&', '!', '?', 'X', 'Y', 'Z', '0', '1' };
        char[] newText = text.ToCharArray();

        for (int i = 0; i < newText.Length; i++)
        {
            if (Random.value > 0.5f)  // 50% chance to glitch each letter
            {
                newText[i] = glitchChars[Random.Range(0, glitchChars.Length)];
            }
        }

        return new string(newText);
    }
}
