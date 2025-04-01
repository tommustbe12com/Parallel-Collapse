using UnityEngine;

public class CheckKey : MonoBehaviour
{
    public TextDisplayManager textDisplayManager; // ref to text display manager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player
        {
            PlayerPickup pickup = other.GetComponent<PlayerPickup>();
            if (pickup != null && pickup.heldItem != null)
            {
                PickupItem item = pickup.heldItem.GetComponent<PickupItem>(); // Access the item script
                if (item != null && item.itemName == "ConvergenceKey")
                {
                    Destroy(pickup.heldItem); // remove key because it unlocked the door
                    GameController.Instance.ChangeStage(7);
                }
            }
        }
    }
}
