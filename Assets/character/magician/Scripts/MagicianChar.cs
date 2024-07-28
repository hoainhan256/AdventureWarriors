using UnityEngine;
public class MagicianChar : MonoBehaviour
{
    [Header("Class")]
    public Animator anim;
    public InputManager InputManager;
    public PlayerLocomotion Locomotion;
    [Header("Object")]
    public GameObject ShieldObject { get; private set; }
    public GameObject FlameFireObject { get; private set; }
    [Header("State..")]
    public ICharacterState currentState;
    public blockSkill blockState;
    public IdleState idleState;
    public MoveState moveState;
    public Dodge dodgeState;
    public AttackState attackState;
    public FlameState flameState;
    [Header("Infor")]
    public float MoveInfor = 0;
    public float timeDodge = 0;
    public float acceleration = 1.2f;
    public float timeFireBall = 1.5f;
    public float timeFlame = 0;
    private void Awake()
    {
        InputManager = GetComponent<InputManager>();
        anim = GetComponent<Animator>();
        Locomotion = GetComponent<PlayerLocomotion>();
        ShieldObject = transform.GetChild(0).gameObject;

        FlameFireObject = transform.GetChild(3).gameObject;
        blockState = new blockSkill(this);
        idleState = new IdleState(this);
        moveState = new MoveState(this);
        dodgeState = new Dodge(this);
        attackState = new AttackState(this);
        flameState = new FlameState(this);
    }
    private void Start()
    {
        currentState = idleState;
        currentState.EnterState();
        FlameFireObject.SetActive(false);

    }
    private void Update()
    {
        InputManager.HandleAllInput();
        currentState.UpdateState();
        MoveInfor = Mathf.Clamp(MoveInfor, 0, 3);
        Debug.Log("State:"+ currentState.GetType().Name);
    }
    public void TransitionToState(ICharacterState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
