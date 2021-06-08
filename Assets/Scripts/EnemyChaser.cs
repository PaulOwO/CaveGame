using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    private const float _colorPeriod = 0.25f;
    private float _colorCooldownTime = _colorPeriod;
    [SerializeField] private float _health = 3.0f;
    private float _damage = 1.0f;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _bulletSpeed = 1.0f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _radiusTransform;
    private float _moveSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, _radiusTransform.transform.position, _moveSpeed * Time.deltaTime);
        _colorCooldownTime += Time.deltaTime;

        if (_sprite.color == Color.red && _colorCooldownTime > _colorPeriod)
        {
            _sprite.color = Color.white;
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
        for (int i = 0; i < 20; i++)
        {
            var rotation = Quaternion.AngleAxis(i / 20.0f * 360, Vector3.forward);
            var direction = rotation * Vector3.left;
            var _bulletInstance = Instantiate(_bulletPrefab, transform.position, rotation);
            var _bodyInstance = _bulletInstance.GetComponent<Rigidbody2D>();
            _bodyInstance.AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
        }
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            TakeDamage();
        }
    }
    
    private void TakeDamage()
    {
        _colorCooldownTime = 0f;
        _sprite.color = Color.red;
        _health -= _damage;
    }
}
