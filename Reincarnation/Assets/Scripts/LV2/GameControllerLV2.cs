using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameControllerLV2 : MonoBehaviour
{
    public GameObject IceOrgan, IceButtonBox, IcePlate,Player;
    public ParticleSystem SmokeIce01, SmokeIce02, SmokeIce03;
    public Transform CanButtonPoint, CanButtonPoint02,IcePlateStop;
    public CinemachineVirtualCamera virtualCamera;
    public Animator BlackAnim;
    bool isLose;
    public SmokeParticle smokeParticle;

    public BlackFade blackFade;

    // Start is called before the first frame update
    void Start()
    {
        isLose = false;
        StartCoroutine(PlaySmokeIce01());
        StartCoroutine(PlaySmokeIce02());
        StartCoroutine(PlaySmokeIce03());
    }

    // Update is called once per frame
    void Update()
    {
        LastIceMove();
        Lose();
        
    }

    public void Organ()
    {

    }

    IEnumerator PlaySmokeIce01()
    {
        yield return new WaitForSeconds(2f);
        SmokeIce01.Play();
        yield return new WaitForSeconds(3f);
        StartCoroutine(PlaySmokeIce01());
    }
    IEnumerator PlaySmokeIce02()
    {
        yield return new WaitForSeconds(1f);
        SmokeIce02.Play();
        yield return new WaitForSeconds(4f);
        StartCoroutine(PlaySmokeIce02());
    }
    IEnumerator PlaySmokeIce03()
    {
        yield return new WaitForSeconds(2f);
        SmokeIce03.Play();
        yield return new WaitForSeconds(3f);
        StartCoroutine(PlaySmokeIce03());
    }

    void Lose()
    {
        if (Player.transform.position.y <= -4)
        {
            virtualCamera.Follow = null;
        }

        if (Player.transform.position.y <= -150)
        {
            BlackAnim.SetTrigger("FadeOut");
            isLose = true;
        }

        if (blackFade.CanChangeScene && isLose)
        {
            SceneManager.LoadScene("LV2");
        }
    }

    void LastIceMove()
    {
        if (IceOrgan.transform.position.y >= 12)
        {
            IceOrgan.transform.position = new Vector3(IceOrgan.transform.position.x, 12, IceOrgan.transform.position.z);
        }

        if (IceButtonBox.transform.position.x >= CanButtonPoint.position.x && IceButtonBox.transform.position.x <= CanButtonPoint02.position.x)
        {
            if (IcePlate.transform.position.x > IcePlateStop.position.x)
            {
                IcePlate.transform.Translate(-1 * Time.deltaTime, 0, 0);
            }
        }
    }
}
