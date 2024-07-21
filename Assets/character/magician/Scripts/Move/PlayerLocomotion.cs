using UnityEngine;
using Unity.Cinemachine;
public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Combat combat;
    [SerializeField] Vector3 moveDirection;
    public Transform cameraObject { get; private set; }
    Rigidbody rig;
    public float moveSpeed = 5;
    float walkSpeed = 75;
    float runSpeed = 150;
    public float rotationSpeed = 15f;
    public bool isCursorlock = true;
    [SerializeField] GameObject Cinemachine;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rig = GetComponent<Rigidbody>();
        combat = GetComponent<Combat>();
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
    void HandleMoverment()
    {
        if (inputManager.isBlock || combat.isAttack || inputManager.isFlameFire)
        {
            rig.linearVelocity = Vector3.zero;
            return;
        }
        moveSpeed = inputManager.Spirits ? runSpeed : walkSpeed;
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * moveSpeed * Time.deltaTime;

        Vector3 movermentVelocity = moveDirection;
        rig.linearVelocity = movermentVelocity;
    }
    void HandleRotation()
    {

        if (inputManager.isBlock || combat.isAttack || isCursorlock == false|| inputManager.isFlameFire) return;
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

}
