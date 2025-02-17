using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isHeld;
    SpriteRenderer sprRend;
    public Sprite itemSprite;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        itemSprite = sprRend.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
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
