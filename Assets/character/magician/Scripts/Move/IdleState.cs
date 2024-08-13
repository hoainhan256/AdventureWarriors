using UnityEngine;
using UnityEngine.TextCore.Text;

public class IdleState : CharacterState
{
    
    public IdleState(MagicianChar magicianChar) : base(magicianChar) { }

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        
        //if (MagicianChar.InputManager.isMoving)
        //{
        //    MagicianChar.TransitionToState(MagicianChar.moveState);
            
        //}
        //else if (MagicianChar.InputManager.isDodge)
        //{
        //    MagicianChar.TransitionToState(MagicianChar.dodgeState);
        //}
        //else if(MagicianChar.InputManager.isBlock)
        //{
        //    MagicianChar.TransitionToState(MagicianChar.blockState);

        //}
        //else if(MagicianChar.InputManager.isAttack) MagicianChar.TransitionToState(MagicianChar.attackState);
        //else if(MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
        if (MagicianChar.MoveInfor > 0)
        {
            MagicianChar.MoveInfor -= MagicianChar.acceleration * Time.deltaTime;
        }
        MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
       
    }

    public override void ExitState()
    {

    }
    public override bool CanTransition()
    {
        return true;
    }
}
