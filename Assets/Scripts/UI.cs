using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.PlayerLoop;


public class UI : MonoBehaviour
{
    [SerializeField] private Text _bulletCounterText;
    [SerializeField] private Text _healthText;

    private float _health = 0;
    private float _bullet = 0;

    public float Health
    {
        get => _health;
        set => _health = value;
    }

    public float Bullet
    {
        get => _bullet;
        set => _bullet = value;
    }
    
    public void UpdateUI()
    {
        _healthText.text = "HEALTH : " + _health;
        if (_bullet == -1)
        {
            _bulletCounterText.text = "Bullet : Reloading";
        }
        else
        {
            _bulletCounterText.text = "Bullet : " + _bullet;
        }
    }
}
