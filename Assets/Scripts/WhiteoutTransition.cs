using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WhiteoutTransition : MonoBehaviour
{
    public Image whiteoutImage;  // assign full-screen UI Image with a white color
    public float fadeDuration = 2f; // time it takes to fully fade
    public Transform teleportLocation; // Where the player will go

    private bool isFading = false;
    private bool hasFaded = false;

    void Update()
    {
        if (GameController.Instance.currentStage == 7)
        {
            StartWhiteout();
        }
    }

    public void StartWhiteout()
    {
        if (!isFading && !hasFaded)
        {
            whiteoutImage.gameObject.SetActive(true);
            StartCoroutine(WhiteoutSequence());
        }
    }

    private IEnumerator WhiteoutSequence()
    {
        isFading = true;
        float elapsedTime = 0f;

        // Gradually increase the alpha to make the screen white
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeDuration;
            whiteoutImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Ensure it's fully white
        whiteoutImage.color = new Color(1, 1, 1, 1);

        // Teleport the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && teleportLocation != null)
        {
            player.transform.position = teleportLocation.position;
        }

        // OPTIONAL: Fade back to normal
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeDuration);
            whiteoutImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // Ensure it's fully transparent
        whiteoutImage.color = new Color(1, 1, 1, 0);
        isFading = false;
        hasFaded = true;
    }
}
