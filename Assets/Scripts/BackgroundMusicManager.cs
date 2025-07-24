using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicManager : MonoBehaviour
{
   [SerializeField] private AudioSource source;

    [SerializeField] private List<AudioClip> tracks;

    private List<AudioClip> usedTracks = new ();


    private void Start()
    {
        source.GetComponent<AudioSource>();
        
        DontDestroyOnLoad(gameObject);
        
        source.clip = tracks[0];
        source.Play();
        
        usedTracks.Add(tracks[0]);
        tracks.RemoveAt(0);
        
    }


    private void Update()
    {
        //track must have finished, select next one or shuffle
        if (!source.isPlaying)
        {
            SelectNewTrack();
        }
    }

    private void SelectNewTrack()
    {
        if (tracks.Count < 1)
        {
            tracks = usedTracks;
        }

        source.clip = tracks[0];
        
    }
}
