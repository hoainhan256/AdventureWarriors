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
       
        MagicianChar.Locomotion.RotateToCamera(MagicianChar.Locomotion.rotationSpeed);

    }
    public override void ExitState()
    {
        MagicianChar.ShieldObject.SetActive(false);
        MagicianChar.InputManager.isBlock = false;
        MagicianChar.anim.SetBool("Block", false);
       
    }
    public override bool CanTransition()
    {
        return true;
    }
}
