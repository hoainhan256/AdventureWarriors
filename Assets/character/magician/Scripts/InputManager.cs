using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class InputManager : MonoBehaviour
{
    [Header("Class")]
    AnimatiorManager animatiorManager;
    PlayerControl playerControls;
    PlayerLocomotion locomotion;
    Combat combat;
    [Header("Input values")]
    public bool isFlameFire;
    public Vector2 moverInput { get; private set; }
    public float horizontalInput;
    public float verticalInput;
    public bool isDodge;
    
    public bool isBlock;
    public bool Spirits { get; private set; }
    private void Awake()
    {
        animatiorManager = GetComponent<AnimatiorManager>();
        combat = GetComponent<Combat>();
        locomotion = GetComponent<PlayerLocomotion>();


    }
    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControl();
            playerControls.Playermoverment.moverment.performed += i => moverInput = i.ReadValue<Vector2>();

            playerControls.Playermoverment.Spirit.performed += context =>
            {
                if (context.interaction is HoldInteraction)
                {
                    Spirits = true;
                    isFlameFire = false;
                }
                else if(context.interaction is TapInteraction)
                {
                    isDodge = true;
                    isFlameFire = false;
                    combat.isAttack = false;
                }
            };
            playerControls.Playermoverment.Spirit.canceled += context => Spirits = false;
            playerControls.Combat.block.started += context => { isBlock = true;  };
            playerControls.Combat.block.canceled += context => isBlock = false;
            playerControls.Combat.NormalAttack.performed += OnAttack;
            playerControls.Other.Cursor.performed += Escap;
            playerControls.Combat.NormalAttack.canceled += context => isFlameFire = false;
        }
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public void HandleAllInput()
    {
        moverInput.Normalize();
        
        HandleMovermentInput();
    }
    void HandleMovermentInput()
    {
        verticalInput = moverInput.y;
        horizontalInput = moverInput.x;
      
    }
    private void Update()
    {
        playerControls.Playermoverment.Spirit.performed += context =>
        {
            if (context.interaction is TapInteraction)
            {
                animatiorManager.PlayDodgeAnim();
                isFlameFire = false;
                //combat.Dodge();
                
            }

                
        };
    }
   private void OnAttack (InputAction.CallbackContext context)
    {
        if (!locomotion.isCursorlock) return;
        if (context.interaction is TapInteraction)
        {
            animatiorManager.PlayNorAttack();
        }
        else if(context.interaction is HoldInteraction)
        {
            isFlameFire = true;
            isBlock = false;
        }
       
        
        
        
    }
   void Escap(InputAction.CallbackContext context)
    {
       if(locomotion.isCursorlock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        isFlameFire = false;
       locomotion.isCursorlock = !locomotion.isCursorlock;
    }
}
