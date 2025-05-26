using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string _itemName;

    public string GetItemName()
    {
        return _itemName;
    }

    public void Interact()
    {
        Destroy(gameObject);
    }
}
