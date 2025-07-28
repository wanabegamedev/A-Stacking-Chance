using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource source;
    
    

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        if (instance != null && instance != this)
        {
            Destroy(this);
            
        }
        else
        {
            instance = this;
        }
        
  
    }

    public  void PlaySound(AudioClip snd)
    {
        source.clip = snd;
        print("hi");
        source.Play();
    }
    
    public  void StopSound()
    {
        source.Stop();
    }
    
    
}
