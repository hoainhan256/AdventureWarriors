using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerLocomotion PlayerLocomotion;
    AnimatiorManager AnimatiorManager;
    Combat _Combat;
    blockSkill blockSkill;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        PlayerLocomotion = GetComponent<PlayerLocomotion>();    
        AnimatiorManager = GetComponent<AnimatiorManager>();
        blockSkill = GetComponent<blockSkill>();
        _Combat = GetComponent<Combat>();
    }
    private void Update()
    {
        inputManager.HandleAllInput();
        AnimatiorManager.PlayAnimation();
        blockSkill.BlockSkill();
        
    }
    private void FixedUpdate()
    {
       
        PlayerLocomotion.HandleAllMoverment();
    }
}
