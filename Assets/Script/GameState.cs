using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public GameObject ball;
    public UpdateRoad updateRoad;

    public GameObject startView;
    public GameObject gameView;
    public GameObject pauseDialog;
    public GameObject overDialog;

    public Image dialogBackground;

    public enum State
    {
        START,
        PLAY,
        PAUSE,
        GAMEOVER
    }

    [HideInInspector]
    public static State currentState;

	// Use this for initialization
	void Start () {
        currentState = State.START;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeState(State state)
    {
        currentState = state;
    }

    public void start()
    {
        currentState = State.PLAY;

        startView.SetActive(false);
        gameView.SetActive(true);
    }


    public void pause()
    {
        currentState = State.PAUSE;

        gameView.SetActive(false);
        dialogBackground.gameObject.SetActive(true);
        pauseDialog.SetActive(true);
    }

    public void gameover()
    {
        currentState = State.GAMEOVER;

        gameView.SetActive(false);
        dialogBackground.gameObject.SetActive(true);
        overDialog.SetActive(true);
    }


    public void restart()
    {
        currentState = State.PLAY;

        dialogBackground.gameObject.SetActive(false);
        overDialog.SetActive(false);
        gameView.SetActive(true);

        ball.transform.position = new Vector3(0, 1, 0);
        updateRoad.resetRoad();
    }

}
