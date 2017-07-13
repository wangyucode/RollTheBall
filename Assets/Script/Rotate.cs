using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public int speedX = 20;
    public int speedY = 20;
    public int speedZ = 20;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime,Space.World);
    }

}
