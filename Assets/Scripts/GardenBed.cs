using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBed : MonoBehaviour
{
    [SerializeField] private GameObject prefabHey;
    [SerializeField] private Transform pointSpavnHey;
    [SerializeField] private float timeToGrown = 10.0f;


    [SerializeField] private GameObject growingHey;
    [SerializeField] private GameObject partAssembledHey;
    [SerializeField] private GameObject grown;

    private float currentTime = 0;
    private HealthPointsHey healthPointsHey;
    private GardenBedState gardenBedState;
    enum GardenBedState
    {
        Nothing,
        Growing,
        HasGrown,
        PartAssembled
    }

    void Start()
    {
        healthPointsHey = GetComponent<HealthPointsHey>();
        gardenBedState = GardenBedState.Nothing;
    }

    void Update()
    {
        if(gardenBedState == GardenBedState.Nothing)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToGrown/2)
            {
                growingHey.SetActive(true);
                gardenBedState = GardenBedState.Growing;
            }
        }
        if (gardenBedState == GardenBedState.Growing)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeToGrown)
            {
                growingHey.SetActive(false);
                grown.SetActive(true);
                gardenBedState = GardenBedState.HasGrown;
                healthPointsHey.HeyGrow();
            }
        }
    }

    private void GiveHayBale()
    {
        Instantiate(prefabHey, pointSpavnHey.position, pointSpavnHey.rotation);
    }

    public void Plant—ollect()
    {
        if (gardenBedState == GardenBedState.PartAssembled)
        {
            gardenBedState = GardenBedState.Nothing;
            currentTime = 0;
            partAssembledHey.SetActive(false);
            GiveHayBale();
        }
    }

    public void PartPlant—ollect()
    {
        if (gardenBedState == GardenBedState.HasGrown)
        {
            gardenBedState = GardenBedState.PartAssembled;
            grown.SetActive(false);
            partAssembledHey.SetActive(true);
            GiveHayBale();
        }
    }
}
