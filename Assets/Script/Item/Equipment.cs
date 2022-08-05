using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This holds the item of the player when it is clicked in the inventory.
/// </summary>
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; //Slot to store equipment in

    public int armorModifier;
    public int damageModifier;
    public int attackSpeedModifier;

    public override void Use()
    {
        base.Use();
        //Equip the item
        EquipmentManager.instance.Equip(this);
        //GameObject equipment 
        //Remove it from the inventory
        RemoveFromInventory();
    }
}
