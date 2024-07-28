using UnityEngine;

public class AttackState : CharacterState
{
    public AttackState(MagicianChar magicianChar) : base(magicianChar) { }

    public override void EnterState()
    {
        MagicianChar.anim.SetTrigger("isAttack");
        MagicianChar.timeFireBall = 0;

    }
    public override void UpdateState()
    {
        MagicianChar.timeFireBall += 1 * Time.deltaTime;
        if (MagicianChar.timeFireBall < 0.5f)
        {
            MagicianChar.Locomotion.RotateToCamera();
        }
        else if (MagicianChar.timeFireBall > 2f)
        {
            if (MagicianChar.InputManager.isBlock) MagicianChar.TransitionToState(MagicianChar.blockState);
            else if (MagicianChar.InputManager.isDodge ) MagicianChar.TransitionToState(MagicianChar.dodgeState);
            else if (MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);
            else if (MagicianChar.InputManager.isMoving) MagicianChar.TransitionToState(MagicianChar.moveState);
            else MagicianChar.TransitionToState(MagicianChar.idleState);
        }
        
    }
    public override void ExitState()
    {
        MagicianChar.InputManager.isAttack = false;
    }
}
