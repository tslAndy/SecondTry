using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleProp : MonoBehaviour
{
    private Animator _animator;
    private Collider2D _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger("IsBroken");
            _collider.enabled = false;
        }
    }
}
