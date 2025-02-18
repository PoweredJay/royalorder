using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType {
        NONE,
        SIMPLE,
        BEVERAGE,
        ROYAL,
        LEAFY,
        HEARTY
    }
    public enum TaskType {
        NONE,
        PAN,
        BOARD,
        OVEN,
        BOWL,
        POT,
        KEG
    }
public class Item : MonoBehaviour
{
    //Behind the scenes stuff
    GameObject obj;
    GameObject player;
    PlayerControl playControl;
    SpriteRenderer sprRend;
    Sprite itemSprite;

    [Header("Relevant Info")]
    public bool isHeld;
    public string itemName;
    public int gold;
    public int timer;
    public int curTime;
    public bool taskComplete;
    public ItemType itemType;
    public TaskType taskToDo;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        itemSprite = sprRend.sprite;
        obj = this.gameObject;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playControl = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (Vector2.Distance(obj.transform.position, player.transform.position) <= 1.15)
            {
                Debug.Log(obj.tag);
                if(playControl.isHoldingSomething){
                    return;
                }else if (obj.tag.Equals("Held"))
                {
                    obj.transform.SetParent(player.transform,true);
                    playControl.heldObj = obj;
                    isHeld = true;
                    playControl.isHoldingSomething = true;
                } else
                {
                    if(playControl.isHoldingSomething){
                    obj.transform.SetParent(null);
                    isHeld = false;
                    playControl.isHoldingSomething = false;
                }
                }
                // check tag of obj for interactions, make this once items can be held and shit
            } else
            {
                if(playControl.isHoldingSomething){
                obj.transform.SetParent(null);
                isHeld = false;
                playControl.isHoldingSomething = false;
            }
        }
            if(isHeld)
            {
                obj.transform.localPosition = new Vector3(0,(float)(1 + 0.8*obj.transform.GetSiblingIndex()),0);
            }
        }
    }

    public bool canBeInteracted(GameObject interactor)
    {
        if(isHeld)
        {
            return false;
        }
        return true;
    }
}
