using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
public class InputManager : MonoBehaviour
{
    [Header("Class")]
  
    PlayerControl playerControls;

    [Header("Input values")]
    public Vector2 moverInput { get; private set; }
    public float horizontalInput;
    public float verticalInput;
    public bool isMoving;
    public bool isDodge;
    public bool isAttack = false;
    public bool isFlame = false;
    public bool isBlock;
    public bool isJump;
    public bool isCrouch;
    public bool Spirits { get; private set; }
    private void Awake()
    {
      


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
                }
                else if(context.interaction is TapInteraction)
                {
                    isDodge = true;
                  
                }
            };
            playerControls.Playermoverment.Spirit.canceled += context => Spirits = false;
            playerControls.Combat.block.started += context => { isBlock = true;  };
            playerControls.Combat.block.canceled += context => isBlock = false;
            playerControls.Combat.NormalAttack.performed += OnAttack;
            playerControls.Other.Cursor.performed += Escap;
            playerControls.Combat.NormalAttack.canceled += context => isFlame = false;
            playerControls.Playermoverment.Jump.started += context => isJump = true;
            playerControls.Playermoverment.Crouch.performed += context => { isCrouch = isCrouch ? false : true; };
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
        if(verticalInput != 0 || horizontalInput != 0)
        {
            isMoving = true;
        }
        else isMoving = false;
      
    }
    private void Update()
    {
        
    }
   private void OnAttack (InputAction.CallbackContext context)
    {
        if(context.interaction is HoldInteraction)
        {
            isFlame = true;
        }
        else if(context.interaction is TapInteraction)
        {
            isAttack = true;
        }
        
        
        
    }
   void Escap(InputAction.CallbackContext context)
    {
       
    }
}
