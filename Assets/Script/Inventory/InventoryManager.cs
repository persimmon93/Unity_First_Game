using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    #region Singleton

    //Singleton Look it up later.
    //Static variable that is shared by all instance of class.
    public static InventoryManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        //Will always be able to access this instance.
        instance = this;
    }

    #endregion

    //If item changes in inventory, it triggers onItemChangedCallback.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public float inventorySpace = 10f;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= inventorySpace)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
