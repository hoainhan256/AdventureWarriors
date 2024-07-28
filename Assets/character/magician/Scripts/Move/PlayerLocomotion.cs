using UnityEngine;
using Unity.Cinemachine;
public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
 
    [SerializeField] Vector3 moveDirection;
    public Transform cameraObject { get; private set; }
    Rigidbody rig;
    public float moveSpeed = 5;
    public float ForceDodge = 500f;
    [SerializeField] float walkSpeed = 75;
    [SerializeField] float runSpeed = 150;
    public float rotationSpeed = 15f;
    public bool isCursorlock = true;
    [SerializeField] GameObject Cinemachine;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
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
        
        moveSpeed = inputManager.Spirits ? runSpeed : walkSpeed;
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * moveSpeed * Time.deltaTime;

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
        rig.linearVelocity = transform.forward * ForceDodge * Time.deltaTime;
    }
    public void RotateToCamera()
    {
        Vector3 CameraDir = cameraObject.forward;
        CameraDir.y = 0;
        Quaternion TargetRotation = Quaternion.LookRotation(CameraDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, rotationSpeed * Time.deltaTime);
    }
}
