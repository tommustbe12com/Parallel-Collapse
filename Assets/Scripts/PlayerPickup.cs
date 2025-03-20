using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform handPosition; // Assign in Inspector
    public GameObject heldItem = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heldItem == null) // If nothing is held, try to pick up
            {
                TryPickup();
            }
            else
            {
                DropItem(); // Drop item if already holding something
            }
        }
    }

    void TryPickup()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Pickable"))
            {
                PickUpItem(collider.gameObject);
                break;
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        heldItem = item;
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true; // Disable physics while held

        item.transform.SetParent(handPosition);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    void DropItem()
    {
        if (heldItem)
        {
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false; // Re-enable physics when dropped

            heldItem.transform.SetParent(null);
            heldItem = null;
        }
    }
}
