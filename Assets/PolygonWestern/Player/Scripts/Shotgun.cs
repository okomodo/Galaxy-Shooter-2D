using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private Transform _barrelEnd;
    [SerializeField] private GameObject _shotgunRound;
    public void Fire()
    {
        Instantiate(_shotgunRound, _barrelEnd.position, Quaternion.identity);
    }

}
