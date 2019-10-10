using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChanger : MonoBehaviour 
{
    GameObject[] TextList;

    GameObject[] WindowList;

    GameObject MainObject;

    bool isGigaPower, DoubleInput;

    void Awake()
    {
        DoubleInput = true;

        if (this.transform.name.Contains("GIGA"))
        {
            isGigaPower = true;
            MainObject = GameObject.Find("GIGAPOWER");
        }
        else
        {
            isGigaPower = false;
            MainObject = GameObject.Find("330MEGA");
        }

        TextList = new GameObject[this.transform.childCount];
        WindowList = new GameObject[MainObject.transform.childCount];

        for (int i = 0; i < TextList.Length; i++)
            TextList[i] = this.transform.GetChild(i).gameObject;

        for (int i = 0; i < WindowList.Length; i++)
            WindowList[i] = MainObject.transform.GetChild(i).gameObject;
    }

    void Update()
    {
        if (MainObject.GetComponent<ControlBase>().isPlaying)
            return;
        
        if (isGigaPower)
            GigaPower();
        else
            MAX330MEGA();
    }

    void GigaPower()
    {
        for (int i = 0; i < 3; i++)
        {
            if (WindowList[i].transform.Find("Label").GetComponent<UILabel>().text != TextList[i].transform.Find("Label").GetComponent<UILabel>().text)
                WindowList[i].transform.Find("Label").GetComponent<UILabel>().text = TextList[i].transform.Find("Label").GetComponent<UILabel>().text;
        }

        if (WindowList[3].GetComponent<CompanyName>().Company != TextList[3].transform.Find("Label").GetComponent<UILabel>().text)
        {
            WindowList[3].GetComponent<CompanyName>().Company = TextList[3].transform.Find("Label").GetComponent<UILabel>().text;
            WindowList[3].GetComponent<CompanyName>().SetName();
        }
    }

    void MAX330MEGA()
    {
        Vector2 pos = WindowList[4].transform.localPosition;

        if (DoubleInput)
        {
            WindowList[0].transform.GetChild(1).GetChild(0).GetComponent<UILabel>().text = TextList[0].transform.Find("Label").GetComponent<UILabel>().text;
            WindowList[0].transform.GetChild(1).GetChild(1).GetComponent<UILabel>().text = TextList[1].transform.Find("Label").GetComponent<UILabel>().text;

            pos.x = 173.0f + (WindowList[0].transform.GetChild(1).GetChild(1).GetComponent<UILabel>().text.Length * 63.3f);
        }
        else
        {
            WindowList[0].transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = TextList[0].transform.Find("Label").GetComponent<UILabel>().text;
            pos.x = 173.0f + (((float)WindowList[0].transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text.Length * 0.5f) * 63.3f);
        }

        WindowList[4].transform.localPosition = pos;


        for (int i = 1; i < 5; i++)
        {
            if (i == 3)
                continue;

            if (WindowList[i].transform.Find("Label").GetComponent<UILabel>().text != TextList[i+1].transform.Find("Label").GetComponent<UILabel>().text)
                WindowList[i].transform.Find("Label").GetComponent<UILabel>().text = TextList[i+1].transform.Find("Label").GetComponent<UILabel>().text;
        }

        if (WindowList[3].GetComponent<CompanyName>().Company != TextList[4].transform.Find("Label").GetComponent<UILabel>().text)
        {
            WindowList[3].GetComponent<CompanyName>().Company = TextList[4].transform.Find("Label").GetComponent<UILabel>().text;
            WindowList[3].GetComponent<CompanyName>().SetName();
        }

    }


    public void Line1Changer(GameObject obj)
    {
        DoubleInput = DoubleInput ? false : true;
        obj.transform.GetChild(0).GetComponent<UILabel>().text = DoubleInput ? "DOUBLE" : "SINGLE";

        if (DoubleInput)
        {
            TextList[1].SetActive(true);
            WindowList[0].transform.GetChild(0).gameObject.SetActive(false);
            WindowList[0].transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            TextList[1].SetActive(false);
            WindowList[0].transform.GetChild(1).gameObject.SetActive(false);
            WindowList[0].transform.GetChild(0).gameObject.SetActive(true);
        }

        MGR_Sound.Instance.SE("ok");
    }
}
