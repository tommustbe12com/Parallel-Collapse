using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName; // Name of the item for inventory
    private bool isNear = false; // Track if player is close

    void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E)) // Press E to pick up
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Picked up: " + itemName);
        gameObject.SetActive(false); // Hide item after pickup
        // Here you can add it to an inventory system later!
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }
}
