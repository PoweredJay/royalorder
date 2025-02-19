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
    public Material outlineMaterial;
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
    public Color ColorConstruct(int red, int green, int blue, int alpha)
    {
        float re = (red/255f);
        float gr = (green/255f);
        float bl = (blue/255f);
        float al = (alpha/255f);
        return (new Color(re, gr, bl, al));
    }
    public Color ColorConstruct(int red, int green, int blue)
    {
        float re = (red/255f);
        float gr = (green/255f);
        float bl = (blue/255f);
        return (new Color(re, gr, bl, 1f));
    }

    public void SetComplete()
    {
        taskComplete = true;
        outlineMaterial.SetColor("_OutlineColor", ColorConstruct(175,255,32));
        sprRend.color = ColorConstruct(255, 247, 97);
        sprRend.material = outlineMaterial;
    }
    public void SetOverdone()
    {
        if(!taskComplete)
        {
            SetComplete();
        }
        if(!overdone)
        {
            gold = gold/2;
            repGain = repGain/2;
            overdone = true;
            outlineMaterial.SetColor("_OutlineColor", ColorConstruct(255, 148, 61));
            sprRend.color = ColorConstruct(176, 176, 176);
            sprRend.material = outlineMaterial;
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
            outlineMaterial.SetColor("_OutlineColor", ColorConstruct(255, 0, 0));
            sprRend.color = ColorConstruct(99,99,99);
            sprRend.material = outlineMaterial;
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
