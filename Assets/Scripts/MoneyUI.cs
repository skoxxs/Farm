using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text myField;
    [SerializeField] private GameObject image;
    [SerializeField] private float speedAddMoney = 1;

    private float currentOnUI = 0;
    private int allMoney = 0;
    private bool canAddMoneyOnUI = false;
    private Animation animationImageCoint;
    void Start()
    {
        animationImageCoint = image.GetComponent<Animation>();
        myField.text = currentOnUI.ToString();
    }

    void Update()
    {
        if (canAddMoneyOnUI)
        {
            if (currentOnUI < allMoney)
            {
                currentOnUI += speedAddMoney * Time.deltaTime;
                myField.text = ((int)currentOnUI).ToString();
            }
            else
            {
                currentOnUI = allMoney;
                myField.text = currentOnUI.ToString();
                canAddMoneyOnUI = false;
            }
        }
    }

    public void TakeMoney(int money)
    {
        animationImageCoint.Play();
        allMoney += money;
        canAddMoneyOnUI = true;
    }

    public Transform GetImageTransf()
    {
        return image.transform;
    }
}
