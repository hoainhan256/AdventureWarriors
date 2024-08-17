using UnityEngine;
using Unity.Cinemachine;
public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    MagicianChar Magician;
    [SerializeField] Vector3 moveDirection;
    public Transform cameraObject { get; private set; }
    public Rigidbody rig;
    public float moveSpeed = 5;
    public float ForceDodge = 500f;
    public float JumpForce = 10f;
    [SerializeField] public float walkSpeed { get; private set; } = 5;
    [SerializeField] public float runSpeed { get; private set; } = 7;
    public float acceleration = 2;
    public float rotationSpeed = 15f;
    public bool isCursorlock = true;
    [SerializeField] GameObject Cinemachine;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        Magician = GetComponent<MagicianChar>();
        rig = GetComponent<Rigidbody>();
        Cinemachine = transform.GetChild(2).gameObject;
        cameraObject = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void HandleAllMoverment()
    {
        HandleMoverment(); HandleRotation();
        if (!isCursorlock )
        {
            Cinemachine.GetComponent<CinemachineOrbitalFollow>().enabled = false;
        }
        else if(isCursorlock) 
        {
            Cinemachine.GetComponent<CinemachineOrbitalFollow>().enabled = true;
        }

    }
    public void HandleMoverment()
    {
        
       
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection = moveDirection * moveSpeed /** Time.deltaTime*/;

        moveDirection.y = rig.linearVelocity.y;

        Vector3 movermentVelocity = moveDirection;
        rig.linearVelocity = movermentVelocity;
    }
   public void HandleRotation()
    {
            Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection== Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotion = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotion;
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
        if (Magician.saveMoveInfor != 0)
        {
            dir = transform.forward;
        }
        else
        {
            dir = new Vector3();
        }
        dir.Normalize();
        dir.y = 1;
        rig.AddForce(dir * JumpForce * Time.fixedDeltaTime , ForceMode.Impulse);
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
