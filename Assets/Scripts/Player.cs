using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public static event Action PlayerEnteredInvicibility;
    public static event Action PlayerExitedInvicibility;
    [SerializeField] private float movementSpeed, rotationSpeed;
    [SerializeField] private float startEnergyAmount;
    [SerializeField] private float dashSpeed, dashDuration, dashSpent, dashTrailVisibleDuration;
    [SerializeField] private float invisibleSpent, afterAttackCooldown;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Light2D nightLight;
    [SerializeField] private MapManager mapManager;

    enum State
    {
        Idle,
        Run,
        DashStart,
        Dash
    }

    private State _currentState;
    private bool _invisible, _coolingDownAfterAttack;
    public float CurrentEnergyAmount { get; set; }
    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public bool Invisible
    {
        get { return _invisible; }
        set { _invisible = value; }
    }


    public bool IsInShadow { get; set; }

    private Vector2 _keyboardDirection, _mouseDirection;
    
    public delegate void UpdateLightAction();
    public static event UpdateLightAction OnLightUpdate;

    void Start()
    {
        _currentState = State.Idle;
        CurrentEnergyAmount = startEnergyAmount;
        PlayerWeapon.OnAttack += StartCooldownCoroutine;
        //trailRenderer.time = dashTrailVisibleDuration;
    }
    
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        bool dashPressed = Input.GetButtonDown("Fire2");
        bool invisiblePressed = Input.GetButton("Jump");
        bool invisibleEntred = Input.GetButtonDown("Jump");
        bool invisibleExited = Input.GetButtonUp("Jump");

        _keyboardDirection = new Vector2(x, y);
        _keyboardDirection.Normalize();

        _mouseDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        _mouseDirection.Normalize();

        //Debug.Log(_keyboardDirection);
        
        if (_currentState == State.Dash)
            return;

        _invisible = invisiblePressed && (CurrentEnergyAmount - invisibleSpent >= 0) && !_coolingDownAfterAttack;
        if (invisibleEntred)
            PlayerEnteredInvicibility?.Invoke();
        if (invisibleExited)
            PlayerExitedInvicibility?.Invoke();
        if (dashPressed && CurrentEnergyAmount - dashSpent >= 0 && !_invisible)
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
            CurrentEnergyAmount -= invisibleSpent * Time.deltaTime;                   
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b,_invisible ? 0.5f : 1.0f);
        
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, _mouseDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        float speedFactor = mapManager.GetSpeedFactor(transform.position);

        switch (_currentState)
        {
            case State.Idle:
                rb.velocity = Vector2.zero;
                break;
            case State.Run:
                rb.velocity = _keyboardDirection * MovementSpeed * speedFactor;
                break;
            case State.DashStart:
                StartCoroutine(DashCoroutine());
                break;
        }
        
        IsInShadow = true;
        OnLightUpdate.Invoke();
    }

    private IEnumerator DashCoroutine()
    {
        trailRenderer.enabled = true;
        rb.velocity = _mouseDirection * dashSpeed;
        _currentState = State.Dash;
        CurrentEnergyAmount -= dashSpent;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        _currentState = State.Idle;
        yield return new WaitForSeconds(dashTrailVisibleDuration);
        trailRenderer.enabled = false;
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

    private void SwitchNightLight() => nightLight.enabled = !nightLight.enabled;
}
