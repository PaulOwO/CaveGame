using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private Animator _animator;
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
        _animator = GetComponent<Animator>();
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
            SetAnimatorMove(_direction);
        }
        else
        {
            _animator.SetLayerWeight(1, 0);
        }
    }

    private void TakeInput() // Takes input to move the player
    {
        _direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)))
        {
            _direction += Vector2.up;
            _stateDir = State.UP;
        }
        if (Input.GetKey(KeyCode.S)|| (Input.GetKey(KeyCode.DownArrow)))
        {
            _direction += Vector2.down;
            _stateDir = State.DOWN;
        }
        if (Input.GetKey(KeyCode.A)|| (Input.GetKey(KeyCode.LeftArrow)))
        {
            _direction += Vector2.left;
            _stateDir = State.LEFT;
        }
        if (Input.GetKey(KeyCode.D)|| (Input.GetKey(KeyCode.RightArrow)))
        {
            _direction += Vector2.right;
            _stateDir = State.RIGHT;
        }
    }

    private void SetAnimatorMove(Vector2 _direction)
    {
        _animator.SetLayerWeight(1, 1);
        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
    }

    [SerializeField] public float _health = 3.0f;
    
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
        Debug.Log("take damage");
        _health -= _damage;
    }
}
