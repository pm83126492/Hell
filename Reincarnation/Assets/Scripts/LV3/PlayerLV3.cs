using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLV3 : Player
{
    public GameObject obstacle;

    protected override void Movement()
    {
        base.Movement();

        if (Input.GetKey(KeyCode.E))
        {
            if (isObstacle)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "obstacle")
                {
                    Obstacle();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (obstacle != null)
            {
                transform.parent = null;
                GetComponent<Rigidbody2D>().isKinematic = false;
                obstacle = null;
            }
        }
    }

    void Obstacle()
    {
        obstacle = hit2.collider.gameObject;
        transform.parent = obstacle.transform;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<FixedJoint2D>().enabled = true;
        //obstacle.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
    }
}
