using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    MagicianChar Magician;
    private PlayerInput _playerInput;
    [SerializeField] MgCam cameraController;
    [SerializeField] Vector3 moveDirection;
    public Transform cameraObject { get; private set; }
    public Rigidbody rig;
    [Header("Float Varibles")]
    public float moveSpeed = 5;
    public float ForceDodge = 500f;
    public float JumpForce = 10f;
    public float walkSpeed  = 5;
    public float runSpeed  = 7;
    private float _cinemachineTargetPitch;
    public float acceleration = 2;
    public float rotationSpeed = 15f;
    private float _rotationVelocity;
    public bool isCursorlock = true;
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;


    private bool IsCurrentDeviceMouse

    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        Magician = GetComponent<MagicianChar>();
        rig = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        _playerInput = GetComponent<PlayerInput>();
        
    }
   
    public void HandleAllMoverment()
    {
        HandleMoverment(); HandleRotation();
       

        
    }
    private void FixedUpdate()
    {
    }
    private void LateUpdate()
    {
        
            firstCameraRotation();
        
    }
    private void firstCameraRotation()
    {
        if (!inputManager.isFirstPerson) return;
        // if there is an input
        if (inputManager.look.sqrMagnitude >= 0.01f)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetPitch += inputManager.look.y * (rotationSpeed / rotationSpeed) * deltaTimeMultiplier;
            _rotationVelocity = inputManager.look.x * (rotationSpeed / rotationSpeed)  * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
            
            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);
            // rotate the player left and right
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, _rotationVelocity, 0f));
            rig.MoveRotation(rig.rotation * deltaRotation);
        }
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    void firstPersonMove()
    {
        if (!inputManager.isFirstPerson) return;
        moveDirection = new Vector3(inputManager.moverInput.x,0.0f,inputManager.moverInput.y).normalized;
        if(inputManager.moverInput != Vector2.zero)
        {
            moveDirection = transform.right * inputManager.moverInput.x + transform.forward * inputManager.moverInput.y;
        }
        moveDirection = moveDirection * moveSpeed;
        moveDirection.y = rig.linearVelocity.y;
        Vector3 movermentVelocity = moveDirection;
        rig.linearVelocity = (movermentVelocity );
    }
    public void HandleMoverment()
    {

        if (!inputManager.isFirstPerson)
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed /** Time.deltaTime*/;

            moveDirection.y = rig.linearVelocity.y;

            Vector3 movermentVelocity = moveDirection;

            rig.linearVelocity = (movermentVelocity);
        }
        else
        {
            firstPersonMove();
        }
    }
   public void HandleRotation()
    {
        if (!inputManager.isFirstPerson)
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotion = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotion;
        }

    }
    public void DodgePhysic()
    {
        Vector3 dir = transform.forward * ForceDodge;
        dir.y = rig.linearVelocity.y;
        rig.linearVelocity = dir;
       
    }
    public void RotateToCamera(float SpeedRotate)
    {
        Vector3 CameraDir = cameraObject.forward;
        CameraDir.y = 0;
        Quaternion TargetRotation = Quaternion.LookRotation(CameraDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, SpeedRotate * Time.deltaTime);
    }
   
    public void JumpPhysic()
    {
        Vector3 dir;
        dir = transform.forward;
        dir.y = 1;
        Debug.Log(dir);
        dir.Normalize();
        dir = dir * JumpForce * Time.fixedDeltaTime;
        rig.linearVelocity =(dir);
        inputManager.isJump = false;
    }
    public void RotateToInput()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        
        transform.rotation = targetRotation;
    }
}
