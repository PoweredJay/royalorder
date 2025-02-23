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
    IEnumerable<GameObject> customerList;
    GameObject closestCustomer;
    CustomerMechanics closestCustomerScript;
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
                if(CustomerNear())
                {
                    GiveItem();
                }
                else if(ApplianceNear(true))
                {
                    BeginTask();
                } else
                {
                    HoldObject();
                }
            } else
            {
                if(CustomerNear())
                {
                    QueryCustomer();
                }
                else if(ApplianceNear(false))
                {
                    if(closestApplianceScript.itemOnAppliance != null)
                    {
                        EndTask();
                    } else
                    {
                        return;
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
        if(!applianceList.Any())
        {
            return false;
        }
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

    public bool CustomerNear()
    {
        customerList = GameObject.FindGameObjectsWithTag("Customer");
        if(!customerList.Any())
        {
            return false;
        }
        customerList = customerList.OrderBy(obj => (obj.transform.position - transform.position).sqrMagnitude);
        closestCustomer = customerList.First();
        closestCustomerScript = closestCustomer.GetComponent<CustomerMechanics>();

        if (Vector2.Distance(closestCustomer.transform.position, player.transform.position) <= 1.15){
            return true;
        }
        return false;
    }
    public void QueryCustomer()
    {
        Debug.Log(closestCustomerScript.customerName + " wants " + closestCustomerScript.ItemWanted.itemName + ". They will stay for " + closestCustomerScript.maxTime + " seconds.");
        // if customer is in the process of stealing
        // they stop and leave
    }
    public void GiveItem()
    {
        closestCustomerScript.ReceiveItem(heldObj,heldObjItem);        
        heldObjItem.isHeld = false;
        heldObj = null;
        heldObjItem = null;
        isHoldingSomething = false;
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
