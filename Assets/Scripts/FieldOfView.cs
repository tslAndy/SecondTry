using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float radius = 5;

    [Range(1,360)]
    private float angle = 45;

    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private LayerMask obstructionLayer;

    [SerializeField]
    private GameObject playerRef;

    public bool CanSeePlayer { get; set; }


}