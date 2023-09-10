using System.Collections;
using System.Collections.Generic;
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
}