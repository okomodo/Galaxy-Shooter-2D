using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private int _speed = 5;
    private Vector3 _direction;

    [SerializeField] private Transform _barrelEnd;
    [SerializeField] private GameObject _enemyPistolRound;

    [SerializeField] private float _pistolFR = 1;
    private float _pistolFRTime = 0;
    private bool _canFire = false;
    private bool _canMove = true;
    private bool _canWin = false;
    private int _currentlaps = 0;
    [SerializeField] private float _rowChangeWaitTime = 3f;
    private Animator _anim;
    private int _speedFloat;

    UIManager _uIManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-1.25f, 1.25f));
        _rigid = GetComponent<Rigidbody>();
        _speedFloat = Animator.StringToHash("Speed");
        _anim = GetComponent<Animator>();
        _uIManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        if(transform.position.x == -9)
        {
            _direction = Vector3.right;
        }
        else if(transform.position.x == 9)
        {
            _direction = Vector3.left;
        }
    }

    private void Update()
    {
        if (Time.time >= _pistolFRTime && _canFire == true)
        {
            _pistolFRTime = Time.time + _pistolFR;
            Instantiate(_enemyPistolRound, _barrelEnd.position, Quaternion.identity);
        }

        if(_currentlaps > 2)
        {
            _currentlaps = 0;
            StartCoroutine(ChangeRow());

            if( _canWin == true)
            {
                Debug.Log("BANG! You Lose");
                Destroy(gameObject);
            }
        }

        _anim.SetFloat(_speedFloat, _direction.x);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PistolRound")
        {
            _uIManager.AddToScore(100);
            Destroy(gameObject);
        }

        if (other.tag == "WinBox")
        {
            _canWin = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_canMove == true)
        {
            _rigid.MovePosition(_rigid.position + _direction * _speed * Time.fixedDeltaTime);
        }

        if (_rigid.position.x < 5 && _rigid.position.x > -5 && _canFire == false)
        {
            _canFire = true;
        }
        else if(_rigid.position.x > 10)
        {
            _direction = Vector3.left;
            _currentlaps++;
        }
        else if(_rigid.position.x < -10)
        {
            _direction = Vector3.right;
            _currentlaps++;
        }
        else
        {
            _canFire = false;
        }
    }

    IEnumerator ChangeRow()
    {
        yield return new WaitForSeconds(0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 11);
        _canMove = false;
        yield return new WaitForSeconds(_rowChangeWaitTime);
        _canMove = true;
    }

}
