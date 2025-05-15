using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public bool playerInRange;

    public string GetItemName()
    {
        return ItemName;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.Instance.onTarget)
        {
            Destroy(gameObject);
        }
    }

}