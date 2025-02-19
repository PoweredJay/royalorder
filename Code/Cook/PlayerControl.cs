using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isHoldingSomething;
    public float InteractDistance;
    public GameObject heldObj;
    Item heldObjItem;
    IEnumerable<GameObject> holdableObjects;
    IEnumerable<GameObject> applianceList;
    GameObject closestAppliance;
    ApplianceInteract closestApplianceScript;
    GameObject closestObj;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        applianceList = GameObject.FindGameObjectsWithTag("Appliance");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            DoRelevantInteraction();
        }
    }
    public void DoRelevantInteraction()
    {
        if(isHoldingSomething)
            {
                if(ApplianceNear(true))
                {
                    BeginTask();
                } else
                {
                    HoldObject();
                }
            } else
            {
                if(ApplianceNear(false))
                {
                    if(!isHoldingSomething)
                    {
                        EndTask();
                    } else
                    {
                        HoldObject();
                    } 
                } else
                {
                    HoldObject();
                }
            }
    }
    public void BeginTask()
    {
        closestApplianceScript.SetItemOnAppliance(heldObj,heldObjItem);        
        heldObjItem.isHeld = false;
        heldObj = null;
        heldObjItem = null;
        isHoldingSomething = false;
    }
    public void EndTask()
    {
        heldObj = closestApplianceScript.RemoveItemOnAppliance();
        heldObj.transform.SetParent(player.transform,true);
        heldObjItem = heldObj.GetComponent<Item>();
        heldObjItem.isHeld = true;
        isHoldingSomething = true;
        heldObj.transform.localPosition = new Vector3(0,1f,0);

    }

    public bool ApplianceNear(bool valid)
    {
        applianceList = applianceList.OrderBy(obj => (obj.transform.position - transform.position).sqrMagnitude);
        closestAppliance = applianceList.First();
        closestApplianceScript = closestAppliance.GetComponent<ApplianceInteract>();

        if (Vector2.Distance(closestAppliance.transform.position, player.transform.position) <= 1.15){
            if(valid)
            {
                if(closestAppliance.GetComponent<ApplianceInteract>().applianceTask == heldObjItem.taskToDo)
                {
                return true;
                }
            } else
            {
                return true;
            }
        }
        return false;
    }
    //Checks to see if there is a holdable object. 
    public void HoldObject()
    {
        if(isHoldingSomething)
        {
            heldObj.transform.SetParent(null);
            heldObjItem.isHeld = false;
            heldObj = null;
            isHoldingSomething = false;
            return;
        }

        holdableObjects = GameObject.FindGameObjectsWithTag("Held");
        holdableObjects = holdableObjects.OrderBy(obj => (obj.transform.position - transform.position).sqrMagnitude);
        closestObj = holdableObjects.First();
        heldObjItem = closestObj.GetComponent<Item>();

        if (Vector2.Distance(closestObj.transform.position, player.transform.position) <= 1.15)
        {
            if(heldObjItem.canBeInteracted()) 
            {
            closestObj.transform.SetParent(player.transform,true);
            heldObj = closestObj;
            heldObjItem.isHeld = true;
            isHoldingSomething = true;
            heldObj.transform.localPosition = new Vector3(0,1f,0);
            }
        }
    }
}
