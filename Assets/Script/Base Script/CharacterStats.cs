using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100f;
    //Get means any other class will be able to get this value but can only set this value within this class.
    public float currentHealth { get; private set; }

    public Stats damage;
    public Stats armor;
    public Stats attackSpeed;


    public bool berserkerMode = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {

    }
    private void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    /// <summary>
    /// Increases health by amount.Use in Player/Enemy Stats.
    /// </summary>
    /// <param name="amount"></param>
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    /// <summary>
    /// Decreases health by amount. Use in Player/Enemy Stats.
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
    }

    /// <summary>
    /// Item that will increase damage, armor, or attackspeed.
    /// </summary>
    /// <param name="amount"></param>
    public IEnumerator PowerUps(int damageAmount, int armorAmount, int attackSpeedAmount, float time)
    {
        damage.AddModifier(damageAmount);
        armor.AddModifier(armorAmount);
        attackSpeed.AddModifier(attackSpeedAmount);

        yield return new WaitForSeconds(time);

        damage.RemoveModifier(damageAmount);
        armor.RemoveModifier(armorAmount);
        attackSpeed.RemoveModifier(attackSpeedAmount);
    }

    public IEnumerator BerserkerMode(int damageAmount, int armorAmount, int attackSpeedAmount, float time)
    {
        berserkerMode = true;
        damage.AddModifier(damageAmount);
        armor.AddModifier(armorAmount);
        attackSpeed.AddModifier(attackSpeedAmount);

        yield return new WaitForSeconds(time);

        berserkerMode = false;
        damage.RemoveModifier(damageAmount);
        armor.RemoveModifier(armorAmount);
        attackSpeed.RemoveModifier(attackSpeedAmount);
    }
    public virtual void Die()
    {
        //Die in some way
        //This method is meant to be overwritten.
        Debug.Log(transform.name + " died");
    }
}
