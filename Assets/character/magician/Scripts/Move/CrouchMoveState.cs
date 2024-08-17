using UnityEngine;

public class CrouchMoveState : CharacterState
{


    public CrouchMoveState(MagicianChar magicianChar) : base(magicianChar)
    {
    }

    public override bool CanTransition()
    {
        return true;
    }

    public override void EnterState()
    {
       

    }

    public override void UpdateState()
    {
        if (MagicianChar.InputManager.isCrouch == false)
        {
            MagicianChar.TransitionToState(MagicianChar.moveState);
            MagicianChar.anim.SetBool("isCrouch", false);
        }
        MagicianChar.Locomotion.HandleAllMoverment();
        if (MagicianChar.MoveInfor < 1f)
        {
            MagicianChar.MoveInfor += MagicianChar.acceleration * Time.deltaTime;
        }
        else if (MagicianChar.MoveInfor >= 1f && MagicianChar.MoveInfor >0)
        {
            MagicianChar.MoveInfor -= MagicianChar.acceleration * Time.deltaTime;
        }
        if (!MagicianChar.InputManager.isMoving)
        {
            MagicianChar.TransitionToState(MagicianChar.crouchIdleState);
        }
        MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
    }
    public override void ExitState()
    {
        if(MagicianChar.nextStateString != MagicianChar.crouchIdleState.ToString())
        {
            MagicianChar.anim.SetBool("isCrouch", false);
            MagicianChar.InputManager.isCrouch = false;
        }
    }
}
