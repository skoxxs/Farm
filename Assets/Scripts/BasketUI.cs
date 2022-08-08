using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasketUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textField;

    public void UpDateCanvas(int current, int maxSaizeBasket)
    {
        slider.value = (float)current / (float)maxSaizeBasket;
        textField.text = current + " / " + maxSaizeBasket;
    }
}
