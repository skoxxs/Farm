using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPointsHey : MonoBehaviour
{
    [SerializeField] private float maxHealthPoint = 100;
    [SerializeField] private float currentHealthPoint = 100;

    private bool canCollection = false;
    private bool CollectionFirstPart = false;
    private GardenBed gardenBed;

    private void Start()
    {
        gardenBed = GetComponent<GardenBed>();
    }

    public void HeyGrow()
    {
        canCollection = true;
        CollectionFirstPart = true;
        currentHealthPoint = maxHealthPoint;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;
        currentHealthPoint -= damage;

        if (currentHealthPoint <= maxHealthPoint/2 && CollectionFirstPart)
        {
            CollectionFirstPart = false;
            gardenBed.PartPlantÑollect();
        }

        if (currentHealthPoint <= 0)
        {
            canCollection = false;
            gardenBed.PlantÑollect();
        }

    }

    public bool CanCollect()
    {
        return canCollection;
    }
}
