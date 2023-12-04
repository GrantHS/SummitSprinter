using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMeter : MonoBehaviour
{
    public Transform trackedObject;
    public Slider xAxisSlider;
    public float sliderSpeed = 1.0f;
    public float maxSliderValue = 21f; 
    private float previousXPosition;

    private void Update()
    {
        if (trackedObject != null)
        {
            float xPosition = trackedObject.position.x;

            if (xPosition != previousXPosition)
            {
                float xMovement = Mathf.Max(0f, previousXPosition - xPosition);

                UpdateSlider(xMovement);

                previousXPosition = xPosition;
            }
        }
    }

    private void UpdateSlider(float xMovement)
    {
        xAxisSlider.value += xMovement * sliderSpeed * Time.deltaTime;

         xAxisSlider.value = Mathf.Clamp(xAxisSlider.value, 0f, maxSliderValue);
    }
}
