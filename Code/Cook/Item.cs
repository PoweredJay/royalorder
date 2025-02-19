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
    Material outlineMaterial;
    [Header("Relevant Info")]
    public Sprite rawSprite;
    public Sprite cookedSprite;
    public bool isHeld;
    public string itemName;
    public int gold;
    public int repGain;
    public float timer;
    public float curTime;
    public bool taskComplete;
    public bool overdone;
    public bool trash;
    public ItemType itemType;
    public TaskType taskToDo;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        sprRend.sprite = rawSprite;
        outlineMaterial = sprRend.material;
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
    public void CashOut()
    {

    }

    public void SetComplete()
    {
        taskComplete = true;
        outlineMaterial.SetColor("_BaseColor",new Color(220,220,220,155));
    }
    public void SetOverdone()
    {
        if(!overdone)
        {
            gold = gold/2;
            repGain = repGain/2;
            overdone = true;
        }
    }
    public void SetTrash()
    {
        if(!trash)
        {
            gold = 0;
            repGain = -5;
            trash = true;
            overdone = true;
        }
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
