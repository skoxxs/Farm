using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointToUI : MonoBehaviour
{
    [SerializeField] private float rangeToImage = 1;
    [SerializeField] private float moveSpeed = 10;

    private int moneyToUI = 0;
    private bool startMove = false;
    private Transform target;

    void Update()
    {
        if (startMove)
        {
            if (Vector3.Distance(this.transform.position, target.position) > rangeToImage)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                target.gameObject.GetComponentInParent<MoneyUI>().TakeMoney(moneyToUI);
                Destroy(this.gameObject);
            }
        }
    }

    public void MoveTo(Transform moneyUI,int money)
    {
        moneyToUI = money;
        target = moneyUI;
        startMove = true;
    }
}
