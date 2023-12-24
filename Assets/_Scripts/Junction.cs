using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Junction : MonoBehaviour
{
    [SerializeField] private SplineContainer rail;

    [Tooltip("List of indices representing positions in the list of splines in the spline container that " +
             "are available  to switch to from this junction")]
    [SerializeField] private List<int> rails;
    
    [Tooltip("Index representing the position in the rails list which represents the spline to switch the cart to")]
    [SerializeField] private int currentRail = 0;

    [SerializeField] private Slider slider;
    private bool hasSlider = false;

    private void Start()
    {
        if (slider != null)
        {
            hasSlider = true;
        }
    }

    private void Update()
    {
        if (hasSlider)
        {
            currentRail = (int)slider.value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RailCart railCart = other.GetComponent<RailCart>();
        if (railCart != null)
        {
            railCart.HitJunction(rail[rails[currentRail]]);
        }
    }
}
