using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    float lastPlaybacktime;
    [SerializeField] AudioSource source;

    [SerializeField] Arrays[] clipList;

    [System.Serializable]
    public class Arrays
    {
        [SerializeField] public AudioClip intro;
        [SerializeField] public AudioClip loop;
        [SerializeField] public AudioClip outro;
    }

    [SerializeField] TMP_Text playingText;

    [SerializeField] int i = 0;
    [SerializeField] int lastI = 0;
    [SerializeField] bool guiLoop = false;
    [SerializeField] bool plyedOutro = false;
    // Start is called before the first frame update
    void Start()
    {
        OnLoop();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPlaybacktime > source.time)
        {
            OnLoop();
            source.Play((ulong)(44100*source.time));
            guiLoop = true;
        }
        lastPlaybacktime = source.time;

        SetPlayString();
    }

  public  void SetSoundTo(int number)
    {
        this.i = number;
    }

    void SetPlayString()
    {
        string text = "playing track :\n";
        text += source.clip.name;
        if (displayLoop > 0)
        {
            text += "\n-Loop-";
        }
        displayLoop -= Time.deltaTime;
        playingText.text = text;
    }

    void OnLoop()
    {
        displayLoop = 0.5f;
        if (lastI != i)
        {
            if (clipList[lastI].outro != null && !plyedOutro)
            {
                source.clip = clipList[lastI].outro;
                plyedOutro = true;
                return;
            }
            plyedOutro = false;
            lastI = i;
            if (clipList[i].intro != null)
            {
                source.clip = clipList[i].intro;
                return;
            }
        }

        source.clip = clipList[i].loop;
        return;
    }
    /*
        float mPosX = Input.mousePosition.x;
        float mPosY = Input.mousePosition.y;
        Resolution r = Screen.currentResolution;
        mPosX /= (r.width * 0.5f);
        mPosX = Mathf.Clamp01(mPosX);
        mPosY /= r.width * 0.5f;
        mPosY = Mathf.Clamp01(mPosY);

        Debug.Log(mPosY);

        AudioClip clip;


        if (mPosY > 0.5f)
        {
            AOne.volume = mPosX;
            ATwo.volume = 1 - mPosX;
            ATre.volume = 0;
            AFor.volume = 0;
        }
        else
        {
            AOne.volume = 0;
            ATwo.volume = 0;
            ATre.volume = mPosX;
            AFor.volume = 1 - mPosX;
        }
    }
    */

    float displayLoop = 0.5f;
    /*
    private void OnGUI()
    {
        string text = "track : ";
        text += source.clip.name;
        if(displayLoop > 0)
        {
            text += "  -Loop-";
        }
        displayLoop -= Time.deltaTime;
        GUI.Label(new Rect(10, 10, 300, 20), text);
    }
    */
}

