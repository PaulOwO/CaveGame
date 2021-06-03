using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cursor;
    [SerializeField] private float _cameraOffSet = -10;
    private Vector2 _mousePosition;
    private Vector2 _lookDirection;

    
    private float _angle;
    private Vector2 movement;

    void Start()
    {

    }


    void Update()
    {
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _cursor.position = _mousePosition;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        _camera.transform.position = this.transform.position + new Vector3(0, 0, _cameraOffSet);
    }

    private void FixedUpdate()
    {
        _lookDirection = _mousePosition - _body.position;
        _angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        _body.rotation = _angle;

    }

}