using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "item/plant")]
public class Item : ScriptableObject
{
    [SerializeField] private string name;
    [SerializeField] private int price;

    public string GetName() { return name; }
    public int GetPrice() { return price; }
}
