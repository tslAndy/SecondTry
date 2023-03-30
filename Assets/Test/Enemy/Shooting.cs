using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float speed, shootRate;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private AudioSource shootSound; 

    float _lastShootTime;

    public void Shoot(Vector2 direction)
    {
        Debug.Log($"{Time.time} < {_lastShootTime} + {shootRate}  ({_lastShootTime + shootRate})");
        if (Time.time < _lastShootTime + shootRate)
            return;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawner);
        projectile.transform.SetParent(null);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        shootSound.Play();
        _lastShootTime = Time.time;
    }
}
