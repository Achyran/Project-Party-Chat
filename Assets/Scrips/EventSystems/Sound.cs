using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch = 1;


    public bool loop;
    [Header("Origin (null = global)")]
    public List<GameObject> gameObjects;
    

    [HideInInspector]
    public AudioSource source;
}
