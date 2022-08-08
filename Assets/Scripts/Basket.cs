using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private int maxSizeBasket = 40;
    [SerializeField] private BasketUI basketUI;
    [SerializeField] private GameObject startPointFill;
    [SerializeField] private Vector3 steps;
    [SerializeField] private Vector3 length;

    private List<GameObject> itemStac = new List<GameObject>();
    private int VisualSazeBasket = 0;

    private void Start()
    {
        UpDateCanvas();
    }

    public void PutItem(GameObject item)
    {
        if( itemStac.Count < maxSizeBasket )
        {
            itemStac.Add(item);
        }
        UpDateCanvas();
    }

    public bool CanPutItem()
    {
        if (VisualSazeBasket < maxSizeBasket)
            return true;
        else return false;
    }

    public bool CanGetItem()
    {
        if (itemStac.Count > 0)
            return true;
        else return false;
    }
    public GameObject GetItem()
    {
        GameObject last = itemStac[itemStac.Count-1];
        itemStac.Remove(last);
        --VisualSazeBasket;
        UpDateCanvas();
        return last;
    }

    public void InPlaced2()
    {
        ++VisualSazeBasket;
        UpDateCanvas();
    }

    public void UpDateCanvas()
    {
        basketUI.UpDateCanvas(itemStac.Count , maxSizeBasket);
    }

    public Vector3 GetPoinDeltaInBasket(int index)
    {
        int i = 0;
        for(int y = 0;  y < length.y; y++)
        {
            for (int x = 0; x < length.x; x++)
            {
                for (int z = 0; z < length.z; z++)
                {
                    ++i;
                    if(i == index)
                    {
                        return new Vector3(x* steps.x, y* steps.y, z* steps.z);
                    }
                }
            }
        }
        return Vector3.zero;
    }

    public GameObject GetPoinStartInBasket()
    {
        return startPointFill;
    }

    public int currentIndex()
    {
        ++VisualSazeBasket;
        return VisualSazeBasket;
    }
}
