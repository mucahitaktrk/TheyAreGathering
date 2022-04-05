using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float _lastFinger;
    private float _moveX;

    private float _speedZ;
    [SerializeField] private float _speedX;

    [SerializeField] private float _bondrey;

    private void Update()
    {
        InputSystem();
        GameController(GameManager.gameManager.speed);
    }

    private void GameController(float speed)
    {
        if (GameManager.gameManager.isStartPlay)
        {
            _speedZ = speed;
        }
    }

    private void InputSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFinger = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveX = _lastFinger - Input.mousePosition.x;
            _lastFinger = Input.mousePosition.x;
        }
        else
        {
            _moveX = 0;
        }

        float xBondrey = Mathf.Clamp(transform.position.x, -_bondrey, _bondrey);
        transform.position = new Vector3(xBondrey, transform.position.y, transform.position.z);

        float swaer = Time.deltaTime * _moveX * _speedX;
        transform.Translate(swaer, 0, _speedZ * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gate")
        {
            other.gameObject.transform.parent.GetChild(3).tag = "Untagged";
            other.gameObject.transform.parent.GetChild(4).tag = "Untagged";
            if (other.gameObject.layer == 7)
            {
                for (int i = 0; i < int.Parse(other.gameObject.name); i++)
                {
                    GameManager.gameManager.StickmanSpawn();
                }
            }
            else if (other.gameObject.layer == 8)
            {
                for (int i = 0; i < int.Parse(other.gameObject.name); i++)
                {
                    if (GameManager.gameManager._stickmanList.Count > int.Parse(other.gameObject.name))
                    {
                        GameManager.gameManager.StickmanDelete(i);
                    }
                    else
                    {
                        for (int j = 0; j < GameManager.gameManager._stickmanList.Count; j++)
                        {
                            GameManager.gameManager.StickmanDelete(j);
                            GameManager.gameManager.LoseGame();
                        }
                    }
                }
            }
        }
        if (other.gameObject.tag == "EndGame")
        {
            GameManager.gameManager.speed = 0.0f;
            GameManager.gameManager.EndGame(true);
        }
    }
}
