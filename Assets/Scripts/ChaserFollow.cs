using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserFollow : MonoBehaviour
{
 [SerializeField] private float _moveSpeed = 0.6f;
 private bool followingPlayer_ = false;
 [SerializeField] private GameObject _player;
 [SerializeField] private string PlayerPrefab;
 [SerializeField] private Transform _chasserTransform;
   
 private void Start()
 {
     _player = GameObject.Find(PlayerPrefab);
 }

 private void Update()
 {
     if (followingPlayer_) 
     {
          Chase();
     }
 }

 private void Chase()
 {
     transform.position =
         Vector2.MoveTowards(transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
 }
   
 private void OnTriggerStay2D(Collider2D other)
 {
     if (other.gameObject.CompareTag("Player")) 
     {
               Debug.Log("Following Player!");
               followingPlayer_ = true;
     }
 }
   
 private void OnTriggerExit2D(Collider2D other)
 {
     if (other.gameObject.CompareTag("Player"))
     {
               Debug.Log("Stopped following Player!");
               followingPlayer_ = false;
     }
 }
}
