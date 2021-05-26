using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 20f;
    

    private GameObject _bulletInstance;
    private Rigidbody2D _bodyInstance;
    private float _bulletCharged = 6f;
    private bool _reloading = false;

    // Update is called once per frame
    void Update()
    {
        if (_bulletCharged > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else if (_reloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        _bulletCharged = _bulletCharged - 1f;
        _bulletInstance = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        _bodyInstance = _bulletInstance.GetComponent<Rigidbody2D>();
        _bodyInstance.AddForce(_firePoint.up * _bulletSpeed, ForceMode2D.Impulse);
    }
    
    IEnumerator Reload()
    {
        _reloading = true;
        Debug.Log("Reloading");
        yield return new WaitForSeconds(2f);
        _bulletCharged = 6f;
        _reloading = false;
    }
}
