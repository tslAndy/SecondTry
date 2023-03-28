using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private  Player player;

    private void Update()
    {
        if (!player.IsInShadow)
            Debug.Log("Can see player");
    }
}
