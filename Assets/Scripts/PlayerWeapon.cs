using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private float weaponCooldown;
    [SerializeField] private float maxDelayBetweenKills, comboDeltaV;
    [SerializeField] private Collider2D weaponCollider;
    [SerializeField] private Player player;

    public delegate void AttackAction();
    public static event AttackAction OnAttack;

    public delegate void UpdateComboAction();
    public static event UpdateComboAction OnComboUpdate;

    private int _killedInCombo;
    public int KilledInCombo
    {
        get { return _killedInCombo; }
    }

    private float _lastTimeKilledEnemy;
    private float _lastAttacked;
    private float _playerStartSpeed;


    private void Start()
    {
        EnemyHealth.OnDeathAction += UpdateCombo;
        _playerStartSpeed = player.MovementSpeed;
    }

    private void Update()
    {
        if (Time.time - _lastTimeKilledEnemy > maxDelayBetweenKills && player.MovementSpeed != _playerStartSpeed)
        {
            Debug.Log("Setting to default");
            _killedInCombo = 0;
            player.MovementSpeed = _playerStartSpeed;
        }

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
        OnAttack.Invoke();
        yield return new WaitForSeconds(weaponCooldown);
        weaponCollider.enabled = false;
    }

    private void UpdateCombo()
    {
        _lastTimeKilledEnemy = Time.time;

        if (_killedInCombo == 0)
        {
            _killedInCombo++;
            player.MovementSpeed = _playerStartSpeed;
            return;
        }

        // else section

        // if too much time between kills
        if (Time.time - _lastTimeKilledEnemy > maxDelayBetweenKills)
        {
            _killedInCombo = 1;
            return;
        }

        // start adding speed after two or more kills
        _killedInCombo++;
        player.MovementSpeed += comboDeltaV;
        OnComboUpdate.Invoke();
    }
}
