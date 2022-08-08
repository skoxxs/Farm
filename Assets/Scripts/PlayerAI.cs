using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    [SerializeField] private Basket basket;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float rangeScaneWorld = 1;
    [SerializeField] private LayerMask layerMask;
  

    private PlayerCollection playerCollection;
    private PlayerState playerState;

    enum PlayerState
    {
        Collection,
        Run,
        Stand
    }

    void Start()
    {
        playerCollection = GetComponent<PlayerCollection>();
        playerState = PlayerState.Stand;
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Collection:
                break;
            case PlayerState.Run:
                ScanWorldForItem();
                break;
            case PlayerState.Stand:
                ScanWorld();
                break;
        }
    }


    public void ScanWorldForItem()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, rangeScaneWorld, layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].gameObject.CompareTag("plant"))
            {
                if (basket.CanPutItem())
                {
                    HarvestedPlants harvestedPlants = coll[i].GetComponent<HarvestedPlants>();
                    if (!harvestedPlants.IsTaken())
                    {
                        harvestedPlants.MoveTo(basket);
                    }
                }
            }
        }
    }
    public void ScanWorld()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, rangeScaneWorld, layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].gameObject.CompareTag("garden_bed") && coll[i].GetComponent<HealthPointsHey>().CanCollect()) 
            {
                StartCollection(coll[i].gameObject);
                Quaternion q = Quaternion.LookRotation(coll[i].gameObject.transform.position - playerModel.transform.position); 
                q.x = 0;
                q.z = 0;
                playerModel.transform.rotation = q;
            }
            if(coll[i].gameObject.CompareTag("plant"))
            {
                if(basket.CanPutItem())
                {
                    HarvestedPlants harvestedPlants = coll[i].GetComponent<HarvestedPlants>();
                    if (!harvestedPlants.IsTaken())
                    {
                        harvestedPlants.MoveTo(basket);
                    }
                }
            }
        }
    }

    private void StartCollection(GameObject _target)
    {
        SetAnimation("Collection");
        playerState = PlayerState.Collection;
        playerCollection.StartCollection(_target);
    }

    public void AllCollection()
    {
        playerState = PlayerState.Stand;
        SetAnimation("Stand");
    }

    public void PlayerIsMove(bool move)
    {
        if(move)
        {
            playerState = PlayerState.Run;
            SetAnimation("Run");
        }
        else
        {
            if(playerState != PlayerState.Collection)
            {
                playerState = PlayerState.Stand;
                SetAnimation("Stand");
                playerCollection.Break();
            }
        }
    }

    public Basket GetBasket()
    {
        return basket;
    }

    private void SetAnimation(string animName)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Stand", false);
        animator.SetBool("Collection", false);
        animator.SetBool(animName, true);
    }

    public void SetSpeedAtack(float speedAtack)
    {
        animator.SetFloat("SpeedAtack",speedAtack);
    }
}
