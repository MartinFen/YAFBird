﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    //Instance variables
    public static BirdScript instance;
    [SerializeField]
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private Animator anim;
    private float forwardSpeed = 3f;
    [SerializeField]
    private float bounceSpeed = 4f;
    public bool didFlap;
    public bool isAlive;

    private Button flapButton;


    void Awake()
    {
        if (instance == null) {
            instance = this;
        }

        isAlive = true;
        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapTheBird());

        SetCamerasX();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()  
    {
        if (isAlive) { 
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFlap) {
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);
                anim.SetTrigger("Flap");//tied to trigger in animator
            }
            //controls the birds direction based on velocity
            if (myRigidBody.velocity.y >= 0) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle );
            }
        }
    }

    //
    void SetCamerasX() {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public float GetPositionX() {
        return transform.position.x;
    }

    //function for checking if the bird is flapping
    public void FlapTheBird() {
        didFlap = true;
    }
}
