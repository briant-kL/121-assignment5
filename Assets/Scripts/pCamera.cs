using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pCamera : MonoBehaviour
{
    public GameObject player;
    public float minDistance;
    //public float maxDistance;
    private Vector3 FPS;
    public bool cameraFPS = false;
    public float sensitivity;
    public Button _button;
    public GameObject target;


    private float x;
    private float y;


   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // lateupdate Best for camera and procedural animations
    void LateUpdate()
    {
        
        float h = sensitivity * Input.GetAxis("Mouse X");
        float v = sensitivity * -Input.GetAxis("Mouse Y");
        //Debug.Log(h);
        //Debug.Log(v);
        
        //fps controls
        if (cameraFPS == true)
        {
            transform.position = target.transform.position;
            //transform.rotation = player.transform.rotation;
            transform.Rotate(0, h, 0);
            if (Input.GetAxis("Mouse X") != 0)
            {
                player.transform.Rotate(0, h, 0);
                
                //player.transform.position = position;
            }
        }

        //based on Unity's package mouse orbit script for the third person view
        if (cameraFPS == false)
        {

            x += Input.GetAxis("Mouse X") * sensitivity;
            y += Input.GetAxis("Mouse Y") * -sensitivity;

            Quaternion rotation = Quaternion.Euler(0, x, 0);
            Vector3 position = rotation * new Vector3(0f, 0f, -minDistance) + player.transform.position;
            Vector3 position2 = rotation * new Vector3(0f, 0f, -minDistance) + target.transform.position;
            transform.rotation = rotation;
            transform.position = position2;
            if (Input.GetAxis("Mouse X") != 0)
            {
                player.transform.Rotate(0, h, 0);
                target.transform.Rotate(0, h, 0);
            }

            /*
            transform.position = player.transform.position;
            transform.LookAt(player.transform); 
            if (Input.GetAxis("Mouse X") != 0)
            
            {
                //transform.LookAt(player.transform);
                //player.transform.Rotate(0, h, 0);
            }
            
            */
        }
        //checkclick();

    }



    void Update()
    {

         

        
    }

    /*
    void checkclick()
    {
        if (Input.GetKeyDown("e"))
        {
            
            if (cameraFPS == true)
            {
                cameraFPS = false;
                // transform.rotation = player.transform.rotation;
                //transform.localPosition = player.transform.localPosition + tpsDista;
                //transform.localPosition = player.transform.localPosition + TPS;

            }
            else
            {
                cameraFPS = true;
                transform.rotation = player.transform.rotation;

            }
        }
    }

 
    void TaskOnClick()
    {
        cameraFPS = true;
        Debug.Log("button has been clicked");
    }
  */

}
