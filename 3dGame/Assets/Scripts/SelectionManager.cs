using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{

    public static SelectionManager Instance { get; set; }

    public GameObject iteractionUI;
    Text interactionText;

    public bool onTarget;

    private void Start()
    {
        interactionText = iteractionUI.GetComponent<Text>();
    }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }       
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

            if (interactable != null && interactable.playerInRange)
            {
                onTarget = true;
                interactionText.text = interactable.GetItemName();
                iteractionUI.SetActive(true);
            }
            else
            {
                onTarget = false;
                iteractionUI.SetActive(false);
            }
        }
        else
        {
            onTarget = false;
            iteractionUI.SetActive(false);
        }
    }
}