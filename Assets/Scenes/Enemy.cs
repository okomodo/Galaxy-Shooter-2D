using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigid;
    [SerializeField] private int _speed = 10;
    private Vector3 _direction = Vector3.left;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 50);
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (transform.position.x > 9)
        {
            _direction = Vector3.left;
        }
        else if (transform.position.x < -9)
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
