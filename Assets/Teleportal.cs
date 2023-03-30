using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Teleportal : MonoBehaviour
{
    [SerializeField]
    Transform portal;

    [SerializeField]
    Teleportal nextTeleportal;


    [NonSerialized]
    public bool isTeleported;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTeleported)
        { 
            collision.gameObject.transform.position = portal.position;
            nextTeleportal.isTeleported = true;
        }    
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isTeleported = false;
    }
}