using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBall : MonoBehaviour {

    public GameObject ball;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        float x = 0;

        float y = ball.transform.position.y + offset.y;

        float z = ball.transform.position.z + offset.z;

        transform.position = new Vector3(x, y, z);
       
    }
}
