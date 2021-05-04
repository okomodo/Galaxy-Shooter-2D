using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private Transform _spawnPointR1R;
    [SerializeField] private Transform _spawnPointR1L;

    [Header("Enemy A Settings")]
    [SerializeField] private bool _spawnEnemyA = false;
    [SerializeField] private GameObject _enemyA;
    [SerializeField] private int _enemyASpawnCD = 5;
    [SerializeField] private int _enemyASpawnAmount = 3;
    private int _amountEnemyAHasSpawned;

    [Header("Powerup Settings")]
    [SerializeField] private bool _spawnPowerups;
    [SerializeField] private int _powerupSpawnCD;
    [SerializeField] private int _powerupSpawnRate;

    [Header("Shotgun Powerup")]
    [SerializeField] private bool _spawnShotgunPU = false;
    [SerializeField] private GameObject _shotgunPU;

    [Header("Speed Powerup")]
    [SerializeField] private bool _spawnSpeedPU = false;
    [SerializeField] private GameObject _speedPU;



    // Start is called before the first frame update
    void Start()
    {
        if(_spawnPowerups == true)
        {
            StartCoroutine(SpawnPowerups());
        }

        if(_spawnEnemyA == true)
        {
            StartCoroutine(SpawnEnemyA());
        }

    }

    IEnumerator SpawnPowerups()
    {
        while (_spawnPowerups == true)
        {
            yield return new WaitForSeconds(_powerupSpawnCD);
            int SpawnRateNum = Random.Range(0, _powerupSpawnRate);
            if (SpawnRateNum == 0 && _spawnShotgunPU == true)
            {
                Instantiate(_shotgunPU, _spawnCenter);
            }
            else if(SpawnRateNum == 1 && _spawnSpeedPU == true)
            {
                Instantiate(_speedPU, _spawnCenter);
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
