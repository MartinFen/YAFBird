using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance;

    [SerializeField]
    private GameObject fadeCanvas;

    [SerializeField]
    private Animator fadeAnim;

    void Awake()
    {
        MakeSingleton();
    }

    //this method creates an instance of the game object and is called by the awake function above
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

    //function to fade in the chosen scene
    public void FadeIn(string levelName)
    {
        StartCoroutine(FadeInAnimation(levelName));
    }

    //fades out the current scene
    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimation());
    }

    //IEnumerator that is used by the FadeIn function
    IEnumerator FadeInAnimation(string levelName)
    {
        fadeCanvas.SetActive(true);
        fadeAnim.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.7f));// calls IEnumerator WaitForRealSeconds in MyCoroutine class
        SceneManager.LoadScene(levelName);
        FadeOut();
    }
    //IEnumerator that is used by the FadeOut function
    IEnumerator FadeOutAnimation()
    {
        fadeAnim.Play("FadeOut");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        fadeCanvas.SetActive(false);
    }
}
