using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed, rotationSpeed;
    [SerializeField] private float startEnergyAmount;
    [SerializeField] private float dashSpeed, dashDuration, dashSpent;
    [SerializeField] private float invisibleSpent, afterAttackCooldown;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer _trailRenderer;

    enum State
    {
        Idle,
        Run,
        DashStart,
        Dash
    }

    private State _currentState;
    private bool _invisible, _coolingDownAfterAttack;
    private float _currentEnergyAmount;

    private Vector2 _keyboardDirection, _mouseDirection;

    void Start()
    {
        _currentState = State.Idle;
        _currentEnergyAmount = startEnergyAmount;
        PlayerWeapon.OnAttack += StartCooldownCoroutine;
        _trailRenderer.time = dashDuration;
    }
    
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        bool dashPressed = Input.GetButtonDown("Fire2");
        bool invisiblePressed = Input.GetButton("Jump");

        _keyboardDirection = new Vector2(x, y);
        _keyboardDirection.Normalize();

        _mouseDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        _mouseDirection.Normalize();

        //Debug.Log(_keyboardDirection);
        
        if (_currentState == State.Dash)
            return;

        _invisible = invisiblePressed && (_currentEnergyAmount - invisibleSpent >= 0) && !_coolingDownAfterAttack;
        
        if (dashPressed && _currentEnergyAmount - dashSpent >= 0 && !_invisible)
            _currentState = State.DashStart;
        else if (_keyboardDirection == Vector2.zero)
            _currentState = State.Idle;
        else
            _currentState = State.Run;
        
        UpdateState();
    }

    private void UpdateState()
    {
        if (_invisible)
            _currentEnergyAmount -= invisibleSpent * Time.deltaTime;
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b,_invisible ? 0.5f : 1.0f);
        
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _mouseDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        switch (_currentState)
        {
            case State.Idle:
                rb.velocity = Vector2.zero;
                break;
            case State.Run:
                rb.velocity = _keyboardDirection * moveSpeed;
                break;
            case State.DashStart:
                StartCoroutine(DashCoroutine());
                break;
        }
    }

    private IEnumerator DashCoroutine()
    {
        _trailRenderer.enabled = true;
        rb.velocity = _mouseDirection * dashSpeed;
        _currentState = State.Dash;
        _currentEnergyAmount -= dashSpent;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        _trailRenderer.enabled = false;
        _currentState = State.Idle;
    }

    private IEnumerator AfterAttackCooldownCoroutine()
    {
        _coolingDownAfterAttack = true;
        yield return new WaitForSeconds(afterAttackCooldown);
        _coolingDownAfterAttack = false;
    }

    private void StartCooldownCoroutine()
    {
        if (_invisible)
            StartCoroutine(AfterAttackCooldownCoroutine());
    }
}
