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
    private float toatalDis;

    //public Text Distance;
    // Update is called once per frame
    void Update()
    {
        
        float playerXPosition = Jeep.position.x;
        float disTraveled = Mathf.Abs(previousXPosition - playerXPosition);
        toatalDis += disTraveled;

        DisSlider.value = toatalDis / Mathf.Abs(xPos);

      
        previousXPosition = playerXPosition;
        // Debug.Log("Dis Traveled:" + toatalDis);


        //Distance.text = "Distance: " + Mathf.RoundToInt(toatalDis).ToString();
    }


}
