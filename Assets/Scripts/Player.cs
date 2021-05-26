using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cursor;
    private Vector2 _mousePosition;
    private Vector2 _lookDirection;

    private float _cameraOffSet = -10;
    private float _angle; 
    private float moveSpeed = 20f;
    private Vector2 movement;
    
    
    void Update()
    {
       _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
       _cursor.position = _mousePosition;
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");

       _camera.transform.position = this.transform.position + new Vector3(0, 0, _cameraOffSet);

       if (_health < 1)
       {
           Death();
       }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _lookDirection = _mousePosition - _body.position;
        _angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _body.rotation = _angle;
        _body.MovePosition(_body.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    [SerializeField] private float _health = 3.0f;
    private float _damage = 1.0f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health -= _damage;
    }
}
