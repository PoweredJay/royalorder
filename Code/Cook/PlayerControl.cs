using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isHoldingSomething;
    public float InteractDistance;
    public GameObject heldObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("return") || Input.GetKeyDown("z"))
        {
            if(HasCloseObject()){
                // Debug.Log(GetClosestObject().name);
            } else {
                // Debug.Log("Nothing within " + InteractDistance + " units.");
            }

        }
    }
    public bool HasCloseObject()
    {
       float closest = InteractDistance;
        GameObject closestObject = null;
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < interactables.Length; i++)  //list of gameObjects to search through
        {
            float dist = Vector2.Distance(interactables[ i ].transform.position, this.transform.position);
            if (dist < closest)
            {
            closest = dist;
            closestObject = interactables[ i ];
            }
        }
        if(closestObject != null)
        {
            return false;
        } return true;
    }
    
    public GameObject GetClosestObject()
    {
        float closest = InteractDistance;
        GameObject closestObject = null;
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
        for (int i = 0; i < interactables.Length; i++)  //list of gameObjects to search through
        {
            float dist = Vector2.Distance(interactables[ i ].transform.position, this.transform.position);
            if (dist < closest)
            {
            closest = dist;
            closestObject = interactables[ i ];
            }
        }
        return closestObject;
    }
}
