using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    private string guestNumberKey = "guestUser";
    private string guestGUIDKey = "guestGUIDKey";
    [HideInInspector]
    public static int guestNumber;
    [HideInInspector]
    public static string guestGUID;

    public Text userName;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(guestNumberKey) && PlayerPrefs.HasKey(guestGUIDKey))
        {
            guestNumber = PlayerPrefs.GetInt(guestNumberKey);
            guestGUID = PlayerPrefs.GetString(guestGUIDKey);
        }
        else
        {
            guestNumber = UnityEngine.Random.Range(15000, 99999);
            guestGUID = Guid.NewGuid().ToString();

            PlayerPrefs.SetInt(guestNumberKey, guestNumber);
            PlayerPrefs.SetString(guestGUIDKey, guestGUID);
            PlayerPrefs.Save();
        }

        userName.text = "欢迎回来：游客" + guestNumber;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
