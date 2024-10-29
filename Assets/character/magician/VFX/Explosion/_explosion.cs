using UnityEngine;

public class _explosion : MonoBehaviour
{
    [SerializeField] ObjectPooling _explosionPool;

    private void Awake()
    {
        _explosionPool = GameObject.FindGameObjectWithTag("TrailExplosionPool").GetComponent<ObjectPooling>();
    }
    private void OnEnable()
    {
        Invoke(nameof(returnObject),1.5f);
    }
    void returnObject()
    {
        _explosionPool.ReturnObject(this.gameObject);
    }
}
