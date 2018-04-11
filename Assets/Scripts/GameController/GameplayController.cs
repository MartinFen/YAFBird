using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;
    Highscores highScore;

    const string privateCode = "-KiZMx0rdEe0YbNIv0FTFw9niEkJBgI0qIZos78pjT9Q"; //private key for leaderboard
    const string publicCode = "5abb8054012b2e1068d5c879"; //public key for leaderboard
    const string webURL = "http://dreamlo.com/lb/"; //weburl address
    string username = "";

    [SerializeField]
    private Text scoreText, endScore, bestScore, gameOverText;

    [SerializeField]
    private Button restartGameButton, instructionsButton;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private Sprite[] medals;

    [SerializeField]
    private Image medalImage;

    [SerializeField]
    private InputField inputField;

    //runs when class starts
    void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f;
        //when the user has finished entering user name at the end of game screen panel 
        //and clicks out of the input field the users name will be added to the data to be uploaded to dreamlo
        inputField.onEndEdit.AddListener(delegate {
            SetUsername();
        });
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // called when the user pauses the game during gameplay
    public void PauseGame()
    {
        if (BirdScript.instance != null)
        {
            if (BirdScript.instance.isAlive)
            {
                pausePanel.SetActive(true);
                // the scores are added to the text in the panel
                gameOverText.gameObject.SetActive(false);
                endScore.text = "" + BirdScript.instance.score;
                bestScore.text = "" + GameController.instance.GetHighscore();
                Time.timeScale = 0f;
                restartGameButton.onClick.RemoveAllListeners();
                restartGameButton.onClick.AddListener(() => ResumeGame());// calls resume game
            }
        }
    }
    //when called the player is returned to the main menu from the pause menu
    public void GoToMenuButton()
    {
        SceneFader.instance.FadeIn("MainMenu");
    }
    //when called the player is returned to gameplay from the pause menu
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    //when called a new game is started
    public void RestartGame()
    {
        // get the current scene name 
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        //SceneFader.instance.FadeIn("FlappyBird");
    }

    //when the instructions button is clicked this is run to start the game
    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        instructionsButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    
    //sets score
    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    // when called the users name and score are uploaded to leader board
    // see awake function in class
    public void SetUsername()
    {
        //Debug.Log("Input: " + inputField.text);
        AddNewHighscore(inputField.text, BirdScript.instance.score);
    }
    //when called uses IEnumerator below
    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }
    //when called the score is uploaded to online leader board
    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
        {
            print("Error uploading: " + www.error);
        }
    }

    //runs when the player dies
    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        endScore.text = "" + score;

        //update high score if beaten
        if (score > GameController.instance.GetHighscore())
        {
            GameController.instance.SetHighscore(score);
        }

        bestScore.text = "" + GameController.instance.GetHighscore();
        
        //if the score is past a certain score award player with medal
        if (score <= 20)
        {
            medalImage.sprite = medals[0];
        }
        else if (score > 20 && score < 40)
        {
            medalImage.sprite = medals[1];
        }
        else
        {
            medalImage.sprite = medals[2];
        }

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());//if player clicks resume game a new game is started
    }
}
