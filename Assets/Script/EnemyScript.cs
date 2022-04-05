using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    private  NavMeshAgent _navMeshAgent;
    private Animator _enemyAnimator;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<Animator>();
        _navMeshAgent.speed = 10.0f;
        InvokeRepeating(nameof(LoseEndGame), 2f, 2f);
    }

    void Update()
    {
        if (GameManager.gameManager._stickmanList.Count >= 1 && !GameManager.gameManager.isEndGameAnimation)
        {
            _navMeshAgent.destination = GameManager.gameManager.transform.position;
        }
    }

    private void LoseEndGame()
    {
        if (GameManager.gameManager._stickmanList.Count >= 1 && GameManager.gameManager.isEndGameAnimation)
        {
            float stickman = Random.Range(0, GameManager.gameManager._stickmanList.Count);
            _navMeshAgent.SetDestination(GameManager.gameManager._stickmanList[System.Convert.ToInt32(stickman)].transform.position);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _navMeshAgent.speed = 0;
            gameObject.tag = "Untagged";
            _enemyAnimator.SetBool("isDeadEnemy", true);
            Destroy(gameObject , 4f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletTag")
        {
            _navMeshAgent.speed = 0;
            gameObject.tag = "Untagged";
            _enemyAnimator.SetBool("isDeadEnemy", true);
            Destroy(gameObject, 4f);
        }
    }
    
}
