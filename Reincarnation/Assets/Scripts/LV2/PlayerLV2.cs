using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLV2 : Player
{

    public GameObject organIce;
    public GameObject obstacle;
    public GameObject OrganCircle;

    public Transform OrganPosition;

    bool isTouchOrgan;
    public bool CanChangeScene;

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
                if (obstacle != null)
                {
                    anim.SetBool("Push", false);
                    anim.SetBool("SquatPush", false);
                    obstacle.GetComponent<Rigidbody2D>().gravityScale = 10;
                    obstacle.GetComponent<FixedJoint2D>().enabled = false;
                    obstacle = null;
                }
                if (organIce != null)
                {
                    anim.enabled = true;
                    anim.SetBool("Roll", false);
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
                    anim.SetBool("SquatPush", false);
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
                    else if (hit2.collider != null && hit2.collider.gameObject.tag == "smallobstacle")
                    {
                        anim.SetBool("SquatPush", true);
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
                isTouchOrgan = true;
            }
            if (isObstacle)
            {
                if (hit2.collider != null && hit2.collider.gameObject.tag == "obstacle")
                {
                    anim.SetBool("Push", true);
                    Obstacle();
                }
                else if (hit2.collider != null && hit2.collider.gameObject.tag == "smallobstacle")
                {
                    anim.SetBool("SquatPush", true);
                    Obstacle();
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (organIce != null)
            {
                anim.enabled = true;
                anim.SetBool("Roll", false);
                organIce.GetComponent<Rigidbody2D>().isKinematic = false;
                isTouchOrgan = false;
            }
            if (obstacle != null)
            {
                anim.SetBool("Push", false);
                anim.SetBool("SquatPush", false);
                obstacle.GetComponent<Rigidbody2D>().gravityScale = 10;
                obstacle.GetComponent<FixedJoint2D>().enabled = false;
                obstacle = null;
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (isTouchOrgan)
        {
            Organ();
        }

        if ((OneTouchX2 > OneTouchX + 200) || (TwoTouchX2 > TwoTouchX + 200) || Input.GetKey(KeyCode.D) || OneTouchX2 + 200 < OneTouchX || TwoTouchX2 + 200 < TwoTouchX || Input.GetKey(KeyCode.A))
        {
            anim.enabled = true;
            anim.SetBool("Roll", false);
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
        gameObject.transform.position = OrganPosition.position;
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (organIce.transform.position.y < 12)
        {
            OrganCircle.transform.Rotate(0, 0, 100 * Time.deltaTime);
        }
        organIce.GetComponent<Rigidbody2D>().isKinematic = true;
        organIce.GetComponent<Rigidbody2D>().velocity = Vector2.up * 1f;
        if (organIce.transform.position.y >= 12)
        {
            organIce.transform.position = new Vector3(organIce.transform.position.x, 12, organIce.transform.position.z);
            anim.enabled = false;
        }
        anim.SetBool("Roll", true);
        //organIce = hit2.collider.gameObject;
        //organIce.transform.Translate(0, 0.01f, 0);
        //rigidbody2D.velocity = Vector2.up * JumpForce;
    }

    public void ReloadScene()
    {
        CanChangeScene = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DieObjects"))
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.08030701f, -0.04518163f);
            GetComponent<BoxCollider2D>().size = new Vector2(1.270004f, 0.08324409f);
            GetComponent<PlayerLV2>().enabled = false;
            anim.SetTrigger("IceSmokeDie");
        }
    }
}
