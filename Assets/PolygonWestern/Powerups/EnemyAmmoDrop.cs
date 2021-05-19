using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAmmoDrop : MonoBehaviour
{
    [SerializeField] private int _speed = 5;

    void Update()
    {
        transform.position = (transform.position + Vector3.back * _speed * Time.deltaTime);
    }
}
