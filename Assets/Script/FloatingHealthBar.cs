using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        if (currentValue == 40)
        {
            fillImage.color = Color.Lerp(Color.yellow, Color.red, 0.5f); 
        }
        else if (currentValue == 20)
        {
            fillImage.color = Color.red;
        }
        else
        {
            fillImage.color = Color.green;
        }
    }
}
