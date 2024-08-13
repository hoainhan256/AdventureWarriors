using UnityEngine;

public class Dodge : CharacterState
{
    public Dodge(MagicianChar magicianChar) : base(magicianChar) { }
    public override void EnterState()
    {
        MagicianChar.InputManager.isDodge = false;
        MagicianChar.anim.SetTrigger("dodge");
        MagicianChar.MoveInfor = 0;
        MagicianChar.timeDodge = 0;
    }

    public override void UpdateState()
    {
        MagicianChar.timeDodge +=1* Time.deltaTime;

        MagicianChar.Locomotion.DodgePhysic();
        if (MagicianChar.timeDodge >= 0.65f)
        {
            MagicianChar.Locomotion.HandleRotation();
        }

    }
   
    public override void ExitState()
    { 
        MagicianChar.InputManager.isDodge = false;
        MagicianChar.timeDodge = 0;
        MagicianChar.Locomotion.rig.linearVelocity = Vector3.zero;
       
        MagicianChar.Locomotion.moveSpeed = 3f;
    }
    public override bool CanTransition()
    {
        if(MagicianChar.timeDodge > 0.9)
        {
            return true;
        }
        else return false;
    }
}
