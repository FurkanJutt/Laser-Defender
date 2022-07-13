using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Config")]
public class WaveConfig : ScriptableObject
{
    // Configuration Parameters
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int numberOfEnemies = 5;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public List<Transform> GetWaypoints() {
        var wavWaypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform)
        {
            wavWaypoints.Add(waypoint);
        }
        return wavWaypoints; 
    }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return spawnRandomFactor; }
    public float GetMoveSpeed() { return moveSpeed;}
    public int GetNumberOfEnemies() { return numberOfEnemies; }
}
