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
    public float timer;
    public float curTime;
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
        
    }
    public void AddTime(float time)
    {
        curTime += time;
    }

    public bool canBeInteracted()
    {
        if(isHeld)
        {
            return false;
        }
        return true;
    }
}
