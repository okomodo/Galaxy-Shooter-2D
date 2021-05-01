using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private Transform _spawnPointR1R;
    [SerializeField] private Transform _spawnPointR1L;

    [Header("Shotgun Powerup")]
    [SerializeField] private bool _spawnShotgunPU = false;
    [SerializeField] private GameObject _shotgunPU;
    [SerializeField] private int _spawnRate = 10;
    [SerializeField] private int _shotgunSpawnCD = 1;

    [Header("Enemy A Settings")]
    [SerializeField] private bool _spawnEnemyA = false;
    [SerializeField] private GameObject _enemyA;
    [SerializeField] private int _enemyASpawnCD = 5;
    [SerializeField] private int _enemyASpawnAmount = 3;
    private int _amountEnemyAHasSpawned;


    // Start is called before the first frame update
    void Start()
    {
        if(_spawnShotgunPU == true)
        {
            StartCoroutine(SpawnShotgunPU());
        }

        if(_spawnEnemyA == true)
        {
            StartCoroutine(SpawnEnemyA());
        }
    }

    IEnumerator SpawnShotgunPU()
    {
        while (_spawnShotgunPU == true)
        {
            yield return new WaitForSeconds(_shotgunSpawnCD);
            int SpawnRateNum = Random.Range(0, _spawnRate);
            if(SpawnRateNum == 0)
            {
                Instantiate(_shotgunPU, _spawnCenter);
            }
        }
    }

    IEnumerator SpawnEnemyA()
    {
        while (_amountEnemyAHasSpawned < _enemyASpawnAmount)
        {
            yield return new WaitForSeconds(_enemyASpawnCD);
            _amountEnemyAHasSpawned++;
            int SpawnPoint = Random.Range(0, 2);
            switch (SpawnPoint)
            {
                case 0:
                    Instantiate(_enemyA, _spawnPointR1L);
                    break;
                case 1:
                    Instantiate(_enemyA, _spawnPointR1R);
                    break;
                default:
                    Debug.Log("switch broke");
                    break;
            }

            
        }
    }

}
