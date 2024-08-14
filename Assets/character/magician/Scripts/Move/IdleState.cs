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
        
        
        if (MagicianChar.MoveInfor > 0)
        {
            MagicianChar.MoveInfor -= MagicianChar.acceleration * Time.deltaTime;
        }
        MagicianChar.anim.SetFloat("Speed", MagicianChar.MoveInfor);
       
    }

    public override void ExitState()
    {
        MagicianChar.saveMoveInfor = 0f;
    }
    public override bool CanTransition()
    {
        return true;
    }
}
