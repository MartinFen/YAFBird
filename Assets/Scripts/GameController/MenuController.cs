using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private GameObject leaderboardpanel;

    void Awake()
    {
        MakeInstance();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //runs when the play button is clicked in main menu
    public void PlayGame()
    {
        SceneFader.instance.FadeIn("FlappyBird");
    }

    public void ShowPanel()
    {
        Time.timeScale = 1f;
        leaderboardpanel.SetActive(true);
    }

    public void HidePanel()
    {
        Time.timeScale = 1f;
        leaderboardpanel.SetActive(false);
    }
}
