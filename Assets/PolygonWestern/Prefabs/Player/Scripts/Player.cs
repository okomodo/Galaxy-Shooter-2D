using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private int _speed = 10;
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _pistolDummy;
    private Pistol _pistolS;
    Vector3 _direction;
    [SerializeField] private float _pistolFR = 0.5f;
    private float _canFire = 0;
    [SerializeField] private int _lives = 3;
    private Animator _anim;
    private int _speedFloat;
    private bool _canMove = false;
    private bool _canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, -46.5f);
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _speedFloat = Animator.StringToHash("Speed");
        _pistolS = _pistol.GetComponent<Pistol>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire && _canShoot == true)
        {
            _canFire = Time.time + _pistolFR;
            _pistolS.Fire();
        }

        if(_lives < 1)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canMove == true)
        {
            _rigid.MovePosition(_rigid.position + _direction * _speed * Time.fixedDeltaTime);
        }
    }

    private void CanMoveandShoot()
    {
        _canMove = true;
        _canShoot = true;
    }
    private void DrawPistol()
    {
            _pistol.SetActive(true);
            _pistolDummy.SetActive(false);

    }

    private void Movement()
    {
        _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        _anim.SetFloat(_speedFloat, _direction.x);

        if (transform.position.x > 4)
        {
            transform.position = new Vector3(4, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -4)
        {
            transform.position = new Vector3(-4, transform.position.y, transform.position.z);
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
