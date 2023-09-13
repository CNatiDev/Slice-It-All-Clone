using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public int _scoreMultipler;
    private GameManager _gameManager;
    private ProceduralMapGenerator _proceduralMapGenerator;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _proceduralMapGenerator = ProceduralMapGenerator.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            _gameManager._winScreen.SetActive(true);
            _gameManager._score *= _scoreMultipler;
            _gameManager._scoreText.text = _gameManager._score.ToString();
            _gameManager._mainPlayer.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePosition;
            _proceduralMapGenerator._currentLevel += 1;
            PlayerPrefs.SetInt("CurrentLevel", _proceduralMapGenerator._currentLevel);
            _gameManager.StopGame();
        }

    }
}
