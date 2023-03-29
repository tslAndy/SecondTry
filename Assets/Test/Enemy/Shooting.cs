using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float speed, shootRate;
    [SerializeField] private GameObject projectilePrefab;

    float _lastShootTime;

    public void Shoot(Vector2 direction)
    {
        if (Time.time < _lastShootTime + shootRate)
            return;
        GameObject projectile = Instantiate(projectilePrefab, transform);
        projectile.transform.SetParent(null);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;    
    }
}
