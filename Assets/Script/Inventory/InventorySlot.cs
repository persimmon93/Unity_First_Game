using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Handles the icon in the inventory.
/// </summary>
public class InventorySlot: MonoBehaviour
    //This is needed for Drag and drop. IDropHandler
{
    public Image icon;
    public Button removeButton;
    public Text stack;
    public int stackCount;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        InventoryManager.instance.Remove(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    /// <summary>
    /// Testing. Used for drag and dropping equipment in inventory.
    /// </summary>
    /// <param name="eventData"></param>
    //public void OnDrop(PointerEventData eventData)
    //{
    //    Debug.Log("OnDrop");
    //    if (eventData.pointerDrag != null)
    //    {
    //        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
    //    }
    //}
}
