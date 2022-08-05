using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    InventoryManager inventory;
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        //Instance references to singleton.
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallback += UpdateUI;

        //If slots are changing, this should go in update. But since this is static
        //running it once is fine.
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    //This may be necessary to modify to deactivate x button.
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
