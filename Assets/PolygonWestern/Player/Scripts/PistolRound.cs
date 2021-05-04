using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRound : MonoBehaviour
{

    Rigidbody _rigid;
    [SerializeField] private int _speed = 50;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        StartCoroutine(DestroyTimer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigid.MovePosition(_rigid.position + Vector3.forward * _speed * Time.fixedDeltaTime);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


}
