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
    public GameObject editNameDialog;

    public Image dialogBackground;

    public Text overScoreText;
    public Text overRankingText;

    public Text myNameText;
    public Text myScoreText;
    public Text myRankingText;

    public InputField nameInput;
    public Text editNameTitle;
    public Text editNamePlaceholder;

    public Text userNameWelcome;


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

        if (UpdateScore.setMyScore((int)UpdateScore.score))
        {
            StartCoroutine(uploadScore((int)UpdateScore.score));
        }

        StartCoroutine(getMyRank(UpdateScore.getMyScore(), overRankingText));
    }

    private IEnumerator uploadScore(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameId", "1");
        form.AddField("score", score);
        form.AddField("userId", UserManager.guestGUID);
        form.AddField("platform", Application.platform.ToString());
        form.AddField("name", UserManager.userName);
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

        ball.GetComponent<MoveBall>().resetBall();
        updateRoad.resetRoad();
        Camera.main.GetComponent<CameraFollowBall>().resetCamera();
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

        ball.GetComponent<MoveBall>().resetBall();
        updateRoad.resetRoad();
        Camera.main.GetComponent<CameraFollowBall>().resetCamera();

        Time.timeScale = 1;
    }


    public void showScore()
    {
        startView.SetActive(false);
        scoreView.SetActive(true);

        setMyInfo();
        StartCoroutine(getTop10Score());
    }

    public void showEditName()
    {
        editNamePlaceholder.text = UserManager.userName;
        nameInput.text = "";
        editNameDialog.SetActive(true);
    }

    public void hideEditName()
    {
        editNameDialog.SetActive(false);
    }

    public void checkName()
    {
        if(String.IsNullOrEmpty(nameInput.text))
        {
            editNameTitle.text = "昵称不能为空";
        }else
        {
            StartCoroutine(uploadName(nameInput.text));
        }
    }

    private IEnumerator uploadName(string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameId", "1");
        form.AddField("userId", UserManager.guestGUID);
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post("http://wycode.cn/api/score/changeName", form))
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
                    WyResultName result = JsonUtility.FromJson<WyResultName>(www.downloadHandler.text);
                    if (result.code == 1)
                    {
                        UserManager.saveName(name);
                        hideEditName();
                        userNameWelcome.text = "欢迎回来：" + name;
                    }else
                    {
                        editNameTitle.text = result.message;
                    }
                    

                }

            }
        }
    }

    private void setMyInfo()
    {
        myNameText.text = UserManager.userName;
        int myScore = UpdateScore.getMyScore();
        myScoreText.text = myScore + "m";
        int myRank = UpdateScore.getMyRank();
        myRankingText.text = "全球排名第" + myRank;
        StartCoroutine(getMyScoreAndRank());
    }

    private IEnumerator getMyScoreAndRank()
    {
        WWWForm form = new WWWForm();
        form.AddField("gameId", "1");
        form.AddField("userId", UserManager.guestGUID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://wycode.cn/api/score/getScoreAndRanking", form))
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
                    WyResultName result = JsonUtility.FromJson<WyResultName>(www.downloadHandler.text);
                    if (result.code == 1)
                    {
                        myScoreText.text = result.data.score + "m";
                        myRankingText.text = "全球排名第" + result.data.rank;

                        UpdateScore.setMyScore(result.data.score);
                        UpdateScore.setMyRank(result.data.rank);
                    }

                }

            }
        }
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
        UpdateScore.setMyRank(result.data);
        
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
