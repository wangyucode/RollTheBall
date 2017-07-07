using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserManager : MonoBehaviour
{
    private string guestKey = "guestUser";
    [HideInInspector]
    public static int guestId;

    public Text userName;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(guestKey))
        {
            guestId = PlayerPrefs.GetInt(guestKey);
        }
        else
        {
            guestId = Random.Range(15000, 99999);

            PlayerPrefs.SetInt(guestKey, guestId);
            PlayerPrefs.Save();
        }

        userName.text = "欢迎回来：游客" + guestId;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
