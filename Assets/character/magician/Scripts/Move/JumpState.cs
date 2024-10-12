using UnityEngine;

public class JumpState : CharacterState
{
    public JumpState(MagicianChar magicianChar) : base(magicianChar)
    {
    }
    
    public override void EnterState()
    {
       MagicianChar.anim.SetBool("isJump", true);
       
        MagicianChar.timeJumped = 0;
    }

   

    public override void UpdateState()
    {
        
        MagicianChar.anim.SetFloat("timeJump",MagicianChar.timeJumped);
        MagicianChar.anim.SetBool("isGround", !MagicianChar.isGround);
        
        MagicianChar.timeJumped += Time.deltaTime;
       
        
           
        
         if(MagicianChar.timeJumped >1f) MagicianChar.anim.SetBool("isJump", MagicianChar.InputManager.isJump);

        if (MagicianChar.isGround == false )
        {
            MagicianChar.InputManager.isJump = false;
        }
    }
    public override void ExitState()
    {
        MagicianChar.anim.SetBool("isJump", false);
        MagicianChar.anim.SetBool("isGround", false);
        MagicianChar.InputManager.isJump = false;
        MagicianChar.timeJumped = 0;
        MagicianChar.anim.SetFloat("timeJump", 0);

    }
    public override bool CanTransition()
    {
        if(MagicianChar.timeJumped > 1.2f)
        {
            return MagicianChar.isGround;
        }
        else return false;
        
    }
}
