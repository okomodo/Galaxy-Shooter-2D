using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private int _speed = 10;
    [SerializeField] Pistol _pistol;
    Vector3 _direction;
    [SerializeField] private float _pistolFR = 0.5f;
    private float _canFire = 0;
    [SerializeField] private int _lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 2, 0);
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire)
        {
            _canFire = Time.time + _pistolFR;
            _pistol.Fire();
        }

        if(_lives < 1)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.MovePosition( _rigid.position + _direction * _speed * Time.fixedDeltaTime);
    }

    private void Movement()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (transform.position.x > 9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyPistolRound")
        {
            _lives--;
        }
    }


}
