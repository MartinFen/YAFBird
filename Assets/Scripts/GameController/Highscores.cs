﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    const string privateCode = "-KiZMx0rdEe0YbNIv0FTFw9niEkJBgI0qIZos78pjT9Q"; //private key for leaderboard
    const string publicCode = "5abb8054012b2e1068d5c879"; //public key for leaderboard
    const string webURL = "http://dreamlo.com/lb/"; //weburl address

    DisplayHighScores Display;
    public Highscore[] highScoresList;
    static Highscores instance;

    //when the class runs the awake function gets the high scores from dreamlo and ads it to Display
    void Awake()
    {
        Display = GetComponent<DisplayHighScores>();
        instance = this;
    }
    //this function calls the below IEnumerator
    public void DownloadHighscores()
    {
        StartCoroutine("DownloadFromDatabase");
    }
    //when called this IEnumerator will retrive the high scores from dreamlo
    IEnumerator DownloadFromDatabase()
    {
        // Get your data as pipe delimited providing the address url and the pulic code
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Format(www.text);
            Display.OnHighscoresDownloaded(highScoresList);
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }

    //this function splits up the high scores into username & score which are added to a list
    void Format(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highScoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highScoresList[i] = new Highscore(username, score);
            print(highScoresList[i].username + ": " + highScoresList[i].score);
        }
    }
    //struct that contains two values string & int as well as a method used in the above function
    public struct Highscore
    {
        public string username;
        public int score;

        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }

}