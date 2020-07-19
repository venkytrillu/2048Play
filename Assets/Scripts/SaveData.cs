using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static string HighScore = "HighScore";
}


public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void SetValue(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    public int GetValue(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else
            SetValue(name, 0);
        return 0;

    }
}//calss
