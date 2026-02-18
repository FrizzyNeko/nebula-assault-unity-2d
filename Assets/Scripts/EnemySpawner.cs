using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO[] waveConfigs;
    [SerializeField] float timeBetweenWaves = 3f;

    WaveConfigSO currentWave;
   

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        foreach (WaveConfigSO wave in waveConfigs)
        {
            currentWave = wave;
            for (int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(
                    currentWave.GetEnemyPrefab(0),
                    currentWave.GetStartingWayPoint().position,
                    Quaternion.identity,
                    transform
                );
                yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
            }
            yield return new WaitForSeconds(timeBetweenWaves);
            // Wave biter
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
