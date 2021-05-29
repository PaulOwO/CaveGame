using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    
    private Vector2 _direction;
    private Vector2 _targetPos;
    private float _angle;
    private float moveSpeed = 20f;




    public enum State
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    private State _stateDir;

    void Start()
    {
        
    }
    void Update()
    {

        if (_health < 1)
        {
            Death();
        }

        TakeInput();
        Move();
    }

    private void Death()
    {
        Destroy(gameObject);
    }


    private void Move() //Moves the player
    {
        transform.Translate(_direction * (moveSpeed * Time.deltaTime));
        if (_direction.x != 0 || _direction.y != 0)                          // PUT DEADZONE HERE
        {
            
        }
        
    }

    private void TakeInput() // Takes input to move the player
    {
        _direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _direction += Vector2.up;
            _stateDir = State.UP;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _direction += Vector2.down;
            _stateDir = State.DOWN;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _direction += Vector2.left;
            _stateDir = State.LEFT;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _direction += Vector2.right;
            _stateDir = State.RIGHT;
        }
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

