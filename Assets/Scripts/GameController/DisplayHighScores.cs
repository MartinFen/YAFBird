using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour {

    public Text[] highscoreFields;
    Highscores highscoresManager;

    //when menu scene starts this function runs
    void Start()
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". Fetching...";//update text in leaderboard
        }

        highscoresManager = GetComponent<Highscores>();
        StartCoroutine("RefreshHighscores");//gets scores
    }

    //when called this function gets the scores and puts them into a list
    public void OnHighscoresDownloaded(Highscores.Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreFields.Length; i++)
        {
            highscoreFields[i].text = i + 1 + ". ";
            if (i < highscoreList.Length)
            {
                highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
                print(highscoreList[i].username + " - " + highscoreList[i].score);
            }
        }
    }
    //downloads the highscores every couple of seconds
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoresManager.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
