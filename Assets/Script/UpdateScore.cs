using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {

    public GameObject ball;

    public Text scoreText;

    [HideInInspector]
    public static float score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        score = ball.transform.position.z;
        scoreText.text = string.Format("{0:N1}m",score);

    }
}
