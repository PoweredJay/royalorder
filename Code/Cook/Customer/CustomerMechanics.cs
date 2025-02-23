using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CustomerState 
{
        NONE,
        EAT,
        SLEEP,
        STEAL,
        RUCKUS,
        COMPLAIN,
        EXTRA,
        LEAVE
}
public class CustomerMechanics : MonoBehaviour
{
    public string customerName;
    public ItemType typeWanted1;
    public ItemType typeWanted2;
    public CustomerState task1;
    public CustomerState task2;
    public CustomerState currentState;
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
    public Chair chair;
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
        Debug.Log("Time's ticking!"); // add effect to make the thingy show how much time you have left somehow
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
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            int taskChoose = -1;
            if (currentState != CustomerState.EXTRA && taskChoose == 0)
            {
                StartCoroutine(Task(task1));
            }
            else if (currentState != CustomerState.EXTRA && taskChoose == 1)
            {
                StartCoroutine(Task(task2));
            }
            else
            {
                StartCoroutine(Task(CustomerState.LEAVE));
            }
        } else
        {
            Debug.Log("Not correct item!");
        }
    }
    IEnumerator Task(CustomerState task)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 15f));
        if (task == CustomerState.SLEEP)
        {

        }
        else if (task == CustomerState.STEAL)
        {
            // pathScript.target = (location of food)
            // currentState = CustomerState.STEAL;
            // pathScript.arrived = false;
        }
        else if (task == CustomerState.RUCKUS)
        {
            
        }
        else if (task == CustomerState.COMPLAIN)
        {
            
        }
        else if (task == CustomerState.EXTRA)
        {
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
            var sprRender = this.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<SpriteRenderer>();
            var sprite = ItemWanted.cookedSprite;
            sprRender.sprite = sprite;
            StartClock();
        }
        else if (task == CustomerState.LEAVE)
        {
            pathScript.target = GameObject.Find("Exit").transform;
            pathScript.arrived = false;
            pathScript.rigidb.simulated = true;
            currentState = CustomerState.LEAVE;
        }
    }
}
