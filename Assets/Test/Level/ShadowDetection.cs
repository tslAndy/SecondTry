using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowDetection : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Light2D light2D;
    [SerializeField] private Player player;
    [SerializeField] ContactFilter2D contactFilter2D;
    private void Start() => Player.OnLightUpdate += CheckIfInShadow;
    private void CheckIfInShadow()
    {
        Vector3 distDiff = playerTransform.position - transform.position;
        if (distDiff.magnitude > light2D.pointLightOuterRadius)
            return;
        int hits = Physics2D.Raycast(transform.position, distDiff.normalized, contactFilter2D, new RaycastHit2D[10],
            distDiff.magnitude);
        if (hits == 1)
            player.IsInShadow = false;
    }
}
