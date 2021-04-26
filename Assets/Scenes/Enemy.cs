using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private int _speed = 5;
    private Vector3 _direction = Vector3.left;

    [SerializeField] private Transform _barrelEnd;
    [SerializeField] private GameObject _enemyPistolRound;

    [SerializeField] private float _pistolFR = 1;
    private float _canFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 30);
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time >= _canFire)
        {
            _canFire = Time.time + _pistolFR;
            Instantiate(_enemyPistolRound, _barrelEnd.position, Quaternion.identity);
        }

        if (transform.position.x > 10)
        {
            _direction = Vector3.left;
        }
        else if (transform.position.x < -10)
        {
            _direction = Vector3.right;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PistolRound")
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.MovePosition(_rigid.position + _direction * _speed * Time.fixedDeltaTime);
    }

}
