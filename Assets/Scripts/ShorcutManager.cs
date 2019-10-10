using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct startValue
{
	public bool giga;
	public bool doubleLine;
	public string line1; 
	public string line1b; 
	public string line2; 
	public string line3; 
	public string line4; 
	public string line5;
	public int bgm;
	public bool autoPlay;
}
public class ShorcutManager : MonoBehaviour
 {
	 /* 
	public static string Clipboard
   {
     get { return GUIUtility.systemCopyBuffer; }
     set { GUIUtility.systemCopyBuffer = value; }
   }
   */
   /*
   type=giga / mega / 
   double=yes
   line1=
   line2=
   line3=
   line4=
   line5=
   bgm=
   play=yes
    */
	

	 public startValue StartValue;
	 public GameObject Selecter;
	 
	 void Start()
	 {
		 string startValue = Application.absoluteURL;
		/*
		 if(startValue.Contains("?") == false)
		 	return;

		StartValueSet(startValue);
 		*/
		 Selecter.GetComponent<BGMSelector>().StartSet(4, false);
	 }


	 void StartValueSet(string startValue)
	 {

		 startValue = startValue.Substring(startValue.IndexOf("?") + 1, startValue.Length - (startValue.IndexOf("?") + 2));

		 string[] splitData = startValue.Split('&');

		 for(int i = 0; i < splitData.Length; i++)
		 {
			 string[] data = splitData[i].Split('=');

			 if(data.Length <= 1) continue;
			 
			 switch(data[0])
			 {
				 case "type":
				 StartValue.giga = data[1] == "giga" ? true : false;
				 break;

				 case "double":
				 StartValue.doubleLine = data[1] == "yes" ? true : false;
				 break;

				 case "line1":
				 if(data[1].Contains(","))
				 {
					 StartValue.line1 = data[1].Substring(0, data[1].IndexOf(',') - 1);
					 StartValue.line1b = data[1].Substring(data[1].IndexOf(','), data[1].Length - (data[1].IndexOf(',')+ 1));
				 }
				 else
				StartValue.line1 = data[1];
				 break;

				 case "line2":
				StartValue.line2 = data[1];
				 break;

				 case "line3":
				StartValue.line3 = data[1];
				 break;

				 case "line4":
				StartValue.line4 = data[1];
				 break;

				 case "line5":
				 StartValue.line5 = data[1];
				 break;

				 case "bgm":
				 StartValue.bgm = int.Parse(data[1]);
				 break;

				 case "auto":
				 StartValue.autoPlay = data[1].Contains("yes") ? true : false;
				 break;
			 }
		 }
	 }

}
