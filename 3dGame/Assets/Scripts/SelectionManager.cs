using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public GameObject iteractionUI;
    Text interactionText;

    private void Start()
    {
        interactionText = iteractionUI.GetComponent<Text>();
    }

    private void Update()
    {
        if (Camera.main == null || iteractionUI == null || interactionText == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            var interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                interactionText.text = interactable.GetItemName();
                iteractionUI.SetActive(true);
            }
            else
            {
                iteractionUI.SetActive(false);
            }
        }
        else
        {
            iteractionUI.SetActive(false);
        }
    }
}