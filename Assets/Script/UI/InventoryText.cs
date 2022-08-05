using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is a text inventory that will display the amount of item in inventory.
/// </summary>
public class InventoryText : MonoBehaviour
{
    public Text inventoryText;
    public InventoryManager inventory;

    // Update is called once per frame
    void Update()
    {
        inventoryText.text = "Inventory Size\n" + inventory.items.Count + " / " + inventory.inventorySpace;
    }
}
