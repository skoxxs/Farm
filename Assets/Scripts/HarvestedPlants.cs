using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestedPlants : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private float rangeToIn = 0.2f;
    [SerializeField] private float moveSpeed = 100;

    private GameObject target;
    private Vector3 deltaTarget;
    private Basket basket;
    private bool startMove = false;
    private bool toBasket = false;
    private bool toGranary = false;

    private void Update()
    {
        if(startMove)
        {
            if (Vector3.Distance(this.transform.position, target.transform.position + deltaTarget) > rangeToIn)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position + deltaTarget, moveSpeed * Time.deltaTime);
            }
            else
            {
                if(toBasket) PutInBasket();
                if (toGranary) PutImGranary();

                startMove = false;
                toBasket = false;
                toGranary = false;
            }
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void MoveTo(Basket _basket)
    {
        toBasket = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().enabled = false;
        basket = _basket;
        target = basket.GetPoinStartInBasket();
        deltaTarget = basket.GetPoinDeltaInBasket(basket.currentIndex());
        startMove = true; 
    }

    public void MoveTo(Granary granary)
    {
        transform.SetParent(transform.parent, false);
        toGranary = true;
        target = granary.gameObject;
        deltaTarget = Vector3.zero;
        startMove = true;
    }
    public bool IsTaken()
    {
        return startMove;
    }

    private void PutInBasket()
    {
        basket.PutItem(this.gameObject); 
        Vector3 vec = this.transform.localScale;
        this.transform.SetParent(basket.transform);
        this.transform.localPosition = target.transform.localPosition + deltaTarget;
        this.transform.localRotation = new Quaternion();
        this.transform.localScale = vec;
    }

    private void PutImGranary()
    {
        target.GetComponent<Granary>().AddToSale(item);
        Destroy(this.gameObject);
    }
}
