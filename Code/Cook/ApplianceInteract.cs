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
