using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioSource _shoutingSound;
    [SerializeField] private GameObject _explosionPrefab;
    private void Start()
    {
        _shoutingSound.Play();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "PlayerBullet")
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall")
            {
                //Debug.Log("destroy bullet");
                GameObject _animation = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(_animation, 0.5f);
                Destroy(this.gameObject);
            }
        }

        if (this.gameObject.tag == "EnemyBullet")
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
            {
                //Debug.Log("destroy bullet");
                GameObject _animation = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(_animation, 0.5f);
                Destroy(this.gameObject);
            }
        }
    }
}
