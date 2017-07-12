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
    public Text overRankingText;

    public Text myNameText;
    public Text myScoreText;
    public Text myRankingText;
    


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

        overScoreText.text = string.Format("{0:N0}m", UpdateScore.score);

        if (UpdateScore.setMyScore())
        {
            StartCoroutine(uploadScore((int)UpdateScore.score));
        }

        StartCoroutine(getMyRank((int)UpdateScore.score, overRankingText));
    }

    private IEnumerator uploadScore(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameId", "1");
        form.AddField("score", score);
        form.AddField("userId", UserManager.guestGUID);
        form.AddField("platform", Application.platform.ToString());
        form.AddField("name", "游客"+UserManager.guestNumber);
        form.AddField("secret", "wycode.cn");
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://wycode.cn/api/score/saveMyScore", form))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
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

        setMyInfo();
        StartCoroutine(getTop10Score());
    }

    private void setMyInfo()
    {
        myNameText.text = "游客" + UserManager.guestNumber;
        int myScore = UpdateScore.getMyScore();
        myScoreText.text = myScore + "m";
        StartCoroutine(getMyRank(myScore,myRankingText));
    }
    
    private IEnumerator getMyRank(int myScore,Text textView)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameId", "1");
        form.AddField("score", myScore);

        using (UnityWebRequest www = UnityWebRequest.Post("http://wycode.cn/api/score/getMyRanking", form))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.responseCode == 200)
                {

                    resolveRankingData(www.downloadHandler.text, textView);

                }

            }
        }
    }

    private void resolveRankingData(string text, Text textView)
    {
        WyResultRanking result = JsonUtility.FromJson<WyResultRanking>(text);
        textView.text = "全球排名第" + result.data;

    }

    private IEnumerator getTop10Score()
    {
        WWWForm form = new WWWForm();
        form.AddField("gameId", "1");

        using (UnityWebRequest www = UnityWebRequest.Post("http://wycode.cn/api/score/getTopScores", form))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.responseCode == 200) {

                    resolveGlobalData(www.downloadHandler.text);
                    
                }
                
            }
        }
    }

    private void resolveGlobalData(string text)
    {
        WyResultGameScore result = JsonUtility.FromJson<WyResultGameScore>(text);
        if (result.code == 1)
        {
            GameObject[] cells = GameObject.FindGameObjectsWithTag("GameScoreCell");
            for(int i = 0; i < result.data.Count; i++)
            {
                GameObject cell = cells[i];
                GameScore gameScore = result.data[i];

                Text[] texts = cell.GetComponentsInChildren<Text>();

                texts[0].text = (i + 1).ToString();
                texts[1].text = gameScore.score+"m";
                texts[2].text = gameScore.name;
            }
        }else
        {
            Debug.Log(result.message);
        }
    }
}
