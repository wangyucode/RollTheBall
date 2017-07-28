using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    private static string guestNameKey = "guestNameKey";
    private static string guestGUIDKey = "guestGUIDKey";
    [HideInInspector]
    public static string guestGUID;
    [HideInInspector]
    public static string userName;

    public Text userNameText;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(guestNameKey) && PlayerPrefs.HasKey(guestGUIDKey))
        {
            userName = PlayerPrefs.GetString(guestNameKey);
            guestGUID = PlayerPrefs.GetString(guestGUIDKey);
        }
        else
        {
            userName = "游客"+UnityEngine.Random.Range(15000, 99999);
            guestGUID = Guid.NewGuid().ToString();

            PlayerPrefs.SetString(guestNameKey, userName);
            PlayerPrefs.SetString(guestGUIDKey, guestGUID);
            PlayerPrefs.Save();
        }

        userNameText.text = "欢迎回来：" + userName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal static void saveName(string name)
    {
        PlayerPrefs.SetString(guestNameKey, name);
        PlayerPrefs.Save();
        userName = name;
    }
}
