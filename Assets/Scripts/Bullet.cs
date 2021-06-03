using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private AudioSource _shoutingSound;
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
                Destroy(this.gameObject);
            }
        }

        if (this.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
            {
                //Debug.Log("destroy bullet");
                Destroy(this.gameObject);
            }
        }
    }
}
