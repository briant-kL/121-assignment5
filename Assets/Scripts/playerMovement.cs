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
    public float sprint;
    public GameObject ragdoll;


    //private variables
    private CharacterController playerController;
    private Animator animator;
    private Vector3 velocity;
    private Vector2 movement;
    private Vector3 movement_direction = Vector3.zero;
    
    private ParticleSystem particles;
    private float _defaultSpeed;


    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();
        
        _defaultSpeed = speed;


        //Debug.Log(animator.GetLayerIndex("WK_heavy_infantry_08_attack_B"));
        

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Attack();
    }




    void Attack()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("attacking", true);
            animator.SetInteger("condition", 4);
            speed = speed/2;
            AttackRoutine();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("attacking", false);
            animator.SetInteger("condition", 0);
            speed = _defaultSpeed;
        }

        
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(1);
        
    }

    void PlayerMovement()
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

        
        //Walking Animations


        //Walking Forward
        if (Input.GetKey("w"))
        {
            //Debug.Log(v);
            //animator.SetBool("walking", true);
            animator.SetInteger("condition", 1);

        }

        else if (Input.GetKeyUp("w"))
        {
            //animator.SetBool("walking", false);
            animator.SetInteger("condition", 0);

        }

   
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) )
        {
            animator.SetBool("running", true);
            speed = sprint;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("running", false);
            speed = _defaultSpeed;
        }


        //walkbackwards
        if (Input.GetKey("s"))
        {
            //Debug.Log(v);
            //animator.SetBool("backwalking", true);
            animator.SetInteger("condition", -1);
        }
        else if (Input.GetKeyUp("s"))
        {
            //animator.SetBool("backwalking", false);
            animator.SetInteger("condition", 0);

        }


        if (Input.GetKey("a"))
        {
           // Debug.Log(v);

            animator.SetInteger("condition", 1);

        }

        else if (Input.GetKeyUp("a"))
        {

            animator.SetInteger("condition", 0);

        }

        if (Input.GetKey("d"))
        {
           // Debug.Log(v);

            animator.SetInteger("condition", -1);

        }

        else if (Input.GetKeyUp("d"))
        {

            animator.SetInteger("condition", 0);

        }








        //check grounded
        if (playerController.isGrounded)
        {
            

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
        if (other.gameObject.CompareTag("kill_env"))
        {
            gameObject.SetActive(false);
            Debug.Log("You died");

            ragdoll.SetActive(true);
        }
        if (other.gameObject.CompareTag("coins"))
        {
            Debug.Log("Collected a coin!");
            other.gameObject.SetActive(false);
        }
    }

    

}
