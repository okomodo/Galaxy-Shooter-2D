using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Transform _barrelEnd;
    [SerializeField] private GameObject _pistolRound;


    public void Fire()
    {
        Instantiate(_pistolRound, _barrelEnd.position, Quaternion.identity);
    }

}
