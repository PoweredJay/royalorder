using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    float horizontal;
    float vertical;
    float moveSpeed = 14;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up")){
            vertical = 1;
        }
        else if (Input.GetKey("down")){
            vertical = -1;
        }
        else {
            vertical = 0;
        }
        if (Input.GetKey("left")){
            horizontal = -1;
        }
        else if (Input.GetKey("right")){
            horizontal = 1;
        }
        else{
            horizontal = 0;
        }
    }

    void FixedUpdate()
    {
        float unit = (float) Math.Sqrt(horizontal*horizontal + vertical*vertical);
        if (unit == 0)
            body.velocity = new Vector2(0,0);
        else
            body.velocity = new Vector2(moveSpeed*horizontal/unit, moveSpeed*vertical/unit);
    }
}
