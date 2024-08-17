using UnityEngine;

public class MoveAttack : CharacterState
{
    public MoveAttack(MagicianChar magicianChar) : base(magicianChar)
    {
    }
    public override void EnterState()
    {

        MagicianChar.anim.SetLayerWeight(2, 0);
        MagicianChar.anim.SetLayerWeight(1, 1);
        MagicianChar.Locomotion.moveSpeed = 4f;
       
        if (MagicianChar.currentStateString == MagicianChar.moveState.ToString())
        {
            MagicianChar.anim.SetTrigger("isAttack");
        }
    }

    

    public override void UpdateState()
    {
        
        MagicianChar.timeFireBall += 1 * Time.deltaTime;
        MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
        MagicianChar.Locomotion.HandleAllMoverment();
        if (!MagicianChar.InputManager.isMoving)
        {

            MagicianChar.anim.SetLayerWeight(2, 1);
        }
        if (MagicianChar.InputManager.Spirits)
        {
            if (MagicianChar.MoveInfor <= 2.5f)
            {
                MagicianChar.MoveInfor += MagicianChar.acceleration * Time.deltaTime;
            }
            if (MagicianChar.Locomotion.moveSpeed < MagicianChar.Locomotion.runSpeed)
            {
                MagicianChar.Locomotion.moveSpeed += 1 * MagicianChar.Locomotion.acceleration * Time.deltaTime;
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
            else if (MagicianChar.Locomotion.moveSpeed > MagicianChar.Locomotion.walkSpeed)
            {
                MagicianChar.Locomotion.moveSpeed -= 1 * MagicianChar.Locomotion.acceleration * Time.deltaTime;

            }
        }
        if (MagicianChar.timeFireBall > 2f)
        {
            MagicianChar.InputManager.isAttack = false;
            if (MagicianChar.InputManager.isMoving)
            {

                MagicianChar.TransitionToState(MagicianChar.moveState);
            }
            else if (MagicianChar.InputManager.isBlock) MagicianChar.TransitionToState(MagicianChar.blockState);
            else if (MagicianChar.InputManager.isDodge) MagicianChar.TransitionToState(MagicianChar.dodgeState);

            else if (MagicianChar.InputManager.isFlame) MagicianChar.TransitionToState(MagicianChar.flameState);

            else MagicianChar.TransitionToState(MagicianChar.idleState);
        }
    }

    public override void ExitState()
    {
        MagicianChar.timeFireBall = 0;
       
       
    }
    public override bool CanTransition()
    {
        if (MagicianChar.timeFireBall >= 2f) return true;
        else return false;

    }
}
