using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceInteract : MonoBehaviour
{
    public GameObject obj;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (Vector2.Distance(obj.transform.position, player.transform.position) <= 1.15)
            {
                Debug.Log(obj.tag);
                if (obj.tag.Equals("Held"))
                {
                    obj.transform.SetParent(player.transform,true);
                }
                // check tag of obj for interactions, make this once items can be held and shit
            }
        }
        
    }
}
