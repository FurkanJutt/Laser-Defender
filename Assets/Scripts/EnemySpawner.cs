using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool Loop = false;

    [Header("Wave Settings")]
    [SerializeField] int waitBetweenWaveSpawns = 1;
    [SerializeField] int randomWaitBetweenWaveSpawns = 3;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do{   
            yield return StartCoroutine(SpawnAllWaves());
        } while (Loop);
    }

    int checkWaveNumber = 0;
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex< waveConfigs.Count; waveIndex++)
        {
            var waveNumber = Random.Range(startingWave, waveConfigs.Count);
            if (waveNumber != checkWaveNumber)
            {
                checkWaveNumber = waveNumber;
                var currentWave = waveConfigs[waveNumber];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            }
            yield return new WaitForSeconds(Random.Range(waitBetweenWaveSpawns, randomWaitBetweenWaveSpawns));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPaths>().SetWaveConfig(waveConfig);
            float waitBetweenSpawns = Random.Range(waveConfig.GetTimeBetweenSpawns(), waveConfig.GetSpawnRandomFactor());
            yield return new WaitForSeconds(waitBetweenSpawns);
        }
    }
}
