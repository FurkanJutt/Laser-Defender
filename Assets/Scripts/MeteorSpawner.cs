using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] List<MeteorWave> meteorWaveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool Loop = false;

    [Header("Wave Settings")]
    [SerializeField] int waitBetweenWaveSpawns = 1;
    [SerializeField] int randomWaitBetweenWaveSpawns = 3;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (Loop);
    }

    int checkWaveNumber = 0;
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < meteorWaveConfigs.Count; waveIndex++)
        {
            var waveNumber = Random.Range(startingWave, meteorWaveConfigs.Count);
            if (waveNumber != checkWaveNumber)
            {
                checkWaveNumber = waveNumber;
                var currentWave = meteorWaveConfigs[waveNumber];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            }
            yield return new WaitForSeconds(Random.Range(waitBetweenWaveSpawns, randomWaitBetweenWaveSpawns));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(MeteorWave meteorWaveConfig)
    {
        for (int enemyCount = 0; enemyCount < meteorWaveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newMeteor = Instantiate(meteorWaveConfig.GetMeteorPrefab(), meteorWaveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newMeteor.GetComponent<MeteorPath>().SetWaveConfig(meteorWaveConfig);
            float waitBetweenSpawns = Random.Range(meteorWaveConfig.GetTimeBetweenSpawns(), meteorWaveConfig.GetSpawnRandomFactor());
            yield return new WaitForSeconds(waitBetweenSpawns);
        }
    }
}
