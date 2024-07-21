using UnityEngine;

public class Combat : MonoBehaviour
{
    InputManager inputManager;
    Rigidbody rb;
    PlayerLocomotion PlayerLocomotion;
    [SerializeField] float ForceDogde = 50f;
    public float dodgeDuration = .5f;
    public bool isDodging = false;
    public bool isAttack;
    Camera mainCam;
    [SerializeField] int AttackType = 0;
    [Header("Fire Ball")]
    public GameObject FireBall;
    Transform fireBallPos;
    [SerializeField] float FireBallSpeed = 100f;
    [Header("FireFlame")]
    [SerializeField] GameObject FireFlameObject;
    CapsuleCollider[] capsuleCollider;
    private void Awake()
    {
        mainCam = Camera.main;
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        PlayerLocomotion = GetComponent<PlayerLocomotion>();
        fireBallPos = transform.GetChild(1);
        FireFlameObject = transform.GetChild(3).gameObject;
        capsuleCollider = FireFlameObject.GetComponents<CapsuleCollider>();
    }
    public void Dodge()
    {
        isDodging=true;
        Invoke("UnDodge", 0.45f);
    }
    private void Update()
    {


        if (isAttack|| inputManager.isFlameFire)
        {
            Vector3 cameraForward = mainCam.transform.forward;
            cameraForward.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            Quaternion SmoothRotate = Quaternion.Slerp(transform.rotation, targetRotation, PlayerLocomotion.rotationSpeed * Time.deltaTime);
            transform.rotation = SmoothRotate;


        }
        if( FireFlameObject.activeSelf)
        {
            if ( inputManager.isDodge)
            {
                FireFlameObject.SetActive(false);
                foreach (CapsuleCollider i in capsuleCollider)
                {
                    i.enabled = true;
                }

            }
            
        }
    }
    private void FixedUpdate()
    {
        if (isDodging && dodgeDuration >0)
        {
            dodgeDuration -=Time.deltaTime;
            rb.linearVelocity = transform.forward * ForceDogde;
        }
        if(dodgeDuration <= 0)
        {
            isDodging = false;
            dodgeDuration = 0.45f;
        }
    }
    void UnDodge()
    {
        inputManager.isDodge = false;

    }
    public void Attack()
    {
        
        if (AttackType == 0)
        {
            Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPos;
            if (Physics.Raycast(ray, out hit))
            {
                targetPos = hit.point;
            }
            else
            {
                targetPos = ray.GetPoint(10000);
            }
            GameObject skillEffect = Instantiate(FireBall, fireBallPos.transform.position, Quaternion.identity);

            skillEffect.GetComponent<Rigidbody>().linearVelocity =((targetPos - fireBallPos.position).normalized * FireBallSpeed * Time.deltaTime);
            Destroy(skillEffect, 4f);
        }
       
    }
    public void OnAttack()
    {
        isAttack = true;
    }
    public void UnAttack()
    {
        isAttack = false;
    }
    public void FireFlame()
    {
        if (!isAttack)
        {
            isAttack = true;
        }
        if(FireFlameObject.activeSelf == false)
        FireFlameObject.SetActive(true);
        foreach(CapsuleCollider i in capsuleCollider)
        {
            i.enabled = true;
        }
        
        Invoke("LoopHitFireFlame", 0.2f);
    }
    public void unActiveFireFlame()
    {
        isAttack=false;
        FireFlameObject.SetActive(false);
        foreach (CapsuleCollider i in capsuleCollider)
        {
            i.enabled = true;
        }
    }
    void LoopHitFireFlame()
    {
        foreach (CapsuleCollider i in capsuleCollider)
        {
            i.enabled = false;
        }
    }
}
