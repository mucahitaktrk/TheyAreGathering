using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField] private GameObject _stickman;
    public List<GameObject> _stickmanList;

    public List<GameObject> _enemyStickmanList;

    public List<GameObject> _stickmanEndGamePos;
    [SerializeField] private GameObject[] _saws;
    private float pi = 3.1415f;
    [SerializeField] private float zRadius = 0.0f;
    [SerializeField] private float xRadius = 0.0f;

    public float speed = 10.0f;
    public bool isStartPlay;
    public bool isEndGameAnimation;

    //-----UI---------------------
    [SerializeField] private GameObject[] _gameEndPanel;
    [SerializeField] private TextMeshProUGUI _levelText;
    private int _level;
    [SerializeField] private GameObject[] _levels;
    public bool isGameEnd = false;


    private void Awake()
    {
        _level = PlayerPrefs.GetInt("Level");
        _levelText.gameObject.SetActive(true);
        _levelText.text = "LEVEL : " + (_level + 1);
        for (int i = 0; i < _gameEndPanel.Length; i++)
        {
            _gameEndPanel[i].SetActive(false);
        }
        Instantiate(_levels[_level]);
        gameManager = this;
        _stickmanEndGamePos = new List<GameObject>(GameObject.FindGameObjectsWithTag("EndGamePos"));
        _saws = GameObject.FindGameObjectsWithTag("Saw");
    }

    private void Update()
    {
        _stickmanList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        _enemyStickmanList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (isEndGameAnimation)
        {
            if (_enemyStickmanList.Count < 1)
            {
                WinGame();
            }
        }
        GameControllerSystem();
        SawSystem();
    }

    public void StickmanSpawn()
    {
        _stickmanList.Add(Instantiate(_stickman, transform));
        UpdateCircle();
    }

    public void StickmanDelete(int i)
    {
        Destroy(_stickmanList[i]);
    }

    private void GameControllerSystem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStartPlay = true;
        }
        if (_stickmanList.Count < 1)
        {
            LoseGame();
        }
    }

    private void UpdateCircle()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 1; i <= _stickmanList.Count; i++)
        {
            GameObject stickmanPos;
            stickmanPos = Instantiate(_stickman, transform);
            if (i >= 2 && i <= 9)
            {
                float deg = (4 - i) * 45;
                float rad = deg / 180 * pi;
                stickmanPos.transform.localPosition = new Vector3(Mathf.Sin(rad) * xRadius, 0, Mathf.Cos(rad) * zRadius);
            }
            else if (i == 1)
            {
                stickmanPos.transform.localPosition = Vector3.zero;
            }
            else if (i >= 10 && i <= 21)
            {
                float deg = (13 - i) * 30;
                float rad = deg / 180 * pi;
                stickmanPos.transform.localPosition = new Vector3(Mathf.Sin(rad) * 1.5f * xRadius, 0f, Mathf.Cos(rad) * 1.5f * zRadius);
            }
            else if (i >= 22 && i <= 37)
            {
                float deg = (26 - i) * 22.5f;
                float rad = deg / 180 * pi;
                stickmanPos.transform.localPosition = new Vector3(Mathf.Sin(rad) * 2 * xRadius, 0, Mathf.Cos(rad) * 2 * zRadius);
            }
            else if (i >= 38)
            {
                float deg = (45 - i) * 18f;
                float rad = deg / 180 * pi;
                stickmanPos.transform.localPosition = new Vector3(Mathf.Sin(rad) * 2.5f * xRadius, 0, Mathf.Cos(rad) * 2.5f * zRadius);
            }
        }
    }

    private void SawSystem(float _sawSpeed = 180f)
    {
        for (int i = 0; i < _saws.Length; i++)
        {
            _saws[i].transform.Rotate(Vector3.up * _sawSpeed * Time.deltaTime);
        }
    }
    public void EndGame(bool isEndGame)
    {
        if (isEndGame)
        {
            isEndGameAnimation = true;
            for (int i = 0; i < _stickmanList.Count; i++)
            {
                _stickmanList[i].transform.DOMove(_stickmanEndGamePos[i].transform.position, 1f);
            }
        }
    }
    
    public void WinGame()
    {
        _levelText.gameObject.SetActive(false);
        isGameEnd = true;
        speed = 0.0f;
        _gameEndPanel[0].SetActive(true);
    }

    public void LoseGame()
    {
        _levelText.gameObject.SetActive(false);
        isGameEnd = true;
        speed = 0.0f;
        _gameEndPanel[1].SetActive(true);
    }

    public void NextLevelButton()
    {
        Destroy(_levels[_level]);
        _level++;
        if (_level > 2)
        {
            _level = 0;
        }
        PlayerPrefs.SetInt("Level", _level);
        SceneManager.LoadScene(0);
    }

    public void LoseButton()
    {
        Destroy(_levels[_level]);
        SceneManager.LoadScene(0);
    }
    
}
