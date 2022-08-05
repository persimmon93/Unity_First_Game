using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public HealthBar healthBar;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
    }

    public override void Die()
    {
        base.Die();

        GameManager.instance.targetUI.SetActive(false);
        GameManager.instance.targetText.text = "";

        //Add ragdoll effect/death animation

        Destroy(gameObject);
    }
}
