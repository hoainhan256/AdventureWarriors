using NUnit.Framework;
using UnityEngine;
[CreateAssetMenu(fileName = "New BuildingComponent", menuName = "BuildingComponent", order = 1)]
public class BuildingComponents : ScriptableObject
{
    public GameObject PreviewPrefab;
    public GameObject BuildingElementPrefab;
    public buildingItem buildingItem;
}
