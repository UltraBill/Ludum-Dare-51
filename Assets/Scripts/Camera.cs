using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public float boundmin = 0f;
    public float boundmax = 527f;

    void FixedUpdate()
    {
        Vector3 nextPos = new Vector3 (target.position.x , 0, -20);
        Vector3 smoothPos = Vector3.Lerp(transform.position, nextPos, smoothSpeed);
        if(smoothPos.x > boundmin && smoothPos.x < boundmax)
            transform.position = smoothPos;
    }
}

