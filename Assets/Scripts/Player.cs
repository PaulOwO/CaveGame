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

    private float _angle; 
    private float moveSpeed = 5f;
    private Vector2 movement;
    private float _offSet = -10;
    
    void Update()
    {
       _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
       _cursor.position = _mousePosition;
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");

       _camera.transform.position = this.transform.position + new Vector3(0, 0, _offSet);
    
       
       
    }

    private void FixedUpdate()
    {
        _lookDirection = _mousePosition - _body.position;
        _angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _body.rotation = _angle;
        _body.MovePosition(_body.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
