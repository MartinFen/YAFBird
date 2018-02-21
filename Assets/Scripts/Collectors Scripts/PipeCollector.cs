using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    private GameObject[] pipeHolders;
    private float distance = 3f;
    private float lastPipeX;
    private float pipeMin = -0.50f;
    private float pipeMax = 1.25f;
    

	// Use this for initialization
	void Awake () {
        //getting all objects that have tag pieHolder
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        //loops hrough array of pipes
        for (int i = 0; i < pipeHolders.Length; i++){
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);//position is based on random number generated between min max
            pipeHolders[i].transform.position = temp;//assigns position
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
