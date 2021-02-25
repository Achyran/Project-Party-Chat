using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.Udon;
using VRC;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public string url;
    private List<AudioClip> _queue;
    public static AudioManager current;
    private float musikcheck;
    private float backgroundMusikCd;
    public AudioSource inputAudioSource;
    public AudioSource outputAudioSource;
    
    
    void Awake()
    {
        
        if(current == null)
        {
            current = this;
        }
        else
        {
            Debug.LogWarning("Mulitble Soundmanagers, destroing this");
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in sounds)
        {
            if(s.gameObjects.Count == 0)
            {
                s.source = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                for(int i = 0; i <s.gameObjects.Count; i ++ )
                {
                    s.source = s.gameObjects[i].AddComponent<AudioSource>();
                }
            }
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        //AudioSource audioSource = GetComponent<AudioSource>();
        if(inputAudioSource == null) Debug.LogError("Input AudioSouce is missng");
        inputAudioSource.volume = 0;
    }
    private void Update()
    {
        CheckInput();
        //PlayFromQueue(0);
    }
    private void CheckInput()
    {
        if(inputAudioSource.isPlaying && inputAudioSource.clip != null)
        {
            inputAudioSource.Pause();
            _queue.Add(inputAudioSource.clip);
            //inputAudioSource.clip = null;
            
        }
    }

    public void PlayFromQueue(int pIndex)
    {
        if(pIndex < _queue.Count && !outputAudioSource.isPlaying)
        {
            outputAudioSource.clip = _queue[pIndex];
            outputAudioSource.Play();
        }
    }
    public void Play(string name)
    {
        Sound target = null;
        foreach(Sound s in sounds)
        {
            if (s.name == name) target = s;
        }

        if( target == null)
        {
            Debug.LogWarning("Sound " + target + "Was not found");
            return;
        }
        target.source.Play();
        
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + s + "Was not found");
            return;
        }
        s.source.Pause();
    }
    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + s + "Was not found");
            return false;
        }
        return s.source.isPlaying;
    }
   

}
