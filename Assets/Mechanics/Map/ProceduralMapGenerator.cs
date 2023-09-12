using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMapGenerator : MonoBehaviour
{
    public GameObject[] SliceablePrefabs;
    public int MaxSpawnDistance = 50; // distance at which new buildings are spawned
    public float SpawnFrequency = 2f; // how often new buildings are spawned
    public float DestroyDistance = 100f; // distance behind the player at which buildings are destroyed
    private float LastSpawnTime; // time when the last building was spawned
    private Transform PlayerTransform; // reference to the player's transform
    [Header("Distance from the player")]
    public float Distance_FP;
    public GameObject[] SpawnedSliceable;
    void Awake()
    {
        LastSpawnTime = Time.time;
    }
    private void Start()
    {
        PlayerTransform = GameManager.Instance.MainPlayer.transform;
    }
    void Update()
    {
        // check if the player has moved far enough to spawn a new building
        if (PlayerTransform != null)
            if (Vector3.Distance(PlayerTransform.position, transform.position) > Distance_FP)
            {
                // check if enough time has passed since the last building was spawned
                if (Time.time - LastSpawnTime > SpawnFrequency)
                {
                    SpawnBuild();
                }
            }
        // check if any buildings are behind the player and far enough to be destroyed
        SpawnedSliceable = GameObject.FindGameObjectsWithTag("Sliceable");
        if (PlayerTransform != null)
            foreach (GameObject building in SpawnedSliceable)
            {
                if (building.transform.position.z > PlayerTransform.position.z + DestroyDistance)
                {
                    Destroy(building);
                }
            }
    }
    public void SpawnBuild()
    {
        int randomSliceable = Random.Range(0, SliceablePrefabs.Length);
        int randomDistance = Random.Range(17 , MaxSpawnDistance);
        Debug.Log($"randomSliceable : {randomSliceable} <-> randomDistance: {randomDistance}");
        transform.localPosition = new Vector3(transform.localPosition.x
            , transform.localPosition.y
            , PlayerTransform.transform.localPosition.z);
        Vector3 spawnPosition = new Vector3(transform.position.x
            , transform.position.y
            , SpawnedSliceable[SpawnedSliceable.Length - 1].transform.localPosition.z + randomDistance);
        Instantiate(SliceablePrefabs[randomSliceable]
            , spawnPosition
            , Quaternion.identity);
        LastSpawnTime = Time.time;
    }
}
