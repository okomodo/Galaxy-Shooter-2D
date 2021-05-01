using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    
    [SerializeField] private int _speed = 2;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-4f, 4f), transform.position.y, transform.position.z);
    }
    void Update()
    {
        transform.position = (transform.position + Vector3.back *_speed * Time.deltaTime);
    }
}
