using System.Collections;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class MoveState : CharacterState
{
    public MoveState(MagicianChar magicianChar) : base(magicianChar) { }
    public override void EnterState()
    {
        MagicianChar.syncSFX.FootStep();
        if(MagicianChar.currentStateString == MagicianChar.idleState.ToString())
        {
            MagicianChar.MoveInfor = 0;
        }
      
        
            MagicianChar.anim.SetLayerWeight(2, 0);
    }

    public override void UpdateState()
    {

        if (MagicianChar.InputManager.Spirits && MagicianChar.syncSFX._moverAudio.clip == MagicianChar.syncSFX._SFX.footStep)
        {
            MagicianChar.syncSFX._moverAudio.clip = MagicianChar.syncSFX._SFX.running;
            MagicianChar.syncSFX._moverAudio.Play();
        }
        else if (MagicianChar.syncSFX._moverAudio.clip == MagicianChar.syncSFX._SFX.running && MagicianChar.InputManager.Spirits == false)
        {
            MagicianChar.syncSFX._moverAudio.clip = MagicianChar.syncSFX._SFX.footStep;
            MagicianChar.syncSFX._moverAudio.Play();

        }
       
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
        MagicianChar.anim.SetLayerWeight(2,0);
        if(MagicianChar.nextStateString != MagicianChar.jumpState.ToString())
        {
            MagicianChar.Locomotion.moveSpeed = 3;
        }
        if (MagicianChar.nextStateString != MagicianChar.moveAttack.ToString() || MagicianChar.nextStateString != MagicianChar.moveState.ToString())
        {
            MagicianChar.syncSFX._moverAudio.Stop();
        }
        
    }
    public override bool CanTransition()
    {
       
        return true;
        
    }
}
    

