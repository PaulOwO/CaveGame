using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 20f;
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private UI _ui;
    private GameObject _bulletInstance;
    private Rigidbody2D _bodyInstance;
    private bool _reloading = false;
    private float _bulletCharged = 6f;

    private void Start()
    {
        _ui = FindObjectOfType<UI>();
        _ui.Bullet = _bulletCharged;
        _ui.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (_bulletCharged > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else if (_reloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        _bulletCharged = _bulletCharged - 1f;
        _bulletInstance = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        _bodyInstance = _bulletInstance.GetComponent<Rigidbody2D>();
        _bodyInstance.rotation += 180;
        _bodyInstance.AddForce(_firePoint.right * _bulletSpeed, ForceMode2D.Impulse);
        _ui.Bullet = _bulletCharged;
        _ui.UpdateUI();
    }
    
    IEnumerator Reload()
    {
        _reloadSound.Play();
        _reloading = true;
        _ui.Bullet = -1; //-1 = reloading
        _ui.UpdateUI();
        Debug.Log("Reloading");
        yield return new WaitForSeconds(2f);
        _bulletCharged = 6f;
        _ui.Bullet = _bulletCharged;
        _ui.UpdateUI();
        _reloading = false;
    }
}
