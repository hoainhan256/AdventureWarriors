using UnityEngine;

public class Combat : MonoBehaviour
{
    public ObjectPooling TrailobjectPool;
    [SerializeField] Transform FirePos;
    [SerializeField] Vector3 destination;
    [SerializeField] Camera cam;
    [SerializeField] int Speed = 100;
    public MagicianChar magicianChar;
    Ray ray;
    RaycastHit hit;
    private void Awake()
    {
        if (TrailobjectPool == null)
        {
            TrailobjectPool = GameObject.FindGameObjectWithTag("TrailPooling").GetComponent<ObjectPooling>();
        }
    }
    private void Update()
    {
       
    }
    public void ThrowSpell()
    {

        ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(100);
        }
        GameObject objPrefab = TrailobjectPool.GetObject();
        objPrefab.transform.localPosition = FirePos.transform.position;
        objPrefab.transform.rotation = Quaternion.identity;
        objPrefab.GetComponent<Rigidbody>().linearVelocity =((destination - FirePos.position ).normalized * Speed) ;
        magicianChar.InputManager.isAttack = false;
    }
}
