using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSpread : MonoBehaviour
{
    [SerializeField] private int _speed = 100;
    [SerializeField] private Transform[] _round;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        _round[0].position += _round[0].forward * _speed * Time.deltaTime;
        _round[1].position += _round[1].forward * _speed * Time.deltaTime;
        _round[2].position += _round[2].forward * _speed * Time.deltaTime;
        _round[3].position += _round[3].forward * _speed * Time.deltaTime;
        _round[4].position += _round[4].forward * _speed * Time.deltaTime;
        _round[5].position += _round[5].forward * _speed * Time.deltaTime;
        _round[6].position += _round[6].forward * _speed * Time.deltaTime;

    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
