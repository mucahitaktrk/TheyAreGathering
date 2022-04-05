using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(nameof(BulletSpawn));
    }

    IEnumerator BulletSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.gameManager.isStartPlay && !GameManager.gameManager.isGameEnd)
            {
                ObjectPool.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
            }
        }
    }
}
