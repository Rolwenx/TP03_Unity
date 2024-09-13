using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    

    public void UpdateHealthBar(float currentValue, float maxValue){

        slider.value = currentValue / maxValue;
    }
}
