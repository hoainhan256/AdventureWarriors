using UnityEngine;

public class Dodge : CharacterState
{
    public Dodge(MagicianChar magicianChar) : base(magicianChar) { }
    public override void EnterState()
    {
        MagicianChar.anim.SetTrigger("dodge");
        MagicianChar.MoveInfor = 0;
        MagicianChar.timeDodge = 0;
    }

    public override void UpdateState()
    {
        MagicianChar.timeDodge +=1* Time.deltaTime;
        MagicianChar.Locomotion.DodgePhysic();
        if (MagicianChar.timeDodge >= 0.8f)
        {
            if (MagicianChar.InputManager.isMoving)
            {
                MagicianChar.TransitionToState(MagicianChar.moveState);
            }
           
            else if (MagicianChar.InputManager.isBlock) MagicianChar.TransitionToState(MagicianChar.blockState);
            else if (MagicianChar.InputManager.isAttack) MagicianChar.TransitionToState(MagicianChar.attackState);
            else if (MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
            else
            {
                MagicianChar.TransitionToState(MagicianChar.idleState);
            }
        }
       
    }
   
    public override void ExitState()
    { 
        MagicianChar.InputManager.isDodge = false;
        MagicianChar.timeDodge = 0;
    }
}
