using UnityEngine;

public abstract class CharacterState 
{
    protected MagicianChar MagicianChar;
    public CharacterState (MagicianChar magicianChar)
    {
        this.MagicianChar = magicianChar;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract bool CanTransition();
}
public interface ICharacterState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}
