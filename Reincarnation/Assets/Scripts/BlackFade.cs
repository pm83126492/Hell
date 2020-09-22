using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFade : MonoBehaviour
{
    public bool CanChangeScene;
    public void ChangeScene()
    {
        CanChangeScene = true;
    }
}
