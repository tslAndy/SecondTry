using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRestore : MonoBehaviour
{
    [SerializeField] private float restoreSpeed = 1.0f;
    [SerializeField] private Player _player;

    private void Update()
    {
        _player.CurrentEnergyAmount += restoreSpeed * Time.deltaTime;
    }
}