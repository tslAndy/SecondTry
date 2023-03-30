using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CustomCamera : MonoBehaviour
{
    [SerializeField] private float speed, maxDistance;
    [SerializeField] private Transform followTransform;
    [SerializeField] private CinemachineVirtualCamera cam;
    private void Update()
    {
        if (Input.GetKey((KeyCode.LeftShift)))
            MoveAway();
        else
            MoveToPlayer();
    }

    private void MoveAway()
    {
        cam.Follow = null;
        cam.LookAt = null;
        Vector3 distDiff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        distDiff = new Vector3(distDiff.x, distDiff.y, 0);
        
        Vector3 direction = distDiff.normalized;
        Vector3 delta = direction * speed * Time.deltaTime;
        
        if (Vector3.Distance(followTransform.position, transform.position + delta) > maxDistance)
            return;
        
        transform.position += delta;
    }

    private void MoveToPlayer()
    {
        if (cam.Follow != null)
            return;
        
        
        Vector3 distDiff = followTransform.position - transform.position;
        distDiff = new Vector3(distDiff.x, distDiff.y, 0);

        Vector3 direction = distDiff.normalized;
        Vector3 delta = direction * speed * Time.deltaTime;

        if (distDiff.magnitude > 0.1f)
        {
            transform.position += delta;
        }
        else
        {
            cam.Follow = followTransform;
            cam.LookAt = followTransform;

        }
    }
}
