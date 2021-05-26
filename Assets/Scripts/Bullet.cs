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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("destroy bullet");
        Destroy(gameObject);
    }
}
