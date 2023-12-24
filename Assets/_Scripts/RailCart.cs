using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody))]
public class RailCart : MonoBehaviour
{

    [SerializeField] private SplineContainer rail;

    private Spline currentSpline;

    private Rigidbody rb;

    public void HitJunction(Spline rail)
    {
        currentSpline = rail;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentSpline = rail.Splines[0];
    }

    private void FixedUpdate()
    {
        var native = new NativeSpline(currentSpline);
        float distance = SplineUtility.GetNearestPoint(native, transform.position, out float3 nearest,out float t);

        transform.position = nearest;
        
        Vector3 forward = Vector3.Normalize(native.EvaluateTangent(t));
        Vector3 up = native.EvaluateUpVector(t);
        
        var remappedForward = new Vector3(0,0,1);
        var remappedUp = new Vector3(0,1,0);
        var axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));
        
        transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;

        Vector3 engineForward = transform.forward;

        if (Vector3.Dot(rb.velocity,transform.forward) < 0)
        {
            engineForward *= -1;
        }

        rb.velocity = rb.velocity.magnitude * engineForward;
    }
}