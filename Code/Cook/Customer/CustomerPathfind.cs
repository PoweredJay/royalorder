using Pathfinding;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomerPathfind : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    public Transform target;
    bool arrived;
    CustomerMechanics mech;
    Rigidbody2D rigidb;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<AIPath>();
        mech = GetComponent<CustomerMechanics>();
        rigidb = GetComponent<Rigidbody2D>();
        rigidb.simulated = true;
    }


    // Update is called once per frame
    void Update()
    {
        path.maxSpeed = moveSpeed;
        path.destination = target.position;
        if (!arrived && Vector2.Distance(this.GetComponent<Transform>().position, target.position) <= 0.015)
        {
            arrived = true;
            mech.StartClock();
            rigidb.simulated = false;
        }
    }
    // public void FindTarget()
    // {
    //     IEnumerable<GameObject> ChairList = GameObject.FindGameObjectsWithTag("Chair");
    //     ChairList.OrderBy(chair => chair.GetComponent<Chair>().PathFound);
    //     target = ChairList.First().transform;
    //     ChairList.First().GetComponent<Chair>().PathFound = true;
    // }
}
