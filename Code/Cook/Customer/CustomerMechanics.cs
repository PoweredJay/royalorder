using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CustomerTask 
{
        NONE,
        SLEEP,
        STEAL,
        RUCKUS,
        COMPLAIN,
        EXTRA
}
public class CustomerMechanics : MonoBehaviour
{
    public string customerName;
    public ItemType typeWanted1;
    public ItemType typeWanted2;
    public CustomerTask task1;
    public CustomerTask task2;
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
    FoodSelect foodSelect;
    // Start is called before the first frame update
    void Start()
    {
        pathScript = GetComponent<CustomerPathfind>();
        cookingSystem = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<CookSystem>();
        repGain = maxRepGain;
        foodSelect = GetComponent<FoodSelect>();
        List<GameObject> choices = new List<GameObject>();
        List<GameObject> choices1 = foodSelect.Select(typeWanted1);
        List<GameObject> choices2 = foodSelect.Select(typeWanted2);
        for (int i = 0; i < choices1.Count; i++)
        {
            choices.Add(choices1[i]);
        }
        for (int i = 0; i < choices2.Count; i++)
        {
            choices.Add(choices2[i]);
        }
        ItemWanted = choices[(int) UnityEngine.Random.Range(0f, choices.Count)].GetComponent<Item>();
        Instantiate(new GameObject(), this.transform.GetChild(0).transform);
        var sprRender = this.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<SpriteRenderer>();
        var sprite = ItemWanted.cookedSprite;
        sprRender.sprite = sprite;
        this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        Destroy(GameObject.Find("New Game Object"));
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
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
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
            int taskChoose = UnityEngine.Random.Range(0, 2);
            if (taskChoose == 0)
            {
                // customeer task (task1)
            }
            else if (taskChoose == 1)
            {
                // customeer task (task2)
            }
        } else
        {
            Debug.Log("Not correct item!");
        }
    }
    void Task(CustomerTask task)
    {
        if (task == CustomerTask.SLEEP)
        {

        }
        else if (task == CustomerTask.STEAL)
        {
            
        }
        else if (task == CustomerTask.RUCKUS)
        {
            
        }
        else if (task == CustomerTask.COMPLAIN)
        {
            
        }
        else if (task == CustomerTask.EXTRA)
        {
            
        }
    }
}
