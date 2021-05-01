using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int _lives = 3;
    private Animator _anim;
    private int _speedFloat;

    [Header("Movement")]
    [SerializeField] private int _speed = 10;
    private Rigidbody _rigid;
    private Vector3 _direction;
    private bool _canMove = false;

    [Header("Revolver")]
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _pistolDummy;
    [SerializeField] private float _pistolFR = 0.5f;
    private Pistol _pistolScript;
    private float _canFire = 0;
    private bool _canShoot = false;

    [Header ("Shotgun Powerup")]
    [SerializeField] private GameObject _shotgun;
    [SerializeField] private float _shotgunFR = 0.7f;
    private bool _shotgunActive = false;
    private int _shotgunAmmo = 0;
    private Shotgun _shotgunScript;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, -46.5f);
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _speedFloat = Animator.StringToHash("Speed");
        _pistolScript = _pistol.GetComponent<Pistol>();
        _shotgunScript = _shotgun.GetComponent<Shotgun>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire && _canShoot == true)
        {
            if (_shotgunActive == true)
            {
                _canFire = Time.time + _shotgunFR;
                _shotgunScript.Fire();

                if(_shotgunAmmo == 1)
                {
                    DrawShotgun();
                }

                _shotgunAmmo--;
            }
            else
            {
                _canFire = Time.time + _pistolFR;
                _pistolScript.Fire();
            }
        }

        if(_lives < 1)
        {
            Debug.Log("You Lose!");
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
            _pistol.SetActive(!_pistol.activeInHierarchy);
            _pistolDummy.SetActive(!_pistolDummy.activeInHierarchy);

    }

    private void DrawShotgun()
    {
        DrawPistol();
        _shotgun.SetActive(!_shotgun.activeInHierarchy);
        _shotgunActive = !_shotgunActive;
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

        if(other.tag == "Shotgun Powerup")
        {
            Destroy(other.gameObject);
            _shotgunAmmo = 6;
            if (_shotgunActive == false)
            { 
                DrawShotgun();
            }
            
        }

    }


}
