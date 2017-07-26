using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void wechatLogin()
    {
        sendMessageToAndroid("wechatLogin");
    }

    public void share2wechat()
    {
        sendMessageToAndroid("share2wechat",UserManager.guestGUID,UserManager.userName,UpdateScore.getMyScore(),UpdateScore.getMyRank());
    }

    public void share2moment()
    {
        sendMessageToAndroid("share2moment", UserManager.guestGUID, UserManager.userName, UpdateScore.getMyScore(), UpdateScore.getMyRank());
    }

    private void sendMessageToAndroid(string method, params object[] args)
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call(method, args);
    }
}
