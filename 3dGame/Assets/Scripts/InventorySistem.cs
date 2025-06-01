using System.Collections.Generic;
using UnityEngine;

public class InventorySistem : MonoBehaviour
{
    public bool isInvOpen = false;
    public static InventorySistem Instance { get; set; }

    [SerializeField] private GameObject _inv;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject _itemToAdd;
    private GameObject slotToEquip;

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
        _inv.SetActive(false); // Закриває інвентар на старті
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in _inv.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject); // Правильне додавання
            }
        }
    }

    private void InventoryOpen()
    {
        isInvOpen = true;
        _inv.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void InventoryClose()
    {
        isInvOpen = false;
        _inv.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isInvOpen && Input.GetKeyDown(KeyCode.Tab)) InventoryOpen();
        else if (isInvOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))) InventoryClose();
    }

    public void AddToInv(string _itemName)
    {
        slotToEquip = FindeNextSlot();
        _itemToAdd = Instantiate(Resources.Load<GameObject>(_itemName),
            slotToEquip.transform.position,
            slotToEquip.transform.rotation);

        _itemToAdd.transform.SetParent(slotToEquip.transform);
        itemList.Add(_itemName);
    }

    private GameObject FindeNextSlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        return new GameObject(); // Якщо слотів немає — повертає пустий
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0) counter++;
        }

        return counter >= slotList.Count; // Гнучкіше
    }
}
