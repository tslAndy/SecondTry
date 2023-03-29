using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float radius = 5;

    [SerializeField]
    [Range(1,360)]
    private float angle = 45;

    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private LayerMask obstructionLayer;

    [SerializeField]
    private GameObject playerRef;

    public bool CanSeePlayer { get; set; }

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if(rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if(Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer) && EnemyScript.CanEnemiesSeePlayer)
                    CanSeePlayer = true;
                else 
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if(CanSeePlayer)
            CanSeePlayer = false;
    }
}