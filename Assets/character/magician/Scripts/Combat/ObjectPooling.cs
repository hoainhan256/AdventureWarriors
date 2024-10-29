using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject PrefabObject;
    public int PoolSize = 10;
    private List<GameObject> Pool = new List<GameObject>();
    private void Awake()
    {
        Pool = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(PrefabObject);
            obj.SetActive(false);
            Pool.Add(obj);
        }
    }
    public GameObject GetObject()
    {
        foreach (GameObject obj in Pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = Instantiate(PrefabObject);
        newObj.SetActive(true);
        Pool.Add(newObj);
        return newObj;
    }
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        
    }
    
}
