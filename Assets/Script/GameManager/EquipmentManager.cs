using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script handles the equipment when player equips an item.
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] currentEquipment;       //Items we currently have equipped.
    //Call back method that triggers whenever our equipment changes.
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryManager inventory;
    GameObject player;

    //Note this is brute force. Should make a list for gameobject locations of where
    //equipment will go. Only have lefthand/righthand because there is no skinnedmesh
    //that can change the equipment for the player. Only weapons can be changed.
    public GameObject leftHand;         //Hold shield
    public GameObject rightHand;        //Hold weapons
    public Image leftEquipment, rightEquipment;         //UI showing what equipment player is holding.

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");

        inventory = InventoryManager.instance;
        //Stores the length of the available slots in equipment slots.
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    //Check if equipment is weapon or shield. If it it is, then it should go into right spot.
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        //Replaces item if the same type is equipped.
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        //Invoke onEquipmentChanged.
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem;
        //This is only to instantiate objects in the scene.
        switch (slotIndex)
        {
            case 0:
                Debug.Log("Equipping Head");
                break;
            case 1:
                Debug.Log("Equipping Chest");
                break;
            case 2:
                Debug.Log("Equipping Legs");
                break;
            case 3:
                Debug.Log("Equipping Weapon");
                InstantiateEquipment(rightHand.transform, oldItem, newItem);
                rightEquipment.sprite = currentEquipment[3].icon;
                //Unequips shield if newitem is a two handed weapon.
                if (!newItem.oneHanded)
                {
                    Unequip(4);
                }
                break;
            case 4:
                Debug.Log("Equipping Shield");
                InstantiateEquipment(leftHand.transform, oldItem, newItem);
                leftEquipment.sprite = currentEquipment[4].icon;
                //Prevents shield from being equippable if twohanded weapon is equipped.
                if (currentEquipment[3] != null && !currentEquipment[3].oneHanded)
                {
                    Unequip(4);
                }
                break;
            case 5:
                Debug.Log("Equipping Feet");
                break;
            case 6:
                ConsumeEquipment();
                Debug.Log("Equipping Consummable");
                break;
        }
    }

    
    /// <summary>
    /// Returns the position of passed in gameobject.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public Vector3 objectPosition(GameObject go)
    {
        return new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
    } 
    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            //Invoke onEquipmentChanged.
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            switch (slotIndex)
            {
                case 0:
                    Debug.Log("Unequip Head");
                    break;
                case 1:
                    Debug.Log("Unequip Chest");
                    break;
                case 2:
                    Debug.Log("Unequip Legs");
                    break;
                case 3:
                    Debug.Log("Unequip Weapon");
                    InstantiateEquipment(rightHand.transform, oldItem);
                    rightEquipment.sprite = null;
                    break;
                case 4:
                    Debug.Log("Unequip Shield");
                    InstantiateEquipment(leftHand.transform, oldItem);
                    leftEquipment.sprite = null;
                    break;
                case 5:
                    Debug.Log("Unequip Feet");
                    break;
                case 6:
                    Debug.Log("Unequip Consummable");
                    break;
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }


    /// <summary>
    /// This will insantiate equipment in the game.
    /// </summary>
    /// <param name="oldItem"></param>          If oldItem = null, it is equipping first item.
    /// <param name="newItem"></param>          If newItem = null, it is unequipping.
    /// <param name="equipmentSlot"></param>    position of the equipment slot.
    /// <param name="location"></param>         location for where equipment will go.
    public void InstantiateEquipment(Transform location, Equipment oldItem, Equipment newItem = null)
    {
        if (oldItem == null)
        {
            Instantiate(newItem.prefab, location.transform.position, location.transform.rotation, location.transform);
        } else if (newItem == null)
        {
            if (location.GetChild(0) != null)
            {
                Destroy(location.GetChild(0).gameObject);
            }
        }
        else
        {
            if (location.GetChild(0))
            {
                Destroy(location.GetChild(0).gameObject);
            }
            Instantiate(newItem.prefab, location.transform.position, location.transform.rotation, location.transform);
        }
    }

    /// <summary>
    /// Checks condition for consummable equipment and will run them if able.
    /// Could move all data related to consummable items into a new script.
    /// </summary>
    public void ConsumeEquipment()
    {
        PlayerStats playerstats = player.GetComponent<PlayerStats>();
        switch(currentEquipment[6].name)
        {
            case "Health Potion":
                if (playerstats.currentHealth < playerstats.maxHealth)
                {
                    playerstats.IncreaseHealth(10);
                    currentEquipment[6] = null;
                } else
                {
                    Unequip(6);
                }
                break;

            case "Berserker Mushroom":
                if (!playerstats.berserkerMode)
                {
                    currentEquipment[6] = null;
                    //Take param int damageAmount, int armorAmount, int attackSpeedAmount, float timer.
                    StartCoroutine(playerstats.BerserkerMode(15, 5, -3, 5f));
                } else
                {
                    Unequip(6);
                }
                break;
        }
    }
}
