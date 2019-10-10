using UnityEngine;
using System.Collections;

public class TweenGroupReset : MonoBehaviour 
{
    private float colorAlpha;
    private Vector3 position;
    private Vector3 scale;


    void Awake()
    {
        if (this.GetComponent<UISprite>())
        {
            colorAlpha = this.GetComponent<UISprite>().color.a;
        }
        else if (this.GetComponent<UI2DSprite>())
        {
            colorAlpha = this.GetComponent<UI2DSprite>().color.a;
        }
        else if (this.GetComponent<SpriteRenderer>())
        {
            colorAlpha = this.GetComponent<SpriteRenderer>().color.a;
        }

        position = this.transform.localPosition;
        scale = this.transform.localScale;


    }



    void OnEnable()
    {
        if (this.GetComponent<UISprite>())
        {
            this.GetComponent<UISprite>().color = new Color(this.GetComponent<UISprite>().color.r, this.GetComponent<UISprite>().color.g, this.GetComponent<UISprite>().color.b, colorAlpha);
        }
        else if (this.GetComponent<UI2DSprite>())
        {
            this.GetComponent<UI2DSprite>().color = new Color(this.GetComponent<UI2DSprite>().color.r, this.GetComponent<UI2DSprite>().color.g, this.GetComponent<UI2DSprite>().color.b, colorAlpha);
        }
        else if (this.GetComponent<SpriteRenderer>())
        {
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, colorAlpha);
        }

        this.transform.localPosition = position;
        this.transform.localScale = scale;


        if (this.GetComponent<TweenAlpha>())
        {
            this.GetComponent<TweenAlpha>().ResetToBeginning();
            this.GetComponent<TweenAlpha>().enabled = true;
        }

        if (this.GetComponent<TweenRotation>())
        {
            this.GetComponent<TweenRotation>().ResetToBeginning();
            this.GetComponent<TweenRotation>().enabled = true;
        }

        if (this.GetComponent<TweenScale>())
        {
            this.GetComponent<TweenScale>().ResetToBeginning();
            this.GetComponent<TweenScale>().enabled = true;
        }

        if (this.GetComponent<TweenPosition>())
        {
            this.transform.localPosition = this.GetComponent<TweenPosition>().from;
            this.GetComponent<TweenPosition>().ResetToBeginning();
            this.GetComponent<TweenPosition>().enabled = true;
        }


    }

}
