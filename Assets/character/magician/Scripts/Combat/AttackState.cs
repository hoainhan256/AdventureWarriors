using UnityEngine;

public class AttackState : CharacterState
{
    public AttackState(MagicianChar magicianChar) : base(magicianChar) { }
    public override void EnterState()
    {
        MagicianChar.anim.SetLayerWeight(2, 1);
        MagicianChar.anim.SetTrigger("isAttack");
        MagicianChar.timeFireBall = 0;

    }
    public override void UpdateState()
    {
        MagicianChar.timeFireBall += 1 * Time.deltaTime;
       
        if (MagicianChar.InputManager.isMoving)
        {
            
            MagicianChar.TransitionToState(MagicianChar.moveAttack);
        }
        
        if (MagicianChar.timeFireBall < 0.5f)
        {
            MagicianChar.Locomotion.RotateToCamera(MagicianChar.Locomotion.rotationSpeed * 2 );
        }
        else if (MagicianChar.timeFireBall > 2f)
        {
            if (MagicianChar.InputManager.isBlock) MagicianChar.TransitionToState(MagicianChar.blockState);
            else if (MagicianChar.InputManager.isDodge) MagicianChar.TransitionToState(MagicianChar.dodgeState);

            else if (MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
           
            else MagicianChar.TransitionToState(MagicianChar.idleState);
        }
        
    }
    public override void ExitState()
    {
        MagicianChar.InputManager.isAttack = false;
    }
    public override bool CanTransition()
    {
        if(MagicianChar.timeFireBall >= 2)
        {
            return true;
        }
        else return false;
    }
}
