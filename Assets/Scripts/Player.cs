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
    private Vector2 _mousePosition;
    private Vector2 _lookDirection;

    private float _angle; 
    private float moveSpeed = 5f;
    private Vector2 movement;
    
    void Update()
    {
       _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _lookDirection = _mousePosition - _body.position;
        _angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _body.rotation = _angle;
        _body.MovePosition(_body.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
