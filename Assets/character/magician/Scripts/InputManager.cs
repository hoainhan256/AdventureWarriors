using UnityEngine;
using System.Collections;
using System;



#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
#endif
public class InputManager : MonoBehaviour
{
    [Header("Class")]
  
    PlayerControl playerControls;

    [Header("Input values")]
    public Vector2 moverInput { get; private set; }
    public Vector2 look;
    public float horizontalInput;
    public float verticalInput;
    public bool isMoving;
    public bool isDodge;
    public bool isAttack = false;
    public bool isFlame = false;
    public bool isBlock;
    public bool isJump;
    public bool isCrouch;
    public bool isSprint;
    public bool isFirstPerson = true;
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
    public bool _interact;
    public bool rotateLeft;
    public bool rotateRight;
    [Header("Movement Settings")]
    public bool analogMovement;

    public bool Spirits { get; private set; }
    private void Awake()
    {
      


    }
    
    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }
    //private void OnApplicationFocus(bool hasFocus)
    //{
    //    SetCursorState(cursorLocked);
    //}

    //private void SetCursorState(bool newState)
    //{
    //    Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    //}
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
            playerControls.Playermoverment.Jump.performed += context => { StartCoroutine(setFalseVaribleInput(() => isJump = true, () => isJump = false)); };
            playerControls.Playermoverment.Crouch.performed += context => { isCrouch = isCrouch ? false : true; };
            playerControls.Playermoverment.Spirit.canceled += context => Spirits = false;
            playerControls.Combat.block.started += context => { isBlock = true;  };
            playerControls.Combat.block.canceled += context => isBlock = false;
            playerControls.Combat.NormalAttack.performed += OnAttack;
            playerControls.Combat.NormalAttack.canceled += context => isFlame = false;
            playerControls.Other.Cursor.performed += Escap;
            playerControls.Other.ChangePerSpective.performed += context => { isFirstPerson = !isFirstPerson; };
            playerControls.Other.interact.started += OnInteract => { StartCoroutine(setFalseVaribleInput(() => _interact = true, () => _interact = false)); };
            playerControls.Other.RotateLeft.started += context => { rotateLeft = true; };
            playerControls.Other.RotateLeft.canceled += context => { rotateLeft = false; };
            playerControls.Other.RotateRight.started += context => { rotateRight = true; };
            playerControls.Other.RotateRight.canceled += context => { rotateRight = false; };




        }
        playerControls.Enable();
    }
    
    IEnumerator setFalseVaribleInput(Action setTrue, Action setFalse)
    {
        setTrue();
        yield return null ;
        setFalse();
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
   
#if ENABLE_INPUT_SYSTEM
    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }
 
#endif
    private void OnAttack (InputAction.CallbackContext context)
    {
        if(context.interaction is HoldInteraction)
        {
            isFlame = true;
        }
        else if(context.interaction is TapInteraction)
        {
            StartCoroutine(setFalseVaribleInput(() => isAttack = true, () => isAttack = false));
        }
        
        
        
    }
   void Escap(InputAction.CallbackContext context)
    {
       
    }
}
