using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaControl : ControlBase 
{
    Color TargetColor;

    void Awake()
    {
        base.Awake();

        TargetColor = new Color(240.0f / 255, 240.0f / 255, 240.0f / 255, 1);
    }

    public IEnumerator AnimationProcess(bool rec, bool windows)
    {
        int prevDisplay = 0;

        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < TextLine.Length; i++)
        {
            TextLine[i].GetComponent<UIPanel>().alpha = 0;

            if (TextLine[i].GetComponent<CompanyName>() != null)
            {
                TextLine[i].GetComponent<CompanyName>().ColorChange(Color.black, true);
            }
        }

        Background.GetComponent<UI2DSprite>().color = TargetColor;

        if (rec)
        {
            PrevFPS = Application.targetFrameRate;
            Application.targetFrameRate = 30;
            //CaptureCam.GetComponent<CameraCapture>().enabled = true;
        }


        if (windows == false)
        {
            /*
            prevDisplay = CaptureCam.GetComponent<Camera>().targetDisplay;
            CaptureCam.GetComponent<Camera>().targetDisplay = Camera.main.targetDisplay;

            */
            WinodwObject.GetComponent<UIPanel>().alpha = 0.0f;
            MVSUI.SetActive(false);
            FullScreen.SetActive(true);
            FullScreenCam.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        float time = MGR_Sound.Instance.BGM("bootup" + BGMID.ToString(), false, rec);


        StartCoroutine(PlayCount(time, prevDisplay, windows));

        yield return StartCoroutine(Line1());

        yield return StartCoroutine(Line2());

        yield return StartCoroutine(Line3());

        yield return StartCoroutine(Line4());
    }


    IEnumerator Line5()
    {
        yield return new WaitForSeconds(0.13f);
        TextLine[4].GetComponent<UIPanel>().alpha = 1.0f;
    }



    IEnumerator Line4()
    {
        Color start = new Color(0, 40.0f / 255, 248.0f / 255, 0);
        Color end = new Color(0, 237.0f / 255, 255, 1);

        bool loop = false;
        float time, targetTime;

        targetTime = 0.37f;


        TextLine[3].GetComponent<CompanyName>().ColorChange(start);

        TextLine[3].GetComponent<UIPanel>().alpha = 1.0f;

        while (true)
        {
            time = 0.0f;

            while (time < targetTime)
            {
                TextLine[3].GetComponent<CompanyName>().ColorChange(Color.Lerp(start, end, time / targetTime));
                time += Time.deltaTime;

                yield return null;
            }

            if (loop)
                break;

            start = end;
            end = new Color(0, 104.0f / 255, 232.0f / 255, 1);

            targetTime = 0.17f;
            loop = true;

            StartCoroutine(Line5());

        }

        //0.37
    }

    IEnumerator Line3()
    {
        TextLine[2].transform.GetChild(0).gameObject.SetActive(true);

        float length = TextLine[2].transform.GetChild(1).GetComponent<UILabel>().text.Length * 17;

        TextLine[2].transform.GetChild(0).GetComponent<TextGradient>().SetSpeed(2, length, 0.9f);

        yield return StartCoroutine(TextLine[2].transform.GetChild(0).GetComponent<TextGradient>().ShowProcess());

        yield return new WaitForSeconds(0.34f);
    }

    IEnumerator Line2()
    {
        TextLine[1].transform.GetChild(0).gameObject.SetActive(true);

        float length = TextLine[1].transform.GetChild(1).GetComponent<UILabel>().text.Length * 20;

        TextLine[1].transform.GetChild(0).GetComponent<TextGradient>().SetSpeed(2, length, 0.9f);

        yield return StartCoroutine(TextLine[1].transform.GetChild(0).GetComponent<TextGradient>().ShowProcess());
    }

    IEnumerator Line1()
    {
        Vector3 start = new Vector3(0, 1, 1);
        Vector3 end = new Vector3(-1, 1, 1);

        TextLine[0].GetComponent<UIPanel>().alpha = 1.0f;
        TextLine[0].transform.localScale = start;

        if (TextLine[0].transform.GetChild(0).gameObject.activeSelf)
            TextLine[0].transform.GetChild(0).GetChild(0).GetComponent<UILabel>().color = new Color(0, 0, 0);
        else
        {
            TextLine[0].transform.GetChild(1).GetChild(0).GetComponent<UILabel>().color = new Color(0, 0, 0);
            TextLine[0].transform.GetChild(1).GetChild(1).GetComponent<UILabel>().color = new Color(0, 0, 0);
            TextLine[0].transform.GetChild(1).GetChild(2).GetComponent<UI2DSprite>().color = new Color(0, 0, 0);
        }

        float time = 0.0f;
        while (time < 0.6f)
        {
            TextLine[0].transform.localScale = Vector3.Lerp(start, end, time / 0.6f);
            time += Time.deltaTime;

            yield return null;
        }
        TextLine[0].transform.localScale = end;

        yield return new WaitForSeconds(0.2f);


        start = new Vector3(-1, 1, 1);
        end = new Vector3(-1, 0, 1);

        bool loop = false;

        StartCoroutine(BGColor(TargetColor, new Color(0, 0, 0, 1), 1.06f));

        while (true)
        {
            time = 0.0f;

            while (time < 0.53f)
            {
                TextLine[0].transform.localScale = Vector3.Lerp(start, end, time / 0.53f);
                time += Time.deltaTime;

                yield return null;
            }
            TextLine[0].transform.localScale = end;

            if (loop)
                break;

            start = new Vector3(1, 0, 1);
            end = new Vector3(1, 1, 1);


            if (TextLine[0].transform.GetChild(0).gameObject.activeSelf)
                TextLine[0].transform.GetChild(0).GetChild(0).GetComponent<UILabel>().color = TargetColor;
            else
            {
                TextLine[0].transform.GetChild(1).GetChild(0).GetComponent<UILabel>().color = TargetColor;
                TextLine[0].transform.GetChild(1).GetChild(1).GetComponent<UILabel>().color = TargetColor;
                TextLine[0].transform.GetChild(1).GetChild(2).GetComponent<UI2DSprite>().color = TargetColor;
            }
            //TextLine[0].transform.GetChild(0).GetComponent<UILabel>().effectColor = new Color(1, 1, 1);

            loop = true;
        }
    }

    IEnumerator BGColor(Color start, Color end, float tweenTime)
    {
        float time = 0.0f;

        while(time < tweenTime)
        {
            Background.GetComponent<UI2DSprite>().color  = Color.Lerp(start, end, time / tweenTime);

            time += Time.deltaTime;

            yield return null;
        }
    }
}
