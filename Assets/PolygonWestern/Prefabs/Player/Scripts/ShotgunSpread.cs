using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSpread : MonoBehaviour
{
    [SerializeField] private int _speed = 100;
    [SerializeField] private Transform _r1, _r2, _r3, _r4, _r5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        _r1.position += _r1.forward * _speed * Time.deltaTime;
        _r2.position += _r2.forward * _speed * Time.deltaTime;
        _r3.position += _r3.forward * _speed * Time.deltaTime;
        _r4.position += _r4.forward * _speed * Time.deltaTime;
        _r5.position += _r5.forward * _speed * Time.deltaTime;
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
