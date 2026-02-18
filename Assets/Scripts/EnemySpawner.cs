using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveConfigSO currentWave;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < currentWave.GetEnemyCount(); i++)
        {
            Instantiate(
                currentWave.GetEnemyPrefab(0),
                currentWave.GetStartingWayPoint().position,
                Quaternion.identity,
                transform
            );           
        }
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
