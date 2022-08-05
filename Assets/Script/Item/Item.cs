using UnityEngine;
//InventoryItemData = item
//InventoryItem = inventory
//Menuname is where we navigate to create new item.
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string id;                   //Id string to reference easily.    
    public string displayName;          //Creates a new item name.
    public Sprite icon = null;          //Item icon
    public GameObject prefab;           //Gameobject to be referenced for item.
    public bool isDefaultItem = false;  //Is the item default wear?
    public bool oneHanded = true;

    /**
     * The reason use doesn't defind functionality is because different
     * items have different functionality. Some are currency, some are potions,
     * some are equippable items.
     */
    public virtual void Use()
    {
        //Use the item
        //Something might happen

        //Debug.Log("Using " + displayName);
    }

    //Removes item from inventory when equipped.
    public void RemoveFromInventory()
    {
        InventoryManager.instance.Remove(this);
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet, Consumable }