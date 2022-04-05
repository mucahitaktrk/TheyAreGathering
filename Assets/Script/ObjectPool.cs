using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class BulletPool
    {
        public string tag;
        public GameObject bulletPrefab;
        public int size;
    }

    public List<BulletPool> bulletPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (BulletPool bulletPool in bulletPools)
        {
            Queue<GameObject> bulletObject = new Queue<GameObject>();

            for (int i = 0; i < bulletPool.size; i++)
            {
                GameObject obj = Instantiate(bulletPool.bulletPrefab, transform.position, Quaternion.Euler(0, 180, 0));
                obj.SetActive(false);
                bulletObject.Enqueue(obj);
            }
            poolDictionary.Add(bulletPool.tag, bulletObject);
        }
    }

    public GameObject SpawnFromPool(string tag , Vector3 position , Quaternion rotation)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IBulletObject bulletObj = objectToSpawn.GetComponent<IBulletObject>();

        if (bulletObj != null)
        {
            bulletObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
