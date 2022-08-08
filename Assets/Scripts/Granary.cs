using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granary : MonoBehaviour
{
    [SerializeField] private MoneyUI moneyUI;
    [SerializeField] private GameObject prefabCoint;
    [SerializeField] private Transform pointSpavnCoint;
    [SerializeField] private float timeToSale = 3;
    [SerializeField] private float timeToGetItemFromBasket = 0.2f;

    private List<Item> itemsList = new List<Item>();
    private float currentTimeToSale = 0;
    private float CurrentTimeToTake = 0;
    private bool playerIn = false;
    private Basket basket;

    void Update()
    {
        if(itemsList.Count > 0)
        {
            currentTimeToSale += Time.deltaTime;
            if(currentTimeToSale > timeToSale)
            {
                currentTimeToSale = 0;
                SendCoin(itemsList[itemsList.Count - 1].GetPrice());
                itemsList.Remove(itemsList[itemsList.Count - 1]);
            }
        }

        if(playerIn)
        {
            CurrentTimeToTake += Time.deltaTime;
            if (CurrentTimeToTake > timeToGetItemFromBasket)
            {
                if(basket.CanGetItem())
                {
                    basket.GetItem().GetComponent<HarvestedPlants>().MoveTo(this);
                }
                CurrentTimeToTake = 0;
            }
        }
    }

    public void AddToSale(Item plant)
    {
        itemsList.Add(plant);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            basket = other.GetComponentInChildren<Basket>();
            playerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
            CurrentTimeToTake = 0;
        }

    }

    private void SendCoin(int money)
    {
        GameObject coint = Instantiate(prefabCoint, pointSpavnCoint.position, pointSpavnCoint.rotation);
        coint.GetComponent<CointToUI>().MoveTo(moneyUI.GetImageTransf(), money);
    }
}
