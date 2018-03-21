using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeIn("FlappyBird");
    }
}
