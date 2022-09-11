using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
    bool playing;
    [SerializeField] AudioClip onIn;
    [SerializeField] AudioClip onLoop;
    [SerializeField] AudioClip onOut;
    [SerializeField] AudioClip next;

    [SerializeField] string playingText;

    AudioClip Request(AudioClip currentClip)
    {
        if (currentClip != onLoop)
        {
            next = currentClip;
            if (playing && onOut != null)
            {
                playing = false;
                return onOut;
            }
            playing = false;
            return next;
        }

        if (playing)
            return onLoop;
        else
        {
            playing = true;
            return onIn;
        }
    }
}
