using UnityEngine;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class CompanyName : MonoBehaviour 
{
    SpriteRenderer[] Name;

    Sprite[] Sprites;

    float PosY;

    public float Width, Size;

    public string Company;

    void Awake()
    {
        int labelNum = this.transform.childCount;

        Name = new SpriteRenderer[labelNum];

        for (int i = 0; i < labelNum; i++)
        {
            Name[i] = this.transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        PosY = this.transform.localPosition.y;

        Sprites = Resources.LoadAll<Sprite>("Sprite/Company/company_font");

        Width = 1.6f;

        Company = "SNK";

        SetName();
    }


    public void SetName()
    {
        char temp;

        int count = 0;

        Company = Company.ToUpper();

        for (int i = 0; i < Company.Length; i++)
        {
            if (count >= Name.Length)
                break;
            
            temp = Company.ToCharArray()[i];
                
            if (CharacterCheck(temp.ToString()) == false)
                continue;
            
            int id = Convert.ToInt32(temp);

            id = (id >= 65 ? id - 65 : (id - 48 + 26));

            Name[count].sprite = Sprites[id];
            Name[count].color = new Color(0, 104.0f / 255, 232.0f / 255);
            count++;    
        }

        for (int i = count; i < Name.Length; i++)
        {
            Name[i].sprite = null;
        }

        float strLength = 0.0f;
        float prev = 0.0f, dest = 0.0f;

        for (int i = 0; i < Name.Length; i++)
        {
            if (Name[i].sprite == null)
                break;

            dest = Name[i].sprite.rect.width * 1.6f;

            if (i > 0)
                Name[i].transform.localPosition = new Vector3(Name[i - 1].transform.localPosition.x + prev + Width, 0, 0);
            else
                Name[i].transform.localPosition = Vector3.zero;

            prev = dest;

            strLength = Name[i].transform.localPosition.x + prev;
        }

        strLength *= -0.5f;
        strLength -= (strLength % 1.6f);

        this.transform.localPosition = new Vector3(strLength, PosY, 0);
    }
        

    void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.Q))
            SetName();    

        return;

        float strLength = 0.0f;
        float prev = 0.0f, dest = 0.0f;

        for (int i = 0; i < Name.Length; i++)
        {
            dest = Name[i].sprite.rect.width * 1.6f;

            if (i > 0)
                Name[i].transform.localPosition = new Vector3(Name[i - 1].transform.localPosition.x + prev + Width, 0, 0);
            else
                Name[i].transform.localPosition = Vector3.zero;

            prev = dest;

            strLength = Name[i].transform.localPosition.x + prev;
        }

        strLength *= -0.5f;
        strLength -= (strLength % 1.6f);

        this.transform.localPosition = new Vector3(strLength, PosY, 0);

    }

    public static bool CharacterCheck(string letter)
    {
        bool IsCheck = true;
        Regex engRegex = new Regex(@"[a-zA-Z]");
        bool ismatch = engRegex.IsMatch(letter);
        Regex numRegex = new Regex(@"[0-9]");
        bool ismatchNum = numRegex.IsMatch(letter);

        if (!ismatch && !ismatchNum)
        {
            IsCheck = false;
        }
        return IsCheck;
    }


    public void ColorChange(Color color, bool alpha = false)
    {
        if (alpha)
            color = new Color(0, 0, 0, 0);

     
        
        for (int i = 0; i < Name.Length; i++)
        {
            if (Name[i].sprite == null)
                return;

            Name[i].color = color;
        }
    }

}
