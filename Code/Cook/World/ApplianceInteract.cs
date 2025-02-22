using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceInteract : MonoBehaviour
{
    public GameObject self;
    public GameObject itemOnAppliance;
    SpriteRenderer sprRend;
    Animator sprAnim;
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    Item itemScript;
    public GameObject player;
    public bool doingTask;
    public TaskType applianceTask;
    public float speed;
    public float errorRate;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        sprAnim = GetComponent<Animator>();
        sprRend.sprite = inactiveSprite;
        sprAnim.enabled = false;
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
        itemOnAppliance.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        ToggleTask();
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
        itemOnAppliance.transform.localScale = new Vector3(1f,1f,1f);
        GameObject itemToReturn = itemOnAppliance;
        itemOnAppliance = null;
        itemScript = null;
        ToggleTask();
        return itemToReturn;
    }
    public void ToggleTask()
    {
        doingTask = !doingTask;
        if(doingTask)
        {
            sprAnim.enabled = true;
            sprRend.sprite = activeSprite;
        } else
        {
            sprAnim.enabled = false;
            sprRend.sprite = inactiveSprite;
        }
    }
}
