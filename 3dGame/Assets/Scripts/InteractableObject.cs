using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string _itemName;

    public string GetItemName()
    {
        return _itemName;
    }

    public void PickUpItem()
    {
        if (!InventorySistem.Instance.CheckIfFull())
        {
            InventorySistem.Instance.AddToInv(_itemName);
            Destroy(gameObject);
        }
    }
}
