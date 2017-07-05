using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public GameObject ball;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.currentState != GameState.State.PLAY)
        {
            return;
        }
#if UNITY_STANDALONE || UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
#endif
#if UNITY_ANDROID || UNITY_IOS
        if(Input.touchCount>0&& Input.GetTouch(0).phase == TouchPhase.Ended)
#endif
        {
            ball.SendMessage("userTap");
        }

    }
}
