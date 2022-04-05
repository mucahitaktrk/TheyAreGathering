using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    [SerializeField] private float minX, maxX;
    [SerializeField] private GameObject _enemyStickman;
    [SerializeField] private int _enemySpawnSecond = 1;

    private void Start()
    {
        StartCoroutine(nameof(EnemySpawnSystem));

    }



    private void Update()
    {
        if (GameManager.gameManager.isStartPlay && !GameManager.gameManager.isEndGameAnimation && !GameManager.gameManager.isGameEnd)
        {
            transform.Translate(Vector3.forward * 10f * Time.deltaTime);
        }
    }


    IEnumerator EnemySpawnSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (GameManager.gameManager.isStartPlay && !GameManager.gameManager.isEndGameAnimation && !GameManager.gameManager.isGameEnd)
            {
                for (int i = 0; i < _enemySpawnSecond; i++)
                {
                    float spawnX = Random.Range(minX, maxX);
                    Vector3 vec = new Vector3(spawnX, 0, transform.position.z);
                    Instantiate(_enemyStickman, vec, Quaternion.identity);
                }
            }
        }
    }
}
