using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour
{

    [SerializeField] private float timeAnimCollection = 2.5f;
    [SerializeField] private float DelayBeforeAttack = 0.5f;
    [SerializeField] private float damage = 50;
    [SerializeField] private float speedAtack = 1;

    [SerializeField] private GameObject tool;
    
    private HealthPointsHey target;
    private PlayerAI playerAI;
    private bool isStart = false;
    private float currentTime = 0;
    private bool canAtack = true;
    private bool toEnd = false;

    void Start()
    {
        timeAnimCollection /= speedAtack;
        DelayBeforeAttack /= speedAtack;
        playerAI = GetComponent<PlayerAI>();
        playerAI.SetSpeedAtack(speedAtack);
        tool.SetActive(false);
    }

    void Update()//надо было делать через состояния enum
    {
        if (isStart)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= DelayBeforeAttack)
            {
                if (canAtack)
                {
                    target.TakeDamage(damage);
                    if (!target.CanCollect())
                    {
                        toEnd = true;
                        
                    }
                    canAtack = false;
                }
                if (currentTime >= timeAnimCollection)
                {
                    currentTime = 0;
                    canAtack = true;
                    if(toEnd)
                    {
                        playerAI.AllCollection();
                        tool.SetActive(false);
                        isStart = false;
                    }
                }
            }
        }
    }

    public void StartCollection(GameObject _target)
    {
        target = _target.GetComponent<HealthPointsHey>();
        isStart = true;
        tool.SetActive(true);
        canAtack = true;
        toEnd = false;
    }

    public void Break()
    {
        currentTime = 0;
        isStart = false;
        tool.SetActive(false);
    }
}
