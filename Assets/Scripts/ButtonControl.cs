using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : Singleton<ButtonControl> 
{
    GameObject FPSLabel;
    GameObject MainObject;

    GameObject[] ButtonList;

    bool Rec, Window;

    void Awake()
    {
        MainObject = GameObject.Find("GIGAPOWER");

        int num = this.transform.childCount;

        ButtonList = new GameObject[num];

        for (int i = 0; i < num; i++)
        {
            ButtonList[i] = this.transform.GetChild(i).gameObject;
        }

        Rec = false;
        Window = true;

        FPSLabel = GameObject.Find("FPSLabel");
    }

    public void FPSLabelText(string text)
    {
        FPSLabel.GetComponent<UILabel>().text = text;
    }

    public void TargetChange(GameObject obj)
    {
        MainObject = obj;
    }

    public void REC()
    {
        Rec = (Rec == false ? true : false);

        ButtonList[0].transform.GetChild(0).GetComponent<UIButton>().normalSprite2D = Resources.Load<Sprite>("Sprite/System/rec_" + (Rec ? "on" : "off"));

        MGR_Sound.Instance.SE("select");
    }

    public void WINDOWS()
    {
        Window = (Window == false ? true : false);

        ButtonList[1].transform.GetChild(0).GetComponent<UIButton>().normalSprite2D = Resources.Load<Sprite>("Sprite/System/window_" + (Window ? "on" : "off"));

        MGR_Sound.Instance.SE("select");
    }

    public void PLAY()
    {
        MGR_Sound.Instance.SE("ok");
        MainObject.GetComponent<ControlBase>().Play(Rec, Window);
    }


}
