using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    
    //References for player controller
    /*https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483
     * 
     * Standard Assets first person controller c# file
    */


    //Public variables
    public float speed;
    public float Gravity = -9.8f;
    public Vector3 drag;
    public float JumpHeight;
    public Text scoreText;
    


    //private variables
    private CharacterController playerController;
    private Animator animator;
    private Vector3 velocity;
    private Vector2 movement;
    private Vector3 movement_direction = Vector3.zero;
    private int score;



    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        movement = new Vector2(h, v);

        //These lines of code change the rotation of the player object so that it can walk in the right the direction according to the orientation of the camera
        Vector3 moveInput = transform.forward * movement.y + transform.right * movement.x;
        movement_direction.x = moveInput.x * speed;
        movement_direction.z = moveInput.z * speed;
        playerController.Move(movement_direction * Time.deltaTime);


        /*moves the body forward in the direction
        if(move != Vector3.zero)
        {
            transform.forward = move;
        }
        */
        if (Input.GetAxis("Vertical") != 0)
        {
            animator.SetInteger("walking", 1);
        }
        else animator.SetInteger("walking", 0);



        //check grounded
        if (playerController.isGrounded)
        {
            //Jump
            if (Input.GetKeyDown("space"))
            {
                velocity.y += Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }

        }

        //Gravity and Drag physics
        velocity.y += Gravity * Time.deltaTime;
        velocity.x /= 1 + drag.x * Time.deltaTime;
        velocity.y /= 1 + drag.y * Time.deltaTime;
        velocity.z /= 1 + drag.z * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coins"))
        { 
           other.gameObject.SetActive(false);
            score++;
            Debug.Log(score);
            setScoreText();
        }
    }

    void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

}
