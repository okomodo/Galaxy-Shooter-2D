using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _heart1, _heart2, _heart3;
    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private ParticleSystem _bloodFX, _bloodDrip1, _bloodDrip2;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private SpawnManager _spawnManager;
    private Animator _anim;
    private int _speedFloat;
    private bool _invincible = false;

    [Header("Movement")]
    [SerializeField] private float _speed = 10;
    private Rigidbody _rigid;
    private Vector3 _direction;
    private bool _canMove = false;
    private bool _canRoll = false;

    [Header("Revolver")]
    [SerializeField] GameObject _pistol, _pistolDummy;
    [SerializeField] private float _pistolFR = 0.5f;
    private Pistol _pistolScript;
    private float _canFire = 0;
    private bool _canShoot = false;
    private int _pistolAmmo = 12;
    private bool _outOfAmmo = false;

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

    [Header("Audio")]
    [SerializeField] private AudioSource _walkSFX;
    [SerializeField] private AudioSource _goreSFX, _shotgunSFX, _speedSFX, _shieldSFX;
    [SerializeField] private AudioSource _bGMusic, _gOMusic;
    private bool _isWalking = false;



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
        _canRoll = true;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canRoll == true)
        {
            if (_direction.x > 0.1f)
            {
                StartCoroutine(Roll(0));
            }
            else if (_direction.x < -0.1f)
            {
                StartCoroutine(Roll(1));
            }
        }

        if (_canMove == true)
        {
            if (_direction.x > 0.1f || _direction.x < -0.1f)
            {
                IsWalking();
            }
            else
            {
                _walkSFX.Stop();
                _isWalking = false;
            }
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
                _shotgunAmmo--;

                if (_shotgunAmmo == 0)
                {
                    DrawShotgun();
                }
            }
            else if (_outOfAmmo == false)
            {
                _canFire = Time.time + _pistolFR;
                _pistolScript.Fire();
                _pistolAmmo--;
                _uIManager.Ammo(_pistolAmmo);

                if (_pistolAmmo == 0)
                {
                    Debug.Log("Out of Ammo!");
                    _outOfAmmo = true;
                    _spawnManager.SpawnAmmo();
                }

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
            _shotgunAmmo = 4;
            _shotgunSFX.Play();
            if (_shotgunActive == false)
            {
                DrawShotgun();
            }

        }

        if (other.tag == "Speed Powerup")
        {
            Destroy(other.gameObject);
            _speedSFX.Play();
            if (_speedBoostActive == false)
            {
                StartCoroutine(SpeedBoost());
            }
        }

        if (other.tag == "Shield Powerup")
        {
            Destroy(other.gameObject);
            _shieldLife = 4;
            _shieldSFX.Play();
            LoseLife();
        }

        if (other.tag == "Ammo Powerup")
        {
            Destroy(other.gameObject);
            _pistolAmmo = 12;
            _uIManager.Ammo(_pistolAmmo);
            _outOfAmmo = false;
        }

        if (other.tag == "Ammo Drop")
        {
            Destroy(other.gameObject);
            _pistolAmmo += 6;
            if(_pistolAmmo > 12)
            {
                _pistolAmmo = 12;
            }
            _uIManager.Ammo(_pistolAmmo);
            _outOfAmmo = false;
        }

        if (other.tag == "Health Powerup")
        {
            Destroy(other.gameObject);
            if(_lives < 3)
            {
                _lives++;
                LifeCounter(true);
            }
        }
    }

    void LoseLife()
    {
        if (_invincible == false)
        {
            if (_shieldLife > 0)
            {
                switch (_shieldLife)
                {
                    case 4:
                        _shields3.transform.gameObject.SetActive(true);
                        _shields2.transform.gameObject.SetActive(false);
                        _shields1.transform.gameObject.SetActive(false);
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
                _bloodFX.Play(true);
                _goreSFX.Play();
                LifeCounter(false);
            }
            else
            {
                Debug.Log("GAME OVER!");

            }
        }
    }

    void LifeCounter(bool Heal)
    {
        if (Heal == false)
        {
            switch (_lives)
            {
                case 3:
                    _heart3.gameObject.SetActive(false);
                    _bloodDrip1.Play(true);
                    _lives--;
                    break;
                case 2:
                    _heart2.gameObject.SetActive(false);
                    _bloodDrip2.Play(true);
                    _lives--;
                    break;
                case 1:
                    _heart1.gameObject.SetActive(false);
                    _lives--;
                    _canMove = false;
                    _anim.SetTrigger("Dead");
                    Time.timeScale = 0;
                    _bGMusic.Stop();
                    _gOMusic.Play();
                    _gameOverCanvas.SetActive(true);
                    break;
            }
        }

        if (Heal == true)
        {
            switch (_lives)
            {
                case 3:
                    _heart3.gameObject.SetActive(true);
                    _bloodDrip1.Pause(true);
                    break;
                case 2:
                    _heart2.gameObject.SetActive(true);
                    _bloodDrip2.Pause(true);
                    break;
            }
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

    private void IsWalking()
    {
        if (_isWalking == false)
        {
            _walkSFX.pitch = (Random.Range(0.85f, 1.15f));
            _walkSFX.Play();
            _isWalking = true;
        }

    }

    IEnumerator Roll(int direction)
    {
        _anim.SetBool("Roll", true);
        _canMove = false;
        _canShoot = false;
        _invincible = true;
        _canRoll = false;
        switch (direction)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
        }
        yield return new WaitForSeconds(0.8f);
        _anim.SetBool("Roll", false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(transform.position.x, 0, -46.5f);
        _canMove = true;
        _canShoot = true;
        _invincible = false;
        _canRoll = true;
    }

}
