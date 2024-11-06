using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialMenuUI : MonoBehaviour
{
    [Header("color")]
    public Color normalColor;
    public Color hightLightColor;
    public GameObject slicePrefab;   
    public Transform centerObject;    
    public int numberOfSlices = 6;
    public List<GameObject> slices = new List<GameObject>();
    [SerializeField] List <RadialItem> items = new List<RadialItem>();
    [Header("INFORMAL CENTER")]
    public Image InformalCenterBackground;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public Image IconCenter;
    [SerializeField] int currentMenuItemIndex;
    [SerializeField] int previousMenuItemIndex;
    [SerializeField] float calculatedMenuIndex;
    [SerializeField] float currentSelectionAngle;
    [SerializeField] Vector3 currentMousePosition;
    [SerializeField] BuildingSystem _buildingSystem;
    [SerializeField] MgCam mgCam;
    InputManager _inputManager;
    [Header("Object")]
    public GameObject RadialMenu;
    private void Start()
    {
        CreateRadialMenu();
        _buildingSystem = FindFirstObjectByType<BuildingSystem>();
        _inputManager = FindFirstObjectByType<InputManager>();
        mgCam = FindFirstObjectByType<MgCam>();
        RadialMenu.SetActive(false);
    }
    private void Update()
    {
        GetCurrentMenuElement();
       mgCam.LockCursor(!RadialMenu.activeSelf);
        if(_buildingSystem.placeNow == false)
        {
            if (_inputManager._interact)
            {
                RadialMenu.SetActive(true);
            }
            else
            {
                RadialMenu.SetActive(false);
            }
        }
        else
        {
            RadialMenu.SetActive(false);
        }
        if(RadialMenu.activeSelf)
        {
            _inputManager.isCombat = false;
            if (_inputManager.isAttack)
            {
                _buildingSystem.PlaceWallCorner(currentMenuItemIndex);
                _inputManager._interact = false;
            }
        }
        
    }
    private void GetCurrentMenuElement()
    {
        float rotationalIncrementalValue = 360f / _buildingSystem.itemsBuilding.Count;
        currentMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);

        currentSelectionAngle = 90 + rotationalIncrementalValue + Mathf.Atan2(currentMousePosition.y, currentMousePosition.x) * Mathf.Rad2Deg;
        currentSelectionAngle = (currentSelectionAngle + 360f) % 360f;

        currentMenuItemIndex = (int)(currentSelectionAngle / rotationalIncrementalValue);

        if (currentMenuItemIndex != previousMenuItemIndex)
        {
            items[previousMenuItemIndex].BackgroundImage.color = normalColor;

            previousMenuItemIndex = currentMenuItemIndex;
            InformalCenterBackground.color = hightLightColor;
            items[currentMenuItemIndex].BackgroundImage.color = hightLightColor;
            RefreshInformalCenter();
            
            
        }
    }
    public void CreateRadialMenu()
    {
        // Xóa các phần tử cũ trước khi tạo mới
        foreach (Transform child in centerObject)
        {
            Destroy(child.gameObject);
        }

        // Tính toán góc của mỗi phần tử dựa trên số lượng phần tử
        float angleStep = 360f / _buildingSystem.itemsBuilding.Count;
        float currentRotationValue = 0;
        float fillAmount = 1f / _buildingSystem.itemsBuilding.Count;

        for (int i = 0; i < _buildingSystem.itemsBuilding.Count; i++)
        {
            // Tạo một instance của slicePrefab
            GameObject newSlice = Instantiate(slicePrefab, centerObject);
            slices.Add(newSlice);
            items.Add(newSlice.GetComponent<RadialItem>());
           RadialItem radialbutton = newSlice.GetComponent<RadialItem>();
           
            // Đặt góc xoay cho phần tử để tạo thành một vòng tròn hoàn chỉnh
            newSlice.transform.localRotation = Quaternion.Euler(0, 0, currentRotationValue);
            currentRotationValue += angleStep;
            radialbutton.BackgroundImage.fillAmount = fillAmount + 0.001f;
            radialbutton.IconImage.sprite = _buildingSystem.itemsBuilding[i].buildingItem.buttonIcon;
            radialbutton.IconRecttransform.rotation = Quaternion.identity;
            
        }
    }
    void RefreshInformalCenter()
    {
        ItemName.text = _buildingSystem.itemsBuilding[currentMenuItemIndex].buildingItem.nameItems;
        ItemDescription.text = _buildingSystem.itemsBuilding[currentMenuItemIndex].buildingItem.description;
        IconCenter.sprite = _buildingSystem.itemsBuilding[currentMenuItemIndex].buildingItem.buttonIcon;
    }
}
