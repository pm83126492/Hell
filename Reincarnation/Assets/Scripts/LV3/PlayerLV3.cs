using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLV3 : Player
{
    public GameObject obstacle;
    public Transform point;
    public DistanceJoint2D distanceJoint;
    public bool isSwing;

    protected override void Movement()
    {
        if (!isSwing)
        {
            base.Movement();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isObstacle)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "Swing")
                {
                    if (rigidbody2D.velocity.x > 0)
                    {
                        rigidbody2D.velocity = new Vector2(500 * Time.deltaTime, rigidbody2D.velocity.y);
                    }
                    else if(rigidbody2D.velocity.x < 0)
                    {
                        rigidbody2D.velocity = new Vector2(-500 * Time.deltaTime, rigidbody2D.velocity.y);
                    }
                   
                }
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (isObstacle)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "Swing")
                {
                    //rigidbody2D.velocity = new Vector2(runSpeed * Time.deltaTime, rigidbody2D.velocity.y);
                    isSwing = true;
                    Obstacle();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (obstacle != null)
            {
                distanceJoint.enabled = false;
                obstacle = null;
            }
        }

        if (isGround)
        {
            isSwing = false;
            GetComponent<Rigidbody2D>().gravityScale = 5;
        }
    }

    protected override void Update()
    {
        base.Update();
       
    }

    void Obstacle()
    {
        obstacle = hit2.collider.gameObject;
        GetComponent<Rigidbody2D>().gravityScale = 3;
        distanceJoint.enabled = true;
        distanceJoint.connectedAnchor = new Vector2(point.position.x, point.position.y);
        //obstacle.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
    }
}
