using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMeter : MonoBehaviour
{
    public Transform Jeep;
    public float xPos = -100f;
    public Slider DisSlider;

    private float previousXPosition;
    // Update is called once per frame
    void Update()
    {
        
        float playerXPosition = Jeep.position.x;
        float MinusX = previousXPosition - playerXPosition;
        DisSlider.value += MinusX / Mathf.Abs(xPos);
        

      
        previousXPosition = playerXPosition;
    }


}
