using UnityEngine;

public class FlameState : CharacterState
{
   public FlameState(MagicianChar magicianChar) : base(magicianChar)
    {
    }

    public override void EnterState()
    {
        MagicianChar.anim.SetBool("flameFire", true);
        MagicianChar.MoveInfor = 0;
        MagicianChar.timeFlame = 0;
        
    }

   

    public override void UpdateState()
    {
        MagicianChar.Locomotion.RotateToCamera(10);
        MagicianChar.timeFlame += 1 * Time.deltaTime;
        if (MagicianChar.timeFlame > 1f)
        {
            MagicianChar.FlameFireObject.SetActive(true);
        }
        if (MagicianChar.InputManager.isDodge) MagicianChar.TransitionToState(MagicianChar.dodgeState);
        
       
        

    }
    public override void ExitState()
    {
        MagicianChar.FlameFireObject.SetActive(false);
        MagicianChar.InputManager.isFlame = false;
        MagicianChar.anim.SetBool("flameFire", false);
    }
    public override bool CanTransition()
    {
        if (MagicianChar.timeFlame > 3.7f)
        {
            return true;
        }
        else return false;
    }
}
