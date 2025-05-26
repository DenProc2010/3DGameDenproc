using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }

    [SerializeField] private GameObject _interactionUI;
    [SerializeField] private float _rayLength = 3f;
    [SerializeField] private LayerMask _interactableLayer;

    private Text _interactionText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _interactionText = _interactionUI.GetComponent<Text>();
    }

    private void Update()
    {
        if (Camera.main == null || _interactionUI == null || _interactionText == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * _rayLength, Color.red);

        if (Physics.Raycast(ray, out hit, _rayLength))
        {
            InteractableObject interactable = hit.transform.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                _interactionText.text = interactable.GetItemName();
                _interactionUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }

                return;
            }
        }

        _interactionUI.SetActive(false);
    }
}
