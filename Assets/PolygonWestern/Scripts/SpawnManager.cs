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

    [Header("Shield Powerup")]
    [SerializeField] private bool _spawnShieldPU = false;
    [SerializeField] private GameObject _shieldPU;

    [Header("Ammo Powerup")]
    [SerializeField] private bool _spawnAmmoPU = false;
    [SerializeField] private GameObject _ammoPU;

    [Header("Health Powerup")]
    [SerializeField] private bool _spawnHealthPU = false;
    [SerializeField] private GameObject _healthPU;


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
            else if(SpawnRateNum == 2 && _spawnShieldPU == true)
            {
                Instantiate(_shieldPU, _spawnCenter);
            }
            else if(SpawnRateNum == 3 && _spawnAmmoPU == true)
            {
                Instantiate(_ammoPU, _spawnCenter);
            }
            else if (SpawnRateNum == 4 && _spawnHealthPU == true)
            {
                Instantiate(_healthPU, _spawnCenter);
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

    public void SpawnAmmo()
    {
        Instantiate(_ammoPU, _spawnCenter);
    }

}
