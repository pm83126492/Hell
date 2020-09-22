using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Test : MonoBehaviour
{
    public Transform target;
    public CinemachineVirtualCamera cinemachine;

    // Update is called once per frame
    void Update()
    {
        cinemachine.Follow = null;
        //Here i assumed that you want to change the X 
        Vector2 newCamPosition = new Vector2(target.position.x, 4.3f);
        gameObject.transform.position = newCamPosition;
    }
}
