using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakerManager : Singleton<MakerManager>
{
    GameObject Giga, Mega;

    GameObject BGMID;

    public bool isPlaying;

    void Awake()
    {
        Giga = GameObject.Find("GIGAPOWER");
        Mega = GameObject.Find("330MEGA");
        BGMID = GameObject.Find("BGMSELECTER");

        isPlaying = false;
    }

    void TemplateChange(int id)
    {
        switch (id)
        {
            case 0:
                Giga.SetActive(false);
                Mega.SetActive(true);
                break;
            case 1:
                Mega.SetActive(false);
                Giga.SetActive(true);
                break;
        }
    }


    void Play()
    {
    }

}
