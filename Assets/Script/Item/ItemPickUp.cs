using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to pick up an item from the scene. Destroys item once it is picked up.
/// </summary>
public class ItemPickUp : Interactable
{
    public Item item;
    public bool canPickUP = true;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        if (canPickUP)
        {
            //Debug.Log("PickingUp item." + item.displayName);
            //Add to inventory
            bool wasPickedUp = InventoryManager.instance.Add(item);
            if (wasPickedUp)
                Destroy(gameObject);
        }
    }
}
