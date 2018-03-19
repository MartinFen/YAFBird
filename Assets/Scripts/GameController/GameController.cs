using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    private const string HIGH_SCORE = "High Score";
    private const string SELECTED_BIRD = "Selected Bird";

    // Use this for initialization
    void Start () {
        MakeSingleton();
        IsTheGameStartedForTheFirstTime();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //this function checks if the game controller object is null and if it is destroy the object or else dont
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
    //this function is used to 
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
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }
}
