using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLV3 : Player
{
    public GameObject obstacle;
    public Transform point,point2;
    public DistanceJoint2D distanceJoint;
    public DistanceJoint2D hingeJoint;
    public DistanceJoint2D hingeJoint2;
    public DistanceJoint2D hingeJoint3;
    public bool isSwing,isSwingJimp;

    protected override void Movement()
    {
        if (!isSwing && !isSwingJimp)
        {
            base.Movement();
        }  
    }

    protected override void Update()
    {
        base.Update();

        if (isSwing)
        {
            if (isObstacle)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "Swing")
                {
                    distanceJoint.connectedAnchor = new Vector2(point.position.x, point.position.y);
                    Obstacle();
                }
                else if (hit2.collider != null && hit2.collider.gameObject.tag == "Swing2")
                {
                    distanceJoint.connectedAnchor = new Vector2(point2.position.x, point2.position.y);
                    Obstacle();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isObstacle)
            {
                if (hit2.collider != null && (hit2.collider.gameObject.tag == "Swing" || hit2.collider.gameObject.tag == "Swing2"))
                {
                    isSwing = true;
                    if (rigidbody2D.velocity.x > 0)
                    {
                        rigidbody2D.velocity = new Vector2(300 * Time.deltaTime, rigidbody2D.velocity.y);
                    }
                    else if (rigidbody2D.velocity.x < 0)
                    {
                        rigidbody2D.velocity = new Vector2(-300 * Time.deltaTime, rigidbody2D.velocity.y);
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
            if (obstacle != null)
            {
                isSwing = false;
                isSwingJimp = true;
                obstacle.transform.parent = null;
                obstacle.GetComponent<Rigidbody2D>().isKinematic = false;
                distanceJoint.enabled = false;
                obstacle = null;
            }
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = new Vector2(300 * Time.deltaTime, rigidbody2D.velocity.y);
            }
            else if (rigidbody2D.velocity.x < 0)
            {
                rigidbody2D.velocity = new Vector2(-300 * Time.deltaTime, rigidbody2D.velocity.y);
            }
        }

        if (isGround)
        {
            GetComponent<Rigidbody2D>().gravityScale = 3;
            isSwingJimp = false;
        }

        hingeJoint.distance = 4.5f;
        hingeJoint2.distance = 4.5f;
    }

    void Obstacle()
    {
        obstacle = hit2.collider.gameObject;
        obstacle.GetComponent<Rigidbody2D>().isKinematic = true;
        obstacle.transform.parent = gameObject.transform;
        distanceJoint.enabled = true;      
    }
}
