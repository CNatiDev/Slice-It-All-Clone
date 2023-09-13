using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    [Header("DISTANCES")]
    [Tooltip("Distance at which new buildings are spawned")]
    public int _maxSpawnDistance = 50;

    [Tooltip("Distance behind the player at which buildings are destroyed")]
    public float _destroyDistance = 100f;

    [Tooltip("Distance from the player")]
    public float _distance2Player;

    [Tooltip("How often new buildings are spawned")]
    public float _spawnFrequency = 2f;

    [Tooltip("Time when the last building was spawned")]
    private float _lastSpawnTime;

    [Header("PREFABS")]
    [Tooltip("All sliceable objects for spawn")]
    public GameObject[] _sliceablePrefabs;

    [Tooltip("sliceable already spawned")]
    public GameObject[] _spawnedSliceable;

    [Tooltip("win prefab")]
    public GameObject _winPrefab;

    [Header("LEVELS")]

    [Tooltip("Current level for know how much sliceable need to spawn")]
    public int _currentLevel = 1;
    //how much sliceable already was spawned
    private int _alreadySpawned = 0;

    [Header("TRAP")]
    public GameObject _trapPrefab;
    public int _distance2NextTrap;
    public GameObject _lastTrap;

    //Reference to the player's transform
    private Transform _playerTransform;

    private static ProceduralMapGenerator _instance;
    public static ProceduralMapGenerator Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("ProceduralMapGenerator is null");
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
        _lastSpawnTime = Time.time;
    }
    private void Start()
    {
        _playerTransform = GameManager.Instance._mainPlayer.transform;
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

    }
    void Update()
    {
        // check if the player has moved far enough to spawn a new building
        if (_playerTransform != null)
            if (Vector3.Distance(_playerTransform.position, transform.position) > _distance2Player)
            {
                // check if enough time has passed since the last building was spawned
                if (Time.time - _lastSpawnTime > _spawnFrequency)
                {
                    SpawnBuild();
                }
            }
        // check if any buildings are behind the player and far enough to be destroyed
        _spawnedSliceable = GameObject.FindGameObjectsWithTag("Sliceable");
        if (_playerTransform != null)
            foreach (GameObject building in _spawnedSliceable)
            {
                if (building.transform.position.z > _playerTransform.position.z + _destroyDistance)
                {
                    Destroy(building);
                }
            }
    }
    public void SpawnBuild()
    {
        int _randomSliceable = Random.Range(0, _sliceablePrefabs.Length);
        int _randomDistance = Random.Range(10 , _maxSpawnDistance);

        transform.localPosition = new Vector3(transform.localPosition.x
            , transform.localPosition.y
            , _playerTransform.transform.localPosition.z);

        Vector3 _spawnPosition = new Vector3(transform.position.x
            , transform.position.y
            , _spawnedSliceable[_spawnedSliceable.Length - 1].transform.localPosition.z + _randomDistance);

        if (_alreadySpawned == _currentLevel)
        {
            Instantiate(_winPrefab
            , _spawnPosition
            , Quaternion.identity);
            GetComponent<ProceduralMapGenerator>().enabled = false;
        }
        else
        {
            Instantiate(_sliceablePrefabs[_randomSliceable]
            , _spawnPosition
            , Quaternion.identity);
        }

        _alreadySpawned++;

        // Spawn a trap at 3 sliceable 
        if (_alreadySpawned % 3 == 0)
        {
            Vector3 _tarpPosition = new Vector3(transform.position.x
            , transform.position.y
            , _lastTrap.transform.position.z + _distance2NextTrap);
            _lastTrap = Instantiate(_trapPrefab
            , _spawnPosition
            , Quaternion.identity);
        }
        _lastSpawnTime = Time.time;
    }
}
