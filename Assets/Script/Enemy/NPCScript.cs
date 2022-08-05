using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles interaction with enemy.
/// </summary>
[RequireComponent(typeof(CharacterStats))]
public class NPCScript : Interactable
{
    GameManager playerManager;
    CharacterStats myStats;
    EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        playerManager = GameManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        //Attack the enemy.
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        //This makes it so the player attacks npc/enemy.
        if (playerCombat != null && enemyController.isEnemy == true)
        {
            playerCombat.Attack(myStats);
        }

    }
}
