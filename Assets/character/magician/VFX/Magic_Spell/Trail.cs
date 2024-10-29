using System;
using System.Collections;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] ObjectPooling objectPool;
    [SerializeField] ObjectPooling explosionPool;
    [SerializeField] Rigidbody rig;
    [SerializeField] bool isCollison = false;
    [SerializeField]
    ContactPoint collisionPosition;
    private void Awake()
    {
        if (objectPool == null || explosionPool == null)
        {
            objectPool = GameObject.FindGameObjectWithTag("TrailPooling").GetComponent<ObjectPooling>();
            explosionPool = GameObject.FindGameObjectWithTag("TrailExplosionPool").GetComponent<ObjectPooling>();
        }
    }
    private void OnEnable()
    {
        isCollison = false;
        StartCoroutine(UnActiveObject( 2f));
    }
    private void OnDisable()
    {
    }
    IEnumerator UnActiveObject( float delay)
    {

        yield return new WaitForSeconds( delay);
        Vector3 contact = isCollison ? collisionPosition.point : transform.position;
        Explosion(contact);
        objectPool.ReturnObject(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        isCollison = true;
        rig.linearVelocity = Vector3.zero;
        collisionPosition = collision.GetContact(0);
        Debug.Log(collision.gameObject.name + collisionPosition);
        StartCoroutine(UnActiveObject(0f));
    }
    
    void Explosion(Vector3 pos)
    {
        GameObject explosion = explosionPool.GetObject();
        explosion.transform.position = pos;
        explosion.transform.rotation = Quaternion.identity;
    }
    void Update()
    {
        
    }
}
