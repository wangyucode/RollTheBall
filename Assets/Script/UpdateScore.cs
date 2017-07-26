using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{

    public GameObject ball;

    public Text scoreText;

    [HideInInspector]
    public static float score;

    private static string myScoreKey = "myScoreKey";
    private static string myRankKey = "myRankKey";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        score = ball.transform.position.z;
        scoreText.text = string.Format("{0:N1}m", score);

    }

    public static bool setMyScore()
    {
        if(score > PlayerPrefs.GetInt(myScoreKey, 0))
        {
            PlayerPrefs.SetInt(myScoreKey, (int)score);
            PlayerPrefs.Save();

            return true;
        }else
        {
            return false;
        }
    }


    public static int getMyScore()
    {
        return PlayerPrefs.GetInt(myScoreKey, 0);
    }

    public static bool setMyRank(int rank)
    {
        if (rank > PlayerPrefs.GetInt(myRankKey, 0))
        {
            PlayerPrefs.SetInt(myRankKey, rank);
            PlayerPrefs.Save();

            return true;
        }
        else
        {
            return false;
        }
    }


    public static int getMyRank()
    {
        return PlayerPrefs.GetInt(myRankKey, 0);
    }

}
