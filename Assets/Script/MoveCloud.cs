using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour {

    public GameObject ball;

    private float offsetZ;

    // Use this for initialization
    void Start () {
        offsetZ = transform.position.z - ball.transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position= new Vector3(transform.position.x, transform.position.y , ball.transform.position.z + offsetZ);

    }
}
