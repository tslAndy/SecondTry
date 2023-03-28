using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float weaponCooldown;
    [SerializeField] private Collider2D weaponCollider;

    private float _lastAttacked;

    private void Update()
    {
        if (!Input.GetButtonDown("Fire1"))
            return;
        if (Time.time < _lastAttacked + weaponCooldown)
            return;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        weaponCollider.enabled = true;
        _lastAttacked = Time.time;
        yield return new WaitForSeconds(weaponCooldown);
        weaponCollider.enabled = false;
    }
}
