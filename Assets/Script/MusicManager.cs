using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip musicMain;
    public AudioClip musicPlay;

    public AudioSource musicSource;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.currentState == GameState.State.PLAY)
        {
            if (musicSource.clip != musicPlay)
            {
                musicSource.clip = musicPlay;
                musicSource.Play();
            }
                
        }
        else
        {
            if (musicSource.clip != musicMain)
            {
                musicSource.clip = musicMain;
                musicSource.Play();
            }
        }
    }
}
