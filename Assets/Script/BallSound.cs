using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour {

    public AudioClip soundFall;
    public AudioClip soundWood;
    public AudioClip soundMetal;

    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if(GameState.currentState != GameState.State.PLAY)
        {
            return;
        }
        if (transform.position.y < 0)
        {
            if (source.clip != soundFall)
            {
                source.clip = soundFall;
                source.Play();
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag.Equals("FloorMetal"))
        {
            source.clip = soundMetal;
            source.Play();
        }
        else if (tag.Equals("FloorWood"))
        {
            source.clip = soundWood;
            source.Play();
        }
        else if (tag.Equals("FloorLarger"))
        {
            source.clip = soundMetal;
            source.Play();
        }
        else if (tag.Equals("FloorSmaller"))
        {
            source.clip = soundMetal;
            source.Play();
        }

        else if (tag.Equals("FloorFaster"))
        {
            source.clip = soundMetal;
            source.Play();
        }
        else if (tag.Equals("FloorSlower"))
        {
            source.clip = soundMetal;
            source.Play();
        }
    }

}
