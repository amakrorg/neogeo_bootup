using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBase : MonoBehaviour 
{
    protected GameObject Background;
    protected GameObject CaptureCam;
    protected GameObject WinodwObject;
    protected GameObject[] TextLine;
    protected GameObject FullScreen, MVSUI, FullScreenCam;

    public bool isPlaying;

    public int BGMID;
    protected int PrevFPS;
   

    protected void Awake()
    {

        FullScreen = GameObject.Find("FullScreenImage");
        MVSUI = GameObject.Find("MVS_UI");
        FullScreenCam = GameObject.Find("FullSreenCam");

        int num = this.transform.childCount;

        TextLine = new GameObject[num];

        for (int i = 0; i < num; i++)
        {
            TextLine[i] = this.transform.GetChild(i).gameObject;
        }

        WinodwObject = GameObject.Find("FullScreen");
        Background = GameObject.Find("BG_Sprite");
        CaptureCam = GameObject.Find("CaptureCam");

        Background.GetComponent<UI2DSprite>().color = new Color(0, 0, 0, 1);

        isPlaying = false;

        BGMID = 0;
    }

    void Start()
    {
        if (FullScreen.activeSelf)
            FullScreen.SetActive(false);

        if(MVSUI.activeSelf == false)
            MVSUI.SetActive(true);

        if (FullScreenCam.activeSelf)
            FullScreenCam.SetActive(false);
    }

    public void LogoAlpha()
    {
        if(this.GetComponent<UIPanel>().alpha > 0.0f)
            TextLine[3].GetComponent<CompanyName>().ColorChange(new Color(0, 104.0f / 255, 232.0f / 255, 1));
        else
            TextLine[3].GetComponent<CompanyName>().ColorChange(Color.red, true);
    }


    public void Play(bool rec, bool windows = true)
    {
        if (isPlaying)
            return;

        isPlaying = true;

        if(this.transform.name.Contains("GIGA"))
            StartCoroutine(this.GetComponent<GigapowerControl>().AnimationProcess(rec, windows));
        else
            StartCoroutine(this.GetComponent<MegaControl>().AnimationProcess(rec, windows));


    }


    protected IEnumerator PlayCount(float time, int prev, bool windows, bool rec = false)
    {

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }       

        if (windows == false)
        {
            FullScreen.SetActive(false);
            MVSUI.SetActive(true);
            WinodwObject.GetComponent<UIPanel>().alpha = 1.0f;
            FullScreenCam.SetActive(false);
        }

        isPlaying = false;

        Application.targetFrameRate = PrevFPS;
    }
}
