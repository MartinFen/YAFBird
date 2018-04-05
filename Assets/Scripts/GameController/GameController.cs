using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    private const string HIGH_SCORE = "High Score";
    
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        MakeSingleton();
        IsTheGameStartedForTheFirstTime();
    }

    //this function checks if the game controller object is not null and if it isnt destroy the object or else dont
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //this function is used to check if its the first time the player has run the game and if it is it sets the high score to 0
    void IsTheGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsTheGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);
        }
    }

    public void SetHighscore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);//sets the score
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);//gets high score
    }
}
