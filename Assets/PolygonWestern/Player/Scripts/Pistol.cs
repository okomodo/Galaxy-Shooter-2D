using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Transform _barrelEnd;
    [SerializeField] private GameObject _pistolRound;
    [SerializeField] private ParticleSystem _muzzleFlash;


    public void Fire()
    {
        _muzzleFlash.Play();
        Instantiate(_pistolRound, _barrelEnd.position, Quaternion.identity);
    }

}
