using NUnit.Framework;
using System.Collections.Generic;
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
    [Header("Item")]
    public List<BuildingComponents> itemsBuilding = new List<BuildingComponents>();

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
            input.isCombat = false;
        }
        else if(!placeNow && !input._interact)
        {
            input.isCombat = true;
        }
        
        if (place_Wall_Corner)
        {
            objectToPlace = wall_Corner;
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
        if (!placeNow) return;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit))
        {
            place = new Vector3(_hit.point.x,_hit.point.y,_hit.point.z);
            if (_hit.transform.CompareTag("Terrain"))
            {
                if(tempObjectExist == false)
                {
                  GameObject tempObj = Instantiate(tempCorner, place, Quaternion.identity);
                    //tempObject = GameObject.Find("wall_Corner_White(Clone)");
                    tempObject = tempObj;
                    tempObjectExist = true;
                }
                
                if (tempObject != null)
                {
                    tempObject.transform.position = place;
                }

            }
            if (input.isAttack && input._interact == false && objectToPlace != null )
            {
                Instantiate(objectToPlace, tempObject.transform.position, tempObject.transform.rotation);
                placeNow = false;
                place_Wall_Corner = false;
                Destroy(tempObject);
                tempObjectExist = false;
                objectToPlace = null;
                

            }
            if (input.isBlock)
            {
                placeNow = false;
                place_Wall_Corner = false;
                Destroy(tempObject);
                tempObjectExist = false;
               objectToPlace = null;
                
            }
        }
    }
    public void PlaceWallCorner(int value)
    {
        wall_Corner = itemsBuilding[value].BuildingElementPrefab;
        tempCorner = itemsBuilding[value].PreviewPrefab;
        placeNow =true;
        place_Wall_Corner = true;
    }
}
