using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {

            if (_instance == null)
                Debug.LogError("Game Manager is null");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    [Header("MainPlayer/Knife")]
    public GameObject _mainPlayer;

    [Header("SCORE")]
    public TextMeshProUGUI _scoreText;
    public int _score;

    [Header("SCREENS")]
    public GameObject _winScreen;
    public GameObject _loseScreen;

    [Header("SPIKE")]
    public float _spikeCanEnable;
    public GameObject _spikeButton;
    private void Start()
    {
        StopGame();
        _score = 0;
        _scoreText.text = _score.ToString();
       
    }
    public void PlayGame()
    {
        Time.timeScale = 1.0f;
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }
    public void IncreasaScore()
    {
        _score += 1;
        _scoreText.text = _score.ToString();
    }
    // Assing on restart button in win screen
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void CheckSpike()
    {
        if (_score==_spikeCanEnable)
        {
            _spikeButton.SetActive(true);
        }
    }
}
