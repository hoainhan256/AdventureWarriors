using UnityEngine;

public class MoveState : CharacterState
{
    public MoveState(MagicianChar magicianChar) : base(magicianChar) { }
    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (MagicianChar.InputManager.isMoving == false)
        {
            MagicianChar.TransitionToState(MagicianChar.idleState);

        }
        else if(MagicianChar.InputManager.isDodge)
        {
            MagicianChar.TransitionToState(MagicianChar.dodgeState);
        }
        else if (MagicianChar.InputManager.isBlock)
        {
            MagicianChar.TransitionToState(MagicianChar.blockState);

        }
        else if (MagicianChar.InputManager.isAttack) MagicianChar.TransitionToState(MagicianChar.attackState);
        else if (MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
        MagicianChar.Locomotion.HandleAllMoverment();
        if (MagicianChar.InputManager.Spirits)
        {
            if (MagicianChar.MoveInfor <= 2.5f)
            {
                MagicianChar.MoveInfor += MagicianChar.acceleration * Time.deltaTime;
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
        }
        
            MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
        
    }

    public override void ExitState() { }
}
    

