using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSelector : MonoBehaviour 
{
    UI2DSprite[] Arrow;

    UILabel BGMIDLabel, TemplateLabel;
    GameObject MainObject, InputObject, BackGradient;
    GameObject[] WindowTemplate;
    GameObject[] InputTemplate;

    int BGMID, TemplateNum;

    void Awake()
    {
        Arrow = new UI2DSprite[4];

        for (int i = 0; i < 2; i++)
        {
            Arrow[i] = this.transform.Find("BGM_BUTTON").GetChild(i).GetComponent<UI2DSprite>();
            Arrow[i+2] = this.transform.Find("TEMPLATE_BUTTON").GetChild(i).GetComponent<UI2DSprite>();
        }

        BGMIDLabel = this.transform.Find("ID").GetComponent<UILabel>();
        TemplateLabel = this.transform.Find("TEMPLATE").GetComponent<UILabel>();

        WindowTemplate = new GameObject[2];
        WindowTemplate[0] = GameObject.Find("GIGAPOWER");
        WindowTemplate[1] = GameObject.Find("330MEGA");

        InputTemplate = new GameObject[2];
        InputTemplate[0] = GameObject.Find("TEXT_GIGA");
        InputTemplate[1] = GameObject.Find("TEXT_MEGA");

        MainObject = WindowTemplate[0];
        InputObject = InputTemplate[0];

        TemplateNum = 0;

        BackGradient = GameObject.Find("BACKGARDIENT");
    }

    void Start()
    {
        WindowTemplate[1].GetComponent<UIPanel>().alpha = 0.0f;
        WindowTemplate[1].GetComponent<ControlBase>().LogoAlpha();
        InputTemplate[0].SetActive(true);
        InputTemplate[1].SetActive(false);

        BGMIDLabel.text = MainObject.GetComponent<ControlBase>().BGMID.ToString();
        StartCoroutine(ArrowAnimation());
    }

    public void StartSet(int bgm, bool giga)
    {
        TemplateNum = (giga == true) ? 0 : 1;

        for (int i = 0; i < WindowTemplate.Length; i++)
        {
            WindowTemplate[i].GetComponent<UIPanel>().alpha = (i == TemplateNum) ? 1.0f : 0.0f;
            WindowTemplate[i].GetComponent<ControlBase>().LogoAlpha();
            InputTemplate[i].SetActive(((i == TemplateNum) ? true : false));
        }

        if(BackGradient != null)
            BackGradient.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/System/back_gradient" + TemplateNum.ToString());

        TemplateLabel.GetComponent<UILabel>().text = TemplateNum == 0 ? "GIGAPOW" : "330MEGA";

        MainObject = WindowTemplate[TemplateNum];

        ButtonControl.Instance.TargetChange(MainObject);

        MainObject.GetComponent<ControlBase>().BGMID = bgm;
        BGMIDLabel.text = MainObject.GetComponent<ControlBase>().BGMID.ToString();
    }

    public void TemplateClick(GameObject obj)
    {
        MGR_Sound.Instance.SE("select");

        if (MainObject.GetComponent<ControlBase>().isPlaying)
            return;

        int id = MainObject.GetComponent<ControlBase>().BGMID;

        TemplateNum = TemplateNum == 0 ? 1 : 0;

        for (int i = 0; i < WindowTemplate.Length; i++)
        {
            if (i == TemplateNum)
            {
                WindowTemplate[i].GetComponent<UIPanel>().alpha = 1.0f;
                WindowTemplate[i].GetComponent<ControlBase>().LogoAlpha();
                InputTemplate[i].SetActive(true);
            }
            else
            {
                WindowTemplate[i].GetComponent<UIPanel>().alpha = 0.0f;
                WindowTemplate[i].GetComponent<ControlBase>().LogoAlpha();
                InputTemplate[i].SetActive(false);
            }
        }

        if(BackGradient != null)
            BackGradient.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/System/back_gradient" + TemplateNum.ToString());

        TemplateLabel.GetComponent<UILabel>().text = TemplateNum == 0 ? "GIGAPOW" : "330MEGA";

        MainObject = WindowTemplate[TemplateNum];

        ButtonControl.Instance.TargetChange(MainObject);

        MainObject.GetComponent<ControlBase>().BGMID = id;
    }

    public void ArrowClick(GameObject obj)
    {
        MGR_Sound.Instance.SE("select");

        if (MainObject.GetComponent<ControlBase>().isPlaying)
            return;
        
        switch (obj.transform.name)
        {
            case "LEFT":
                MainObject.GetComponent<ControlBase>().BGMID--;

                if (MainObject.GetComponent<ControlBase>().BGMID < 0)
                    MainObject.GetComponent<ControlBase>().BGMID = 6;
                break;

            case "RIGHT":
                MainObject.GetComponent<ControlBase>().BGMID++;

                if (MainObject.GetComponent<ControlBase>().BGMID > 6)
                    MainObject.GetComponent<ControlBase>().BGMID = 0;
                break;
        }



        BGMIDLabel.text = MainObject.GetComponent<ControlBase>().BGMID.ToString();
    }

    IEnumerator ArrowAnimation()
    {
        int count = 0;
        while(true)
        {
            for (int i = 0; i < 4; i++)
            {
                Arrow[i].sprite2D = Resources.Load<Sprite>("Sprite/System/arrow" + count.ToString());
            }

            count++;

            if (count > 3)
                count = 0;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
