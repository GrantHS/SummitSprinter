using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GasMeter : MonoBehaviour
{
    public Slider GasSlider;
    private float gasDrain = 1.0f;
    private float RefillGas = 10.0f;

    public float currentValue;

    private void Awake()
    {
        currentValue = GasSlider.maxValue;
        GasSlider.value = currentValue;
    }

    void Update()
    {
        currentValue -= gasDrain * Time.deltaTime;
        currentValue = Mathf.Max(currentValue, GasSlider.minValue);
        GasSlider.value = currentValue;
    }
}
