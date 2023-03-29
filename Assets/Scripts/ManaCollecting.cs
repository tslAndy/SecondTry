using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCollecting : MonoBehaviour
{
    [SerializeField] private float manaPoints; 
    [SerializeField] private Player player;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Mana"))
            return;
        player.CurrentEnergyAmount += manaPoints;
    }
}
