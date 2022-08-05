using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            attackSpeed.AddModifier(newItem.attackSpeedModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            attackSpeed.RemoveModifier(oldItem.attackSpeedModifier);
        }
    }
    
    public override void Die()
    {
        base.Die();
        GameManager.instance.EndGame();
    }
}
