using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointR1R;
    [SerializeField] private Transform _spawnPointR1L;

    [Header("Enemy A Settings")]
    [SerializeField] private bool _spawnEnemyA = false;
    [SerializeField] private GameObject _enemyA;
    [SerializeField] private int _enemyASpawnCD = 5;
    [SerializeField] private int _enemyASpawnAmount = 3;
    private int _amountEnemyAHasSpawned;


    // Start is called before the first frame update
    void Start()
    {

        if(_spawnEnemyA == true)
        {
            StartCoroutine(SpawnEnemyA());
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
                    GameObject EnemyAR = _enemyA;
                    Instantiate(EnemyAR, _spawnPointR1R);
                    EnemyAR.GetComponent<EnemyA>()._direction = Vector3.left;
                    break;
                case 1:
                    Instantiate(_enemyA, _spawnPointR1L);
                    break;
                default:
                    Debug.Log("switch broke");
                    break;
            }

            
        }
    }

}
