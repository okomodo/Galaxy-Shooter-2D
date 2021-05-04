using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyARound : MonoBehaviour
{
    Rigidbody _rigid;
    [SerializeField] private int _speed = 50;
    private Vector3 _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Enemy Target").transform.position;
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        transform.LookAt(_playerPos);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        _rigid.position = Vector3.MoveTowards(_rigid.position, _playerPos, _speed * Time.fixedDeltaTime);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
