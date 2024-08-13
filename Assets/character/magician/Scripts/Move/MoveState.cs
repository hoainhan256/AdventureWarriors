using System.Collections;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class MoveState : CharacterState
{
    public MoveState(MagicianChar magicianChar) : base(magicianChar) { }
    public override void EnterState()
    {
        MagicianChar._mSFX.SFX.clip = MagicianChar._mSFX.footStep;
        MagicianChar._mSFX.SFX.Play();
    }

    public override void UpdateState()
    {
        MagicianChar._mSFX.SFX.loop = true;
       
        //else if(MagicianChar.InputManager.isDodge)
        //{
        //    MagicianChar.TransitionToState(MagicianChar.dodgeState);
        //}
        //else if (MagicianChar.InputManager.isBlock)
        //{
        //    MagicianChar.TransitionToState(MagicianChar.blockState);

        ////}
        // if (MagicianChar.InputManager.isAttack)
        //{
        //    Debug.Log("Move to Attack");
        //    MagicianChar.anim.SetTrigger("isAttack");
        //    MagicianChar.TransitionToState(MagicianChar.moveAttack);
        //}
        //else if (MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
        MagicianChar.Locomotion.HandleAllMoverment();
        if (MagicianChar.InputManager.Spirits)
        {
            if (MagicianChar.MoveInfor <= 2.5f)
            {
                MagicianChar.MoveInfor += MagicianChar.acceleration * Time.deltaTime;
            }
            if(MagicianChar.Locomotion.moveSpeed < MagicianChar.Locomotion.runSpeed)
            {
                MagicianChar.Locomotion.moveSpeed += 1 *MagicianChar.Locomotion.acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (MagicianChar.MoveInfor <= 1f)
            {
                MagicianChar.MoveInfor += MagicianChar.acceleration * Time.deltaTime;
            }
            else if (MagicianChar.MoveInfor > 1.1f)
            {
                MagicianChar.MoveInfor -= MagicianChar.acceleration * Time.deltaTime;
            }
            if (MagicianChar.Locomotion.moveSpeed < MagicianChar.Locomotion.walkSpeed)
            {
                MagicianChar.Locomotion.moveSpeed += 1 * MagicianChar.Locomotion.acceleration * Time.deltaTime;
            }
            else if(MagicianChar.Locomotion.moveSpeed > MagicianChar.Locomotion.walkSpeed)
            {
                MagicianChar.Locomotion.moveSpeed -= 1 * MagicianChar.Locomotion.acceleration *  Time.deltaTime;

            }
        }
        
            MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
        
    }

    public override void ExitState()
    {
        MagicianChar.anim.SetLayerWeight(1, 0);
        MagicianChar.anim.SetLayerWeight(2, 1);
        MagicianChar.Locomotion.moveSpeed =0;
        MagicianChar._mSFX.SFX.loop = false;
        MagicianChar._mSFX.SFX.Stop();
    }
    public override bool CanTransition()
    {
       
        return true;
        
    }
}
    

