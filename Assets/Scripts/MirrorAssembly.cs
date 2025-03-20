using UnityEngine;

public class MirrorAssembly : MonoBehaviour
{
    private int shardsCollected = 0;
    public GameObject fullMirror; // Assign in Inspector (the completed mirror prefab)
    public TextDisplayManager textDisplayManager; // ref to text display manager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player
        {
            PlayerPickup pickup = other.GetComponent<PlayerPickup>();
            if (pickup != null && pickup.heldItem != null)
            {
                PickupItem item = pickup.heldItem.GetComponent<PickupItem>(); // Access the item script
                if (item != null && item.itemName == "MirrorShard")
                {
                    CollectShard(pickup);
                    if (shardsCollected == 1)
                    {
                        textDisplayManager.AddMessageToQueue("The first shard has been placed.");
                        GameController.Instance.ChangeStage(2);
                    }
                    if (shardsCollected == 2)
                    {
                        textDisplayManager.AddMessageToQueue("The mirror is now complete.");
                        GameController.Instance.ChangeStage(4);
                    }
                }
            }
        }
    }

    void CollectShard(PlayerPickup pickup)
    {
        Destroy(pickup.heldItem); // Remove shard from player’s hand
        pickup.heldItem = null; // Reset held item
        shardsCollected++;

        if (shardsCollected >= 2) // Once both shards are collected
        {
            fullMirror.SetActive(true); // Reveal full mirror
            gameObject.SetActive(false); // Hide the old mirror trigger
        }
    }
}
