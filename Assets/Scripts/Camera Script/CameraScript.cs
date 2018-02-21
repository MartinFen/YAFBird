using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public static float offsetX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    //checks if the bird is within the game bounds & if the bird is a live. if so the camera will move 
	void Update () {
        //if the bird is in the game
        if (BirdScript.instance != null) {
            if (BirdScript.instance.isAlive) {
                MoveTheCamera();
            }
        }
		
	}
    //function for the camer to follow the bird object
    void MoveTheCamera() {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + offsetX;
        transform.position = temp;
    }
}
