using System;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;
    private void Awake()
    {
        if (objectPool == null)
        {
            objectPool = FindFirstObjectByType<ObjectPooling>();
        }
    }
    private void OnEnable()
    {
        Invoke(nameof(UnActiveObject), 5f);
    }
   
   void UnActiveObject()
    {
        objectPool.ReturnObject(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        UnActiveObject();
        Debug.Log(other.name);
    }
    void Update()
    {
        
    }
}
