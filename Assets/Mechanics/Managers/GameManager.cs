using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {

            if (instance == null)
                Debug.Log("Game Manager is null");

            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public GameObject MainPlayer;
    public TextMeshProUGUI ScoreText;
    private int Score;
    private void Start()
    {
        StopGame();
        Score = 0;
        ScoreText.text = Score.ToString();
       
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
        Score += 1;
        ScoreText.text = Score.ToString();
    }
}
