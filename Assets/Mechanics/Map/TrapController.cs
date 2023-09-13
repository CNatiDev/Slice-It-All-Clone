using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager.StopGame();
            _gameManager._loseScreen.SetActive(true);
        }

    }
}
