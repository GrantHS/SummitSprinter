using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GasMeter : MonoBehaviour
{
    public Slider GasSlider;
    private float gasDrain = 0.5f;
    private float RefillGas = 10.0f;

    public float currentValue;

    public float GasDrain { get => gasDrain; set => gasDrain = value; }

    private void Awake()
    {       
        //GameManager.Instance.gasLevel = GasSlider.maxValue;
        //GasSlider.value = GameManager.Instance.gasLevel;
        currentValue = GasSlider.maxValue;
        GasSlider.value = currentValue;
    }

    void Update()
    {
        //Debug.Log("Gas Level: " + GameManager.Instance.gasLevel);
        //GameManager.Instance.gasLevel -= gasDrain * Time.deltaTime;
       // GameManager.Instance.gasLevel = Mathf.Max(GameManager.Instance.gasLevel, GasSlider.minValue);
        //GasSlider.value = GameManager.Instance.gasLevel;

        /* Temp disable
        currentValue -= GasDrain * Time.deltaTime;
        currentValue = Mathf.Max(currentValue, GasSlider.minValue);
        GasSlider.value = currentValue;
        */
    }

    public void FillTank()
    {
        currentValue = GasSlider.maxValue;
        GasSlider.value = currentValue;
        Debug.Log("Filled Tank!");
    }
}
