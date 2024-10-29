using UnityEngine;
[RequireComponent(typeof(SyncSFX))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerLocomotion))]
public class MagicianChar : MonoBehaviour
{
    [Header("Class")]
    public Animator anim;
    public InputManager InputManager;
    public PlayerLocomotion Locomotion;
    public SyncSFX syncSFX;
    [Header("Object")]
    public GameObject ShieldObject;
    public GameObject FlameFireObject;
    [Header("State..")]
    public CharacterState currentState;
    public blockSkill blockState;
    public IdleState idleState;
    public MoveState moveState;
    public Dodge dodgeState;
    public AttackState attackState;
    public FlameState flameState;
    public MoveAttack moveAttack;
    public JumpState jumpState;
    public CrouchIdleState crouchIdleState;
    public CrouchMoveState crouchMoveState;
    public string currentStateString;
    public string nextStateString;
    [Header("Infor")]
    public float MoveInfor = 0;
    public float timeDodge = 0;
    public float acceleration = 1.2f;
    public float timeFireBall = 1.5f;
    public float timeFlame = 0;
    public bool isGround;
    public float timeJumped;
    public float radiusCheck = 0.2f;
    public float saveMoveInfor;
    [SerializeField] float transformCheck;
    
    [SerializeField] LayerMask GroundLayer;
    private void Awake()
    {
        
        
        
         blockState = new blockSkill(this);
        idleState = new IdleState(this);
        moveState = new MoveState(this);
        dodgeState = new Dodge(this);
        attackState = new AttackState(this);
        flameState = new FlameState(this);
        moveAttack = new MoveAttack(this);
        jumpState = new JumpState(this);
        crouchIdleState = new CrouchIdleState(this);
        crouchMoveState = new CrouchMoveState(this);
    }
    private void Start()
    {
        currentState = idleState;
        currentState.EnterState();
        FlameFireObject.SetActive(false);


    }
    private void FixedUpdate()
    {
        InputManager.HandleAllInput();
        currentState.UpdateState();
        MoveInfor = Mathf.Clamp(MoveInfor, 0, 3);
       
    }
    public void TransitionToState(CharacterState newState)
    {
        nextStateString = newState.ToString();
        currentStateString = currentState.ToString();
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
    void DebugDrawSphere(Vector3 center, float radius, Color color)
    {
        
        float step = 10.0f;
        for (float theta = 0; theta < 360; theta += step)
        {
            float x1 = radius * Mathf.Cos(theta * Mathf.Deg2Rad);
            float y1 = radius * Mathf.Sin(theta * Mathf.Deg2Rad);
            float x2 = radius * Mathf.Cos((theta + step) * Mathf.Deg2Rad);
            float y2 = radius * Mathf.Sin((theta + step) * Mathf.Deg2Rad);

          
            Debug.DrawLine(center + new Vector3(x1, y1, 0), center + new Vector3(x2, y2, 0), color);

            
            Debug.DrawLine(center + new Vector3(x1, 0, y1), center + new Vector3(x2, 0, y2), color);

           
            Debug.DrawLine(center + new Vector3(0, x1, y1), center + new Vector3(0, x2, y2), color);
        }
    }
    private void Update()
    {
        CheckState();
        isGround = Physics.CheckSphere(transform.position, radiusCheck, GroundLayer);
        DebugDrawSphere(transform.position, radiusCheck, Color.red);

    }
    void CheckState()
    {
        if (isGround == false) return;
            #region BlockState
            if (InputManager.isBlock && !InputManager.isAttack && !InputManager.isDodge )
            {
            if (currentState == blockState) return;
            if (currentState == idleState || currentState == moveState )
            {
                TransitionToState(blockState);
            }
            else
            {
                if (currentState.CanTransition())
                {
                    TransitionToState(blockState);
                }
            }
           
        }        
        #endregion

            #region dodge State
        else if (InputManager.isDodge)
        {
            
            if(currentState == attackState || currentState == moveAttack || currentState == dodgeState || currentState == jumpState)
            {
                if (currentState.CanTransition())
                {
                    TransitionToState(dodgeState);
                }
            }
            else
            {
                TransitionToState(dodgeState);
            }
            saveMoveInfor = MoveInfor;
        }
        #endregion
        #region Flame State
        else if (InputManager.isFlame)
        {
            if (currentState == idleState || currentState == moveState || currentState == blockState)
            {
                TransitionToState(flameState);
            }
            else if (currentState == attackState || currentState == dodgeState || currentState == moveAttack || currentState == jumpState)
            {
                if (currentState.CanTransition())
                {
                    TransitionToState(flameState);
                }
            }
            saveMoveInfor = Locomotion.moveSpeed;
        }
        #endregion
        #region Attack State
        else if (InputManager.isAttack)
        {
            if (currentState == idleState  || currentState == blockState)
            {
                InputManager.isBlock = false;
                TransitionToState(attackState);
            }
            else if (currentState == moveState || currentState == moveAttack)
            {

                TransitionToState(moveAttack);
            }
            else 
            {
                if (currentState.CanTransition())
                {
                    TransitionToState(attackState);
                }
            }
            
        }
        #endregion
        #region Move State
        else if (InputManager.isMoving)
        {
            if (InputManager.isJump == true)
            {
                TransitionToState(jumpState);
            }
            if (currentState == crouchIdleState || currentState == crouchMoveState || currentState == moveState) return;
            if (currentState == attackState)
            {
                TransitionToState(moveAttack);
            }
            else if (currentState == idleState) TransitionToState(moveState);
            else 
            {
                if (currentState.CanTransition())
                {
                    TransitionToState(moveState);
                }
            }
            
        }
        #endregion

        #region JumpState
        else if(InputManager.isJump == true)
        {
            if (currentState.CanTransition())
            {
                Debug.Log((currentState.CanTransition()));
                TransitionToState(jumpState);
                InputManager.isJump =false;
                
            }

            

        }
        #endregion

        #region Crouch Ilde State
        else if (InputManager.isCrouch)
        {
            if(currentState == crouchIdleState || currentState == crouchMoveState) return;
            if (currentState.CanTransition())
            {
                TransitionToState(crouchIdleState);
            }
        }
        #endregion

        #region Crouch Move State

        #endregion

        #region Ilde State
        else
        {
            if (currentState == idleState) return;     
            if(currentState == crouchIdleState) return;
            if (currentState.CanTransition()) TransitionToState(idleState);  
        }
        #endregion

       
    }

}
