using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class UI : MonoBehaviour
{
    [SerializeField] private Text _bulletCounterText;
    [SerializeField] private Text _healthText;
    [SerializeField] private Player _player;
    [SerializeField] private Shooting _shooting;

    // Update is called once per frame
    void Update()
    {
        _healthText.text = "HEALTH : " + _player._health;
       
        if (_shooting._reloading == true)
        {
            _bulletCounterText.text = "Bullet : Reloading" ;
        }
        else
        {
            _bulletCounterText.text = "Bullet :" + _shooting._bulletCharged;
        }

    }
}
