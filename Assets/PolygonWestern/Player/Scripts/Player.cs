using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _heart1, _heart2, _heart3;
    [SerializeField] private GameObject _gameOverCanvas;
    private Animator _anim;
    private int _speedFloat;

    [Header("Movement")]
    [SerializeField] private float _speed = 10;
    private Rigidbody _rigid;
    private Vector3 _direction;
    private bool _canMove = false;

    [Header("Revolver")]
    [SerializeField] GameObject _pistol, _pistolDummy;
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

    [Header("Speed Powerup")]
    [SerializeField] private float _speedBoostCD = 10;
    [SerializeField] private float _speedMult = 1.4f;
    [SerializeField] private float _pistolFRMult = 1.4f;
    [SerializeField] private float _shotgunFRMult = 1.4f;
    private bool _speedBoostActive = false;

    [Header("Shield Powerup")]
    [SerializeField] private int _shieldLife = 0;
    [SerializeField] private GameObject _shields3, _shields2, _shields1, _shieldExit;



    void Start()
    {
        Time.timeScale = 1;

        transform.position = new Vector3(0, 0, -46.5f);
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _speedFloat = Animator.StringToHash("Speed");
        _pistolScript = _pistol.GetComponent<Pistol>();
        _shotgunScript = _shotgun.GetComponent<Shotgun>();

        StartCoroutine(StartLives());
    }

    private void Update()
    {
        Movement();
        Shoot();

    }

    private void CanMoveandShoot()
    {
        _canMove = true;
        _canShoot = true;
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

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _canFire && _canShoot == true)
        {
            if (_shotgunActive == true)
            {
                _canFire = Time.time + _shotgunFR;
                _shotgunScript.Fire();

                if (_shotgunAmmo == 1)
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
    }

    void FixedUpdate()
    {
        if (_canMove == true)
        {
            _rigid.MovePosition(_rigid.position + _direction * _speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyPistolRound")
        {
            other.GetComponent<BoxCollider>().enabled = false;
            LoseLife();
        }

        if (other.tag == "Shotgun Powerup")
        {
            Destroy(other.gameObject);
            _shotgunAmmo = 6;
            if (_shotgunActive == false)
            {
                DrawShotgun();
            }

        }

        if (other.tag == "Speed Powerup")
        {
            Destroy(other.gameObject);
            if (_speedBoostActive == false)
            {
                StartCoroutine(SpeedBoost());
            }
        }

        if (other.tag == "Shield Powerup")
        {
            Destroy(other.gameObject);
            _shieldLife = 4;
            LoseLife();
        }

    }

    void LoseLife()
    {
        if (_shieldLife > 0)
        {
            switch (_shieldLife)
            {
                case 4:
                    _shields3.transform.gameObject.SetActive(true);
                    _shieldLife--;
                    break;
                case 3:
                    _shields3.transform.gameObject.SetActive(false);
                    _shields2.transform.gameObject.SetActive(true);
                    _shieldLife--;
                    break;
                case 2:
                    _shields2.transform.gameObject.SetActive(false);
                    _shields1.transform.gameObject.SetActive(true);
                    _shieldLife--;
                    break;
                case 1:
                    _shields1.transform.gameObject.SetActive(false);
                    StartCoroutine(ShieldExit());
                    _shieldLife--;
                    break;
            }
            
        }
        else if (_lives > 0)
        {
            switch (_lives)
            {
                case 3:
                    _heart3.gameObject.SetActive(false);
                    _lives--;
                    break;
                case 2:
                    _heart2.gameObject.SetActive(false);
                    _lives--;
                    break;
                case 1:
                    _heart1.gameObject.SetActive(false);
                    _lives--;
                    Time.timeScale = 0;
                    _gameOverCanvas.SetActive(true);
                    break;
            }
        }
        else
        {
            Debug.Log("GAME OVER!");

        }
    }

    IEnumerator ShieldExit()
    {
        _shieldExit.SetActive(true);
        yield return new WaitForSeconds(1);
        _shieldExit.SetActive(false);
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

    IEnumerator SpeedBoost()
    {
        _speedBoostActive = true;
        _speed *= _speedMult;
        _pistolFR /= _pistolFRMult;
        _shotgunFR /= _shotgunFRMult;
        yield return new WaitForSeconds(_speedBoostCD);
        _speed /= _speedMult;
        _pistolFR *= _pistolFRMult;
        _shotgunFR *= _shotgunFRMult;
        _speedBoostActive = false;

    }

    IEnumerator StartLives()
    {

        yield return new WaitForSeconds(3f);
        _heart1.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _heart2.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _heart3.gameObject.SetActive(true);

    }


}
