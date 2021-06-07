using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private float _slowBulletSpeed = 2.0f;
    [SerializeField] private float _fastBulletSpeed = 5.0f;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private GameObject _explosionPrefab;

    private const float _shootPeriod = 1.0f;
    private float _shootCooldownTime = _shootPeriod;
    private const float _colorPeriod = 0.25f;
    private float _colorCooldownTime = _colorPeriod;
   

    
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
        
        _shootCooldownTime += Time.deltaTime;
        _colorCooldownTime += Time.deltaTime;

        if (_sprite.color == Color.yellow && _colorCooldownTime > _colorPeriod)
        {
            _sprite.color = Color.white;
        }
        
        if  (_shootCooldownTime > _shootPeriod)   //        if  (cooldownTime > cooldownPeriod)   

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
        GameObject _animation = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(_animation, 5f);
        Destroy(this.gameObject);
    }

    private void Shoot()
    {
        for (int i = 0; i < 20; i++)
        {
            var rotation = Quaternion.AngleAxis(i / 20.0f * 360, Vector3.forward);
            var direction = rotation * Vector3.left;
            _bulletInstance = Instantiate(_bulletPrefab, _firePoint.position, rotation);
            _bodyInstance = _bulletInstance.GetComponent<Rigidbody2D>();
            _bodyInstance.AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
            _shootCooldownTime = 0.0f;
            _body.rotation += 18;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamage();
            
        }
    }

    private void TakeDamage()
    {
        _sprite.color = Color.yellow;
        _health -= _damage;
        _colorCooldownTime = 0;
    }
}
