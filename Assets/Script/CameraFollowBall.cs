using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBall : MonoBehaviour
{

    public GameObject ball;

    public float effectStep = 0.4f;

    private Vector3 offset;

    private Vector3 effectTarget;

    private Vector3 effectCurrent;

    private Vector3 velocity;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        effectCurrent = Vector3.SmoothDamp(effectCurrent, effectTarget, ref velocity, 0.2f);

        transform.position = ball.transform.position + offset + effectCurrent;

        transform.LookAt(ball.transform);

    }

    public void resetCamera()
    {
        effectTarget = Vector3.zero;
    }

    void larger()
    {
        effectTarget.y += effectStep;
    }

    void smaller()
    {
        effectTarget.y -= effectStep;
    }


    void faster()
    {
        effectTarget.z -= effectStep;
    }

    void slower()
    {
        effectTarget.z += effectStep;
    }

}
