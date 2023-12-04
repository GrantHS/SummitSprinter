using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMeter : MonoBehaviour
{
    public Transform Jeep;
    public float targetDistance = -2000;
    public Slider DisSlider;

    // Update is called once per frame
    void Update()
    {
        float playerXPosition = Jeep.position.x;

        float remainingDistance = Mathf.Max(0f, targetDistance - playerXPosition);

        DisSlider.value = 1f - (remainingDistance / targetDistance);

    }
}
