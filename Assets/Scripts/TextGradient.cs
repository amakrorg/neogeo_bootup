using UnityEngine;
using System.Collections;

public class TextGradient : MonoBehaviour 
{
    float MoveWeight;
    float Length;
    float Weight;

    Vector3 Start, End;

    public void SetSpeed(float time, float length, float weight = 1.3f)
    {
        MoveWeight = time;
        Length = length;
        Weight = weight;
        this.transform.localPosition = Start = new Vector3(length * -0.5f, 0, 0);
        End = new Vector3(length * 0.5f, 0, 0);
    }



    public IEnumerator ShowProcess()
    {
        float time = 0.0f;

        this.transform.parent.GetComponent<UIPanel>().alpha = 1.0f;

        while (time < Weight)
        {
            this.transform.localPosition = Vector3.Lerp(Start, End, time / Weight);

            time += Time.deltaTime * MoveWeight;

            yield return new WaitForSeconds(Time.deltaTime * MoveWeight);
        }

        this.gameObject.SetActive(false);
    }
}
