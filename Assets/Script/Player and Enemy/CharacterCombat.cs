using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    CharacterStats opponentStats;

    public float attackSpeed = 1f;
    public float attackCooldown = 0;
    public float DEFAULTATTACKCOOLDOWN = 3f;
    const float combatCoolDown = 6f;     //if character hasn't been in combat for n sec, no longer in combat
    float lastAttackTime;

    public float attackDelay = 0.6f;

    public bool InCombat { get; private set; }
    //Syntax to notify animator controller.
    //This is an easy way to create a delegate with a return type void.
    public event System.Action OnAttack;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        attackCooldown = Mathf.Clamp(attackCooldown, -1, int.MaxValue); //coolDown will not go below -1
        if (Time.time - lastAttackTime > combatCoolDown)
        {
            InCombat = false;
        }
    }
    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;
            //This will trigger the animation.
            if (OnAttack != null)
                OnAttack();
            attackCooldown = (DEFAULTATTACKCOOLDOWN + myStats.attackSpeed.GetValue()) / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());
        //Add Sound audio here.
        AudioManager.Instance.PlaySound("WeaponSwing");
        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
