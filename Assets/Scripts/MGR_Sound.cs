using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MGR_Sound : Singleton<MGR_Sound> 
{
    public AudioSource soundBGM;
    public AudioSource soundSE;

    public AudioClip[] SE_BGM;
    public AudioClip[] SE_Effect;

    Dictionary<string, AudioClip> BGMList;
    Dictionary<string, AudioClip> SEList;

    void Awake()
    {
        GameObject soundGroup = new GameObject();
        soundGroup.transform.name = "[SOUND] SOUND OBJECT";
        soundGroup.AddComponent<AudioSource>();
        soundGroup.transform.SetParent(this.transform);
        soundSE = soundGroup.GetComponent<AudioSource>();

        GameObject voiceGroup = new GameObject();
        voiceGroup.transform.name = "[SOUND] MUSIC OBJECT";
        voiceGroup.AddComponent<AudioSource>();
        voiceGroup.transform.SetParent(this.transform);
        soundBGM = voiceGroup.GetComponent<AudioSource>();
    

        BGMList = new Dictionary<string, AudioClip>();
        SEList = new Dictionary<string, AudioClip>();
        Object[] bgm = Resources.LoadAll("Sound/BGM/");
        for(int i = 0; i < bgm.Length; i++)
        {
            BGMList.Add(bgm[i].name, (AudioClip)bgm[i]);
        }


        Object[] effect = Resources.LoadAll("Sound/SE/");
        for(int i = 0; i < effect.Length; i++)
        {
            SEList.Add(effect[i].name, (AudioClip)effect[i]);
        }
    }

  

    /*
    public bool SE(string name, float time = 0)
    {
        if (soundSE == null)
            return false;


        if (SEList.ContainsKey(name))
        {
            soundSE.clip = SEList[name];
            StartCoroutine(SoundPlay(time, soundSE, "[SE] " + name));
            return true;
        }

        return false;
    }
    */

    public bool SETime(string name, float time, bool loop)
    {
        if (soundSE == null)
            return false;

        for (int i = 0; i < SE_Effect.Length; i++)
        {
            if (SE_Effect[i].name == name)
            {
                AudioSource soundClip = Instantiate(soundSE, soundSE.transform.parent.transform) as AudioSource;
                soundClip.clip = SE_Effect[i];
                soundClip.loop = loop;
                soundClip.Play();
                Destroy(soundClip.gameObject, time == 0.0f ? (float)soundClip.clip.length : time);
                return true;
            }
        }

        return false;
    }

    IEnumerator SoundPlay(float time, AudioSource soundSE, string name)
    {
        yield return new WaitForSeconds(time);

        AudioSource soundClip = Instantiate(soundSE, soundSE.transform.parent.transform) as AudioSource;
        soundClip.transform.name = name;
        soundClip.Play();    

        Destroy(soundClip.gameObject, (float)soundClip.clip.length);
    }



    public bool SE(string name, float time, Vector3 playPos)
    {
        for (int i = 0; i < SE_Effect.Length; i++)
        {
            if (SE_Effect[i].name.Contains(name))
            {
                soundSE.clip = SE_Effect[i];

                StartCoroutine(SoundPlay(time, soundSE, "[SE] " + name, playPos));
                return true;
            }
        }
        return false;
    }





    IEnumerator SoundPlay(float time, AudioSource soundSE, string name, Vector3 playPos)
    {
        yield return new WaitForSeconds(time);
        AudioSource.PlayClipAtPoint(soundSE.clip, playPos);
    }




    public void SE(string name)
    {
        if (SEList.ContainsKey(name))
        {
            AudioSource soundClip = Instantiate(soundSE, soundSE.transform.parent.transform) as AudioSource;
            soundClip.clip = SEList[name];
            soundClip.loop = false;
            soundClip.Play();
            Destroy(soundClip.gameObject, (float)soundClip.clip.length);
        }
    }




    public float BGM(string name, bool loop = true, bool mute = false)
    {
        if (soundBGM == null)
        {
            Debug.Log("BGM OBJECT NULL");
            return 0;
        }

        else if (BGMList.ContainsKey(name))
        {
            soundBGM.clip = BGMList[name];
            soundBGM.loop = loop;
            soundBGM.Stop();

            if(mute == false)
                soundBGM.Play();

            return soundBGM.clip.length;
        }
        else
        {
            Debug.Log("BGM NOT FOUND");
        }

        return 0;
    }


    IEnumerator BGMpart(string name)
    {
        AudioClip partA = null, partB = null;

        for (int i = 0; i < SE_BGM.Length; i++)
        {
            if (SE_BGM[i].name.Contains(name))
            {
                if (SE_BGM[i].name.Contains("partA"))
                {
                    partA = SE_BGM[i];
                }
                else if (SE_BGM[i].name.Contains("partB"))
                {
                    partB = SE_BGM[i];
                }
                
            }

            if(partA != null && partB != null)
                break;
        }

        soundBGM.clip = partA;
        soundBGM.loop = false;
        soundBGM.Play();

        yield return new WaitForSeconds(partA.length);

        soundBGM.clip = partB;
        soundBGM.loop = true;
        soundBGM.Play();
    }
}
