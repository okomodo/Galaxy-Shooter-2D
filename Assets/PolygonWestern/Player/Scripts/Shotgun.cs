using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private Transform _barrelEnd;
    [SerializeField] private GameObject _shotgunRound;
    [SerializeField] private ParticleSystem _muzzleFlash;
    public void Fire()
    {
        _muzzleFlash.Play();
        Instantiate(_shotgunRound, _barrelEnd.position, Quaternion.identity);
    }

}
