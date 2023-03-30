using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite _activatedSprite;
    [SerializeField] private GameObject[] _objectsToDeactivate;
    private SpriteRenderer _renderer;
    private bool _activated = false;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_activated) return;

            _activated = true;
            _renderer.sprite = _activatedSprite;
            foreach (GameObject obj in _objectsToDeactivate)
            {
                obj.SetActive(false);
            }
        }
    }
}
