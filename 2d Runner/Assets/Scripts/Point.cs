using UnityEngine;

public class Point : MonoBehaviour 
{
    public GameObject gear; // Префаб конкретного врага (например, SpiderEnemy)

    void Start () 
    {
        if (gear != null)
        {
            // ВАЖНО: Последний параметр — null! Враг появляется и летит сам по себе, 
            // независимо от того, что произойдет с папкой Variant.
            ObjectPoolManager.Instance.SpawnFromPool(gear, transform.position, Quaternion.identity, null);
        }
    }
}