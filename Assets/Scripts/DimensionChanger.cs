using UnityEngine;

public class DimensionChanger : MonoBehaviour
{
    public Transform happyWorldPosition; // ref for happy world position.
    public Transform machineWorldPosition; // ref for corrupt world position.
    public PostProcessing corruptedWorldPostProcess; // ref for corrupted world post-process volume.
    private bool isInHappyWorld = true;

    void Update()
    {
        // check current stage is 2 or higher, if so allow dimension switching
        if (GameController.Instance.currentStage >= 2 && Input.GetKeyDown(KeyCode.E)) // press e to switch dimensions
        {
            SwitchDimension();
        }
    }

    void SwitchDimension()
    {
        if (isInHappyWorld)
        {
            transform.position = machineWorldPosition.position;
            corruptedWorldPostProcess.CorruptionEffect(true);
        }
        else
        {
            transform.position = happyWorldPosition.position;
            corruptedWorldPostProcess.CorruptionEffect(false);
        }

        isInHappyWorld = !isInHappyWorld;
    }
}
