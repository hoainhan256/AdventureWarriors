using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public Vector3 place;
    [SerializeField] RaycastHit _hit;
    public GameObject objectToPlace, tempObject;
    public GameObject wall_Corner, tempCorner;
    public bool placeNow;
    public bool place_Wall_Corner;
    public bool tempObjectExist;
    [Header("Rotate")]
    public bool _rotateLeft, _rotateRight;
    [SerializeField] float speedRotate = 1f;
    [Header("class")]
    [SerializeField] InputManager input;
    private void Awake()
    {
        input = FindFirstObjectByType<InputManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Place
        if (placeNow)
        {
            SendRay();
        }
        if (place_Wall_Corner)
        {
            objectToPlace = wall_Corner;
        }
        if (input._interact)
        {
            PlaceWallCorner();
        }
        #endregion
        #region Rotate
        if(input.rotateLeft)
        {
            RotateLeft();
        }
        if (input.rotateRight)
        {
            RotateRight();
        }
        #endregion
    }
    public void RotateLeft()
    {
        tempObject.transform.Rotate(0f, -speedRotate, 0f, Space.World);
    }
    public void RotateRight()
    {
        tempObject.transform.Rotate(0f, speedRotate, 0f, Space.World);
    }
    public void SendRay()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit))
        {
            place = new Vector3(_hit.point.x,_hit.point.y,_hit.point.z);
            if (_hit.transform.CompareTag("Terrain"))
            {
                if(tempObjectExist == false)
                {
                    Instantiate(tempCorner, place, Quaternion.identity);
                    tempObject = GameObject.Find("wall_Corner_White(Clone)");
                    tempObjectExist = true;
                }
                
                if (tempObject != null)
                {
                    tempObject.transform.position = place;
                }
            }
            if (input.isAttack)
            {
                Instantiate(objectToPlace, tempObject.transform.position, tempObject.transform.rotation);
                placeNow = false;
                place_Wall_Corner = false;
                Destroy(tempObject);
                tempObjectExist = false;
            }
            if (input.isBlock)
            {
                placeNow = false;
                place_Wall_Corner = false;
                Destroy(tempObject);
                tempObjectExist = false;
            }
        }
    }
    public void PlaceWallCorner()
    {
        placeNow=true;
        place_Wall_Corner = true;
    }
}
