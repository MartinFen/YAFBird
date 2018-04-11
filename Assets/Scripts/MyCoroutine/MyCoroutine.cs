using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCoroutine : MonoBehaviour {
    //This IEnumerator is used in the Scenefader class to enable the scene to fade out after going into gameplay
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup < (start + time))
        {
            yield return null;
        }

    }
}
