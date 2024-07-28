using UnityEngine;

public class blockSkill : CharacterState
{
    public blockSkill(MagicianChar magicianChar) : base(magicianChar) { }

    public override void EnterState()
    {
        MagicianChar.ShieldObject.SetActive(true);
        MagicianChar.anim.SetBool("Block", true);
        MagicianChar.MoveInfor = 0;
    }

    
    public override void UpdateState()
    {
       
        MagicianChar.Locomotion.RotateToCamera();

        if(MagicianChar.InputManager.isDodge)
        {
            MagicianChar.TransitionToState(MagicianChar.dodgeState);
        }
        else if(MagicianChar.InputManager.isAttack) MagicianChar.TransitionToState(MagicianChar.attackState);
        else if(MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
        else if(MagicianChar.InputManager.isBlock == false)
        {
            if (MagicianChar.InputManager.isMoving)
            {
                MagicianChar.TransitionToState(MagicianChar.moveState);
            }
            else
            {
                MagicianChar.TransitionToState(MagicianChar.idleState);
            }
        }
    }
    public override void ExitState()
    {
        MagicianChar.ShieldObject.SetActive(false);
        MagicianChar.InputManager.isBlock = false;

        MagicianChar.anim.SetBool("Block", false);
    }

}
