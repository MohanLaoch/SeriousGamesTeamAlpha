using System;
using UnityEngine;
using UnityEngine.Pool;


public class PlatformPool : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float currentXSpawnPosition = 12f;

    [SerializeField] private bool collectionChecks = true;
    [SerializeField] private int maxPoolSize = 15;
    public IObjectPool<GameObject> m_pool { get; set; }

    private void Start()
    {
        m_pool = new ObjectPool<GameObject>(CreatePlatform, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject,
            collectionChecks, 10, maxPoolSize);
    }

    public void SpawnPlatform() => m_pool.Get();


    public void ReleasePlatform(GameObject platform) => m_pool.Release(platform);
  
    
    
    private GameObject CreatePlatform()
    {
        GameObject platform = Instantiate(platformPrefab, transform);
        platform.SetActive(false);

        return platform;
    }

    private void OnTakeFromPool(GameObject platform)
    {
        platform.transform.position = new Vector3(currentXSpawnPosition, 1, 0);
        platform.gameObject.SetActive(true);
    }
    
    private void OnDestroyPoolObject(GameObject platform)
    {
        Destroy(platform);
    }

    private void OnReturnedToPool(GameObject platform)
    {
        platform.gameObject.SetActive(false);
    }
}