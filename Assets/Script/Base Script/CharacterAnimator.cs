using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim; //Will be used to replace animation clip in  OnAttack
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomationAnimationSmoothTime = 0.1f;
    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    public AnimatorOverrideController overrideController;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Agent current speed divided by maximum speed.
        float speedPercent = agent.velocity.magnitude / agent.speed;
        //locomationAnimationSmoothTime is the time it take to smooth between animation.
        animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("inCombat", combat.InCombat);

    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
