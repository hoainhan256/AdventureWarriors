using UnityEngine;

public class CrouchIdleState : CharacterState
{
    public CrouchIdleState(MagicianChar magicianChar) : base(magicianChar)
    {
    }

    public override bool CanTransition()
    {
        return true;

    }

    public override void EnterState()
    {
      

       
        MagicianChar.anim.SetBool("isCrouch", true);
    }

    public override void UpdateState()
    {
        if (MagicianChar.MoveInfor > 0)
        {
            MagicianChar.MoveInfor -= MagicianChar.acceleration * Time.deltaTime;
        }
        if (MagicianChar.InputManager.isMoving)
        {
            MagicianChar.TransitionToState(MagicianChar.crouchMoveState);
        }
        MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
        if(MagicianChar.InputManager.isCrouch == false)
        {
            MagicianChar.TransitionToState(MagicianChar.idleState);
            MagicianChar.anim.SetBool("isCrouch", false);
        }
    }
    public override void ExitState()
    {
        if(MagicianChar.nextStateString != MagicianChar.crouchMoveState.ToString())
        {
            MagicianChar.anim.SetBool("isCrouch", false);
            MagicianChar.InputManager.isCrouch = false;
        }
        Debug.Log("Exit crouch");
        MagicianChar.saveMoveInfor = 0f;
    }
}
