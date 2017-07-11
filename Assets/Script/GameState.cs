using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public GameObject ball;
    public UpdateRoad updateRoad;

    public GameObject startView;
    public GameObject gameView;
    public GameObject pauseDialog;
    public GameObject overDialog;
    public GameObject scoreView;

    public Image dialogBackground;

    public Text overScoreText;

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
        if (currentState == State.PLAY && ball.transform.position.y < -10)
        {
            gameover();
        }
    }

    public void changeState(State state)
    {
        currentState = state;
    }

    public void start()
    {
        currentState = State.PLAY;

        dialogBackground.gameObject.SetActive(false);
        startView.SetActive(false);
        gameView.SetActive(true);
    }


    public void pause()
    {
        currentState = State.PAUSE;

        gameView.SetActive(false);
        dialogBackground.gameObject.SetActive(true);
        pauseDialog.SetActive(true);

        Time.timeScale = 0;
    }

    public void gameover()
    {
        currentState = State.GAMEOVER;

        gameView.SetActive(false);
        dialogBackground.gameObject.SetActive(true);
        overDialog.SetActive(true);

        overScoreText.text = string.Format("{0:N1}m", UpdateScore.score);
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

    public void continuePlay()
    {
        currentState = State.PLAY;

        gameView.SetActive(true);
        dialogBackground.gameObject.SetActive(false);
        pauseDialog.SetActive(false);

        Time.timeScale = 1;
    }


    public void backToMain()
    {
        currentState = State.START;

        startView.SetActive(true);
        dialogBackground.gameObject.SetActive(true);
        overDialog.SetActive(false);
        pauseDialog.SetActive(false);
        scoreView.SetActive(false);

        ball.transform.position = new Vector3(0, 1, 0);
        updateRoad.resetRoad();

        Time.timeScale = 1;
    }


    public void showScore()
    {
        startView.SetActive(false);
        scoreView.SetActive(true);

        StartCoroutine(getTop10Score());
    }

    private IEnumerator getTop10Score()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("gameId=1"));

        UnityWebRequest www = UnityWebRequest.Post("http://wycode.cn/api/score/getTopScores", formData);
        yield return www.Send();

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
