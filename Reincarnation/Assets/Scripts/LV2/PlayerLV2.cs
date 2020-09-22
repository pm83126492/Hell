using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLV2 : Player
{

    public GameObject organIce;
    public GameObject obstacle;
    public GameObject OrganCircle;

  

    protected override void MobileTouch()
    {
        base.MobileTouch();

        //第一隻手指
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);


            //第一隻手指放掉瞬間
            if (touch.phase == TouchPhase.Ended)
            {
                if (organIce != null)
                {
                    organIce.GetComponent<Rigidbody2D>().isKinematic = false;
                }
            }
            if (touch.phase == TouchPhase.Stationary && OneTouchX == OneTouchX2)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "organ" && isObstacle)
                {
                    Organ();
                }
            }

        }

        //第二隻手指     
        if (Input.touchCount > 1)
        {
            isTouch2 = true;
            Touch touch2 = Input.GetTouch(1);
            //第二隻手指放掉瞬間
            if (touch2.phase == TouchPhase.Ended)
            {
                if (obstacle != null)
                {
                    anim.SetBool("Push", false);
                    obstacle.GetComponent<Rigidbody2D>().gravityScale = 10;
                    obstacle.GetComponent<FixedJoint2D>().enabled = false;
                    obstacle = null;
                }
            }

            if (touch2.phase == TouchPhase.Stationary)
            {
                if (isObstacle)
                {
                    if (hit2.collider != null && hit2.collider.gameObject.tag == "obstacle")
                    {
                        anim.SetBool("Push", true);
                        Obstacle();
                    }
                }
            }

        }

        //判斷起重機是否放掉
        if (!isObstacle || OneTouchX != OneTouchX2 || TwoTouchX != TwoTouchX2)
        {
            organIce.GetComponent<Rigidbody2D>().isKinematic = false;
        }


        if (Input.GetKey(KeyCode.E))
        {
            if (hit2.collider != null && hit2.collider.gameObject.tag == "organ" && isObstacle)
            {
                Organ();
            }
            if (isObstacle)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "obstacle")
                {
                    anim.SetBool("Push", true);
                    Obstacle();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (organIce != null)
            {
                organIce.GetComponent<Rigidbody2D>().isKinematic = false;
            }
            if (obstacle != null)
            {
                anim.SetBool("Push", false);
                obstacle.GetComponent<Rigidbody2D>().gravityScale = 10;
                obstacle.GetComponent<FixedJoint2D>().enabled = false;
                obstacle = null;
            }
        }
    }

    //冰障礙物事件
    void Obstacle()
    {
        obstacle = hit2.collider.gameObject;
        obstacle.GetComponent<Rigidbody2D>().gravityScale = 3;
        obstacle.GetComponent<FixedJoint2D>().enabled = true;
        obstacle.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
    }
    //起重冰事件
    void Organ()
    {
        OrganCircle.transform.Rotate(0, 0, 30 * Time.deltaTime);
        organIce.GetComponent<Rigidbody2D>().isKinematic = true;
        organIce.GetComponent<Rigidbody2D>().velocity = Vector2.up * 1f;
        //organIce = hit2.collider.gameObject;
        //organIce.transform.Translate(0, 0.01f, 0);
        //rigidbody2D.velocity = Vector2.up * JumpForce;
    }
}
