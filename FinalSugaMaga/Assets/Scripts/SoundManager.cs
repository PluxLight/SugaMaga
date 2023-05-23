using Ricimi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;
    private static SoundManager soundObject;
    AudioSource bgm;

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }

    private void Awake()
    {
        if (soundObject != null)
        {
            Destroy(gameObject);
            return;
        }
        soundObject = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BgmPlay()
    {
        if (soundObject == null)
        {
            bgm = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            bgm = soundObject.GetComponent<AudioSource>();
        }
        bgm.enabled = true;
    }

    public void BgmStop()
    {
        if (soundObject == null)
        {
            bgm = gameObject.GetComponent<AudioSource>();
        }
        else
        {
            bgm = soundObject.GetComponent<AudioSource>();
        }
        bgm.enabled = false;
    }
}
