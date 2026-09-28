using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    public Wave[] waves;
    public Wave _currentWave;
    private int _currentWaveIndex;

    private int _enemyRemainingAliveCount;
    private float _upgrade;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Cant find enemy spawn points, please check it!");
            return;
        }

        StartCoroutine(NextWaveCoroutine());
    }

    private IEnumerator NextWaveCoroutine()
    {
        _currentWaveIndex++;
        if(_currentWaveIndex - 1 < waves.Length)
        {
            _currentWave = waves[_currentWaveIndex - 1];
            for (int i = 0; i < _currentWave.count; i++)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);
                EnemyMovement enemy = Instantiate(_currentWave.enemy, spawnPoints[spawnIndex].position, Quaternion.identity);
                enemy.OnDeath += OnEnemyDeath;
                yield return new WaitForSeconds(_currentWave.timeBetweenSpawns);
            }
        }
        else
        {
            _currentWaveIndex = 0;

            _upgrade += 0.1f;
            StartCoroutine(NextWaveCoroutine());
        }
    }

    private void OnEnemyDeath()
    {
        _enemyRemainingAliveCount--;
        if (_enemyRemainingAliveCount <= 0)
        {
            StartCoroutine(NextWaveCoroutine());
        }
    }
}
