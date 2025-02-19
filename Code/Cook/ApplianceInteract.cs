using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceInteract : MonoBehaviour
{
    public GameObject self;
    public GameObject itemOnAppliance;
    Item itemScript;
    public GameObject player;
    public bool doingTask;
    public TaskType applianceTask;
    public float speed;
    public float errorRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(doingTask)
        {
            itemScript.AddTime(Time.deltaTime);
            if((itemScript.curTime >= itemScript.timer) && (!itemScript.taskComplete))
            {
                ErrorChance();
            }
            if((itemScript.curTime >= 2*itemScript.timer) && (!itemScript.overdone))
            {
                itemScript.SetOverdone();
            }
            if((itemScript.curTime >= 3*itemScript.timer) && (!itemScript.trash))
            {
                itemScript.SetTrash();
            }
        }
    }
    public void SetItemOnAppliance(GameObject ItemObject, Item item)
    {
        itemOnAppliance = ItemObject;
        itemScript = item;
        itemOnAppliance.transform.SetParent(this.gameObject.transform);
        itemOnAppliance.transform.localPosition = new Vector3(0,0,0);
        doingTask = true;
    }
    public void ErrorChance()
    {
        if(itemScript == null)
        {
            return;
        }
        if(UnityEngine.Random.Range(0f,1f) <= errorRate)
        {
            itemScript.SetOverdone();
            Debug.Log("Oops, no jackpot.");
        } else
        {
            itemScript.SetComplete();
        }
    }
    public GameObject RemoveItemOnAppliance()
    {
        GameObject itemToReturn = itemOnAppliance;
        itemOnAppliance = null;
        itemScript = null;
        doingTask = false;
        Debug.Log("Task off");
        return itemToReturn;
    }
    public void ToggleTask()
    {
        doingTask = !doingTask;

    }
}
