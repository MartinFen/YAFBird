using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    private GameObject[] pipeHolders;
    private float distance = 3f;
    private float lastPipeX;
    private float pipeMin = -1f;
    private float pipeMax = 1.6f;


    // Use this for initialization
    void Awake()
    {
        //getting all objects that have tag pieHolder
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        //loops hrough array of pipes
        for (int i = 0; i < pipeHolders.Length; i++)
        {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);//position is based on random number generated between min max
            pipeHolders[i].transform.position = temp;//assigns position
        }

        lastPipeX = pipeHolders[0].transform.position.x;

        for (int i = 1; i < pipeHolders.Length; i++)
        {
            if (lastPipeX < pipeHolders[i].transform.position.x)
            {
                lastPipeX = pipeHolders[i].transform.position.x;
            }
        }
    }

    //function for re-assigning pipes location when a pipeholder collider is triggered
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag =="PipeHolder") {
            Vector3 temp = target.transform.position;

            temp.x = lastPipeX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);

            target.transform.position = temp;
            lastPipeX = temp.x;
        }    
    }

    // Update is called once per frame
    void Update () {
		
	}
}
