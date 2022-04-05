using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanScript : MonoBehaviour
{

    private Animator _stickmanAnimator;

    void Start()
    {
        _stickmanAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.gameManager.isEndGameAnimation)
        {
            _stickmanAnimator.SetBool("isStart", GameManager.gameManager.isStartPlay);
        }
        else
        {
            _stickmanAnimator.SetBool("isStart", false);
            _stickmanAnimator.SetBool("isEndGame", GameManager.gameManager.isEndGameAnimation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.tag = "Untagged";
            collision.gameObject.tag = "Untagged";
            _stickmanAnimator.SetBool("isDead", true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}
