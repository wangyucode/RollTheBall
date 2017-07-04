using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {
    public GameObject gameManager;

    public float speedZ= 1;

    public float speedX = 1;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //transform.localPosition += new Vector3(0, 0, Time.deltaTime* speedZ);
        //if(rb.velocity.y)

        if (GameState.currentState == GameState.State.PLAY && transform.position.y < -10)
        {
            gameManager.SendMessage("gameover");
        }
        
    }

    private void FixedUpdate()
    {
        if (GameState.currentState == GameState.State.PLAY)
        {
            rb.AddForce(new Vector3(0, -2000, 0));
            rb.velocity = new Vector3(speedX, rb.velocity.y, speedZ);
        }
        
    }

    public void userTap()
    {
        speedX *= -1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Border"))
        {
            userTap();
        }
    }
}
