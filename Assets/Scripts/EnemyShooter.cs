using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private float _slowBulletSpeed = 5.0f;
    [SerializeField] private float _fastBulletSpeed = 10.0f;
   

    
    private GameObject _bulletInstance;
    private Rigidbody2D _bodyInstance;
    private float _bulletSpeed;
    
    void Start()
    {
        _bulletSpeed = _slowBulletSpeed;
        //Shoot();
    }

   
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
        if (_health < 1)
        {
            Death();
        }
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }

    private void Shoot()
    {
        for (int i = 0; i < 20; i++)
        {
            _bulletInstance = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            _bodyInstance = _bulletInstance.GetComponent<Rigidbody2D>();
            _bodyInstance.rotation += 180;
            _bodyInstance.AddForce(_firePoint.up * _bulletSpeed, ForceMode2D.Impulse);
            _body.rotation += 1.0f;
            SwitchBullet();
        }
    }

    private void SwitchBullet()
    {
        if (_bulletSpeed == _fastBulletSpeed)
        {
            _bulletSpeed = _slowBulletSpeed;
        }
        else if (_bulletSpeed == _slowBulletSpeed)
        {
            _bulletSpeed = _fastBulletSpeed;
        }
    }
    
    
    [SerializeField] private float _health = 6.0f;
    private float _damage = 1.0f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamage();
            
        }
    }

    private void TakeDamage()
    {
        _health -= _damage;
    }
}
