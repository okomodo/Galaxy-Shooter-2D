using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private int _speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 1f, 0f);
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        _rigid.MovePosition( _rigid.position + _direction * _speed * Time.fixedDeltaTime);
    }
}
