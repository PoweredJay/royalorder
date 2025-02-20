using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMechanics : MonoBehaviour
{
    public string customerName;
    public ItemType typeWanted;
    public Item ItemWanted;
    public GameObject objectHeld;
    Item itemHeld;
    CustomerPathfind pathScript;
    public float timeTaken;
    public float maxTime;
    public bool skye;
    public bool halfMark;
    public bool done;
    public float maxRepGain;
    public float repGain;
    CookSystem cookingSystem;
    // Start is called before the first frame update
    void Start()
    {
        pathScript = GetComponent<CustomerPathfind>();
        cookingSystem = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<CookSystem>();
        repGain = maxRepGain;
    }

    // Update is called once per frame
    void Update()
    {
        if(skye)
        {
            timeTaken += Time.deltaTime;
            TimeUpdate();
        }
    }

    public void StartClock()
    {
        skye = true;
        Debug.Log("Time's ticking!");
    }

    public void TimeUpdate()
    {
        if((timeTaken >= maxTime/2) && (!halfMark))
        {
            halfMark = true;
        }
        if(halfMark)
        {
            repGain -= (maxRepGain * 0.15f) * Time.deltaTime;
            repGain = Math.Max(repGain,0);
        }
        if((timeTaken >= maxTime) && (!done))
        {
            done = true;
            repGain = -5;
        }
    }
    public void SatisfyCustomer(Item item)
    {
        if(item.taskComplete)
        {
            if(item.overdone)
            {
                if(item.trash)
                {
                    repGain = -10;
                } else
                {
                    repGain = repGain * 0.5f;
                }
            }
        } else
        {
            repGain = 0;
        }
    }
    public void ReceiveItem(GameObject ItemObject, Item item)
    {
        if((item.itemName == ItemWanted.itemName) && (item.taskComplete))
        {
            objectHeld = ItemObject;
            itemHeld = item;
            objectHeld.transform.SetParent(this.gameObject.transform);
            objectHeld.transform.localPosition = new Vector3(0,0,0);
            skye = false;
            SatisfyCustomer(itemHeld);
            cookingSystem.ModifySystem((int)repGain,(itemHeld.CashOut()));
        } else
        {
            Debug.Log("Not correct item!");
        }
    }
}
