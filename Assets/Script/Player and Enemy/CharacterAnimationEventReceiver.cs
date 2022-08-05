using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script will handle events during animation process.
/// For example, when a weapon is swung, it will take damage in
/// a few seconds during animation.
/// </summary>
public class CharacterAnimationEventReceiver : MonoBehaviour
{
    public CharacterCombat combat;
    private void Start()
    {
    }


    //This method will be called during frame when weapon comes into contact with enemy.
    public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }




    #region SoundScript
    public void PunchAudio()
    {
        AudioManager.Instance.PlaySound("Punch4");
    }

    public void WeaponAudio()
    {
        AudioManager.Instance.PlaySound("WeaponSwing");
    }

    public void FootStepEvent()
    {
        AudioManager.Instance.PlaySound("Footstep");
    }
    #endregion
}
