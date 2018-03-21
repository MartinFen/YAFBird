using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    //Variables
    public static BirdScript instance;

    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private Animator anim;

    private float forwardSpeed = 3f;

    private float bounceSpeed = 4f;

    public bool didFlap;

    public bool isAlive;

    private Button flapButton;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip flapClick, pointClip, diedClip;

    public int score;


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
                audioSource.PlayOneShot(flapClick);
                anim.SetTrigger("Flap");//tied to trigger in animator
            }
            //controls the birds direction based on velocity
            if (myRigidBody.velocity.y >= 0) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else {
                float angle = 0;
                angle = Mathf.Lerp(0, -80, -myRigidBody.velocity.y / 14);
                transform.rotation = Quaternion.Euler(0, 0, angle );
            }
        }
    }

    //this function is used to set the cameras location
    void SetCamerasX() {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    //this getter is used for the birds location
    public float GetPositionX() {
        return transform.position.x;
    }

    //function for checking if the bird is flapping
    public void FlapTheBird() {
        didFlap = true;
    }
    //this function is called if the bird dies by hitting a pipe or the ground
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(diedClip);
            }
        }
    }
    //this function is called each time the bird passes through the pipholder collider
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
           audioSource.PlayOneShot(pointClip);
        }
    }
}
