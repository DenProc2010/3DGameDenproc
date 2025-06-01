using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (DragDrop.itemBeingDragged != null)
        {
            // Прив'язати предмет до цього слота
            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
