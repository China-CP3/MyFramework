using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class MusicManager : Singleton<MusicManager>
{
    private AudioSource musicSource = null;
    private GameObject soundObj = new GameObject("Sound");//该空物体上可以有多个音效AudioSource
    private List<AudioSource> soundList=new List<AudioSource>();
    private float bkVolume = 0.7f;
    private float soundVolume = 0.7f;
    public MusicManager()
    {
        MonoManager.Instance.AddListner(Update);
    }
    private void Update()
    {
        for (int i = soundList.Count-1; i>=0; i--)
        {
            if (!soundList[i].isPlaying)
            {
                Object.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }
    public void PlayBkMusic(string name)
    {
        if (musicSource == null)
        {
            GameObject obj = new GameObject("BkMusic");
            musicSource = obj.AddComponent<AudioSource>();
        }
        ResourcesManager.Instance.LoadAsync<AudioClip>("Music/Bk/" + name, (clip) =>
        {
            musicSource.clip = clip;
            musicSource.volume = bkVolume;
            musicSource.Play();
        });
    }
    public void StopBkMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    public void PauseBkMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }
    public void ChangeBkVolume(float volume)
    {
        if (musicSource != null)
        {
            bkVolume = volume;
            musicSource.volume = volume;
        }
    }
    public void ChangeSoundVolume(float volume)
    {
        soundVolume = volume;
        if (soundList.Count > 0)
        {
            for (int i = 0; i < soundList.Count; i++)
            {
                soundList[i].volume = volume;
            }
        }
    }
    public void PlaySound(string name, bool isLoop = false, UnityAction<AudioSource> callBack = null)
    {
        ResourcesManager.Instance.LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource audioSource = soundObj.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.loop = isLoop;
            audioSource.volume = bkVolume;
            audioSource.Play();
            soundList.Add(audioSource);
            callBack?.Invoke(audioSource);
        });
    }
    public void StopSound(AudioSource audioSource)
    {
        if (soundList.Contains(audioSource))
        {
            audioSource.Stop();
            soundList.Remove(audioSource);
            Object.Destroy(audioSource);
        }
    }
}
